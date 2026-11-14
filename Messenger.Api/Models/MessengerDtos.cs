namespace Messenger.Api.Models;

public sealed record UserDto(string Id, string Name, string Username, string Avatar, string Status, bool IsOnline);

public sealed record MessageDto(string Id, string Sender, string Text, DateTimeOffset SentAt, bool IsMine, bool IsRead);

public sealed record ConversationDto(string Id, string Name, string Avatar, string Preview, DateTimeOffset UpdatedAt, int Unread, bool IsOnline, bool IsGroup, IReadOnlyList<MessageDto> Messages);

public sealed record ContactDto(string Id, string Name, string Username, string Avatar, string Status, bool IsOnline, bool IsFavorite);

public sealed record GroupDto(string Id, string Name, string Avatar, string Description, int MemberCount, string LastActivity);

public sealed record FriendRequestDto(string Id, string Name, string Username, string Avatar, string MutualFriends);

public sealed record NotificationDto(string Id, string Title, string Description, DateTimeOffset CreatedAt, string Icon, bool IsUnread);

public sealed record LoginRequest(string Email, string Password);

public sealed record RegisterRequest(string Name, string Email, string Password);

public sealed record SendMessageRequest(string Text);

public sealed record AuthResponse(string AccessToken, UserDto User);
