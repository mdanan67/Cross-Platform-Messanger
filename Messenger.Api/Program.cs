using Messenger.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(options => options.AddPolicy("DevelopmentClient", policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("DevelopmentClient");

var user = new UserDto("me", "Alex Morgan", "@alexmorgan", "AM", "Available for a chat", true);
var conversations = new List<ConversationDto>
{
    new("sophie", "Sophie Wilson", "SW", "That sounds perfect! See you soon.", DateTimeOffset.UtcNow, 2, true, false,
    [
        new("m1", "Sophie Wilson", "Hey Alex! Are we still on for coffee this afternoon?", DateTimeOffset.UtcNow.AddMinutes(-14), false, true),
        new("m2", "Alex Morgan", "Absolutely! I found a lovely new place downtown.", DateTimeOffset.UtcNow.AddMinutes(-11), true, true),
        new("m3", "Sophie Wilson", "That sounds perfect! See you soon.", DateTimeOffset.UtcNow.AddMinutes(-8), false, true)
    ]),
    new("design", "Design team", "DT", "Maya shared a new prototype", DateTimeOffset.UtcNow.AddHours(-1), 5, true, true,
    [new("m4", "Maya Chen", "I shared a new prototype for everyone to review.", DateTimeOffset.UtcNow.AddHours(-1), false, true)]),
    new("james", "James Anderson", "JA", "I will send the files tonight.", DateTimeOffset.UtcNow.AddDays(-1), 0, false, false,
    [new("m5", "James Anderson", "I will send the files tonight.", DateTimeOffset.UtcNow.AddDays(-1), false, true)])
};

var contacts = new List<ContactDto>
{
    new("sophie", "Sophie Wilson", "@sophiew", "SW", "Designing a better day", true, true),
    new("emma", "Emma Davis", "@emmadavis", "ED", "Available for a chat", true, true),
    new("james", "James Anderson", "@jamesa", "JA", "Away", false, false),
    new("olivia", "Olivia Brown", "@oliviab", "OB", "Exploring new places", false, false)
};

var groups = new List<GroupDto>
{
    new("design", "Design team", "DT", "Ideas, feedback, and good work.", 8, "Maya shared a new prototype"),
    new("weekend", "Weekend plans", "WP", "Making the weekend count.", 6, "James added a new place")
};

var notifications = new List<NotificationDto>
{
    new("n1", "Sophie sent you a message", "That sounds perfect! See you soon.", DateTimeOffset.UtcNow.AddMinutes(-2), "chat", true),
    new("n2", "New friend request", "Noah Williams wants to connect with you.", DateTimeOffset.UtcNow.AddHours(-1), "person", true),
    new("n3", "You were added to Design team", "Maya added you to a new group.", DateTimeOffset.UtcNow.AddDays(-1), "group", false)
};

app.MapGet("/api/health", () => Results.Ok(new { status = "ok", service = "Messenger.Api" }));

app.MapPost("/api/auth/login", (LoginRequest request) =>
    Results.Ok(new AuthResponse("demo-token", user)));

app.MapPost("/api/auth/register", (RegisterRequest request) =>
    Results.Ok(new AuthResponse("demo-token", user with { Name = request.Name })));

app.MapGet("/api/me", () => Results.Ok(user));
app.MapGet("/api/conversations", () => Results.Ok(conversations));
app.MapGet("/api/conversations/{id}/messages", (string id) =>
    conversations.FirstOrDefault(item => item.Id == id) is { } conversation
        ? Results.Ok(conversation.Messages)
        : Results.NotFound());

app.MapPost("/api/conversations/{id}/messages", (string id, SendMessageRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
    {
        return Results.BadRequest(new { message = "Message text is required." });
    }

    var conversation = conversations.FirstOrDefault(item => item.Id == id);
    if (conversation is null)
    {
        return Results.NotFound();
    }

    var message = new MessageDto(Guid.NewGuid().ToString(), user.Name, request.Text.Trim(), DateTimeOffset.UtcNow, true, true);
    var updated = conversation with
    {
        Preview = message.Text,
        UpdatedAt = message.SentAt,
        Messages = [.. conversation.Messages, message]
    };
    conversations[conversations.IndexOf(conversation)] = updated;
    return Results.Ok(message);
});

app.MapGet("/api/contacts", () => Results.Ok(contacts));
app.MapGet("/api/groups", () => Results.Ok(groups));
app.MapGet("/api/notifications", () => Results.Ok(notifications));

app.Run();