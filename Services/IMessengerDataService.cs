using mauitest.Models;

namespace mauitest.Services;

public interface IMessengerDataService
{
    Task<MessengerSeed> LoadAsync();
}
