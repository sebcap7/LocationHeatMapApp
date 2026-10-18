using L***tionHeatMapApp.Models;
using SQL***;

namespace LocationHeatMapApp.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;

    private async Task InitializeAsync()
    {
        if (_database is not null)
        {
            return;
        }

        string databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "locations.db3");

        _database =
            new SQLiteAsyncConnection(databasePath);

        await _database.CreateTableAsync<LocationRecord>();
    }

    public async Task SaveLocationAsync(
        LocationRecord location)
    {
        ArgumentNullException.ThrowIfNull(location);

        await InitializeAsync();

        await _database!.InsertAsync(location);
    }

    public async Task<List<LocationRecord>>
        GetLocationsAsync()
    {
        await InitializeAsync();

        return await _database!
            .Table<LocationRecord>()
            .OrderBy(location => location.Timestamp)
            .ToListAsync();
    }
}