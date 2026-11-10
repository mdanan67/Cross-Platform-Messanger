using System.Text.Json;
using mauitest.Models;

namespace mauitest.Services;

public sealed class LocalMessengerDataService : IMessengerDataService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private MessengerSeed? _seed;

    public async Task<MessengerSeed> LoadAsync()
    {
        if (_seed is not null)
        {
            return _seed;
        }

        await using var stream = await FileSystem.OpenAppPackageFileAsync("messenger-seed.json");
        _seed = await JsonSerializer.DeserializeAsync<MessengerSeed>(stream, JsonOptions) ?? new MessengerSeed();
        return _seed;
    }
}
