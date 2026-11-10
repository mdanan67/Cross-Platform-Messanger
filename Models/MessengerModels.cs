namespace mauitest.Models;

public sealed class MessengerSeed
{
    public UserProfile CurrentUser { get; set; } = new();
    public List<Conversation> Conversations { get; set; } = [];
    public List<Contact> Contacts { get; set; } = [];
    public List<GroupConversation> Groups { get; set; } = [];
    public List<FriendRequest> FriendRequests { get; set; } = [];
    public List<AppNotification> Notifications { get; set; } = [];
}

public sealed class UserProfile
{
    public string Id { get; set; } = "me";
    public string Name { get; set; } = "Alex Morgan";
    public string Username { get; set; } = "@alexmorgan";
    public string Avatar { get; set; } = "AM";
    public string Status { get; set; } = "Available for a chat";
    public bool IsOnline { get; set; } = true;
}

public sealed class Conversation
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Avatar { get; set; } = "";
    public string Preview { get; set; } = "";
    public string Time { get; set; } = "";
    public int Unread { get; set; }
    public bool IsOnline { get; set; }
    public bool IsPinned { get; set; }
    public bool IsGroup { get; set; }
    public List<ChatMessage> Messages { get; set; } = [];
}

public sealed class ChatMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Sender { get; set; } = "";
    public string Text { get; set; } = "";
    public string Time { get; set; } = "";
    public bool IsMine { get; set; }
    public bool IsRead { get; set; } = true;
}

public sealed class Contact
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Username { get; set; } = "";
    public string Avatar { get; set; } = "";
    public string Status { get; set; } = "";
    public bool IsOnline { get; set; }
    public bool IsFavorite { get; set; }
}

public sealed class GroupConversation
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Avatar { get; set; } = "";
    public string Description { get; set; } = "";
    public int MemberCount { get; set; }
    public string LastActivity { get; set; } = "";
}

public sealed class FriendRequest
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Username { get; set; } = "";
    public string Avatar { get; set; } = "";
    public string MutualFriends { get; set; } = "";
}

public sealed class AppNotification
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Time { get; set; } = "";
    public string Icon { get; set; } = "";
    public bool IsUnread { get; set; }
}
