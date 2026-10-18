using LocationHeatMapApp.Models;
using LocationHeatMapApp.Services;

namespace LocationHeatMapApp;

public partial class MainPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private readonly LocationService _locationService;

    public MainPage()
    {
        InitializeComponent();

        _databaseService = new DatabaseService();
        _locationService = new LocationService();

        _ = UpdateSavedLocationCountAsync();
    }

    private async void OnSaveLocationClicked(
        object? sender,
        EventArgs e)
    {
        SaveLocationButton.IsEnabled = false;
        SaveLocationButton.Text = "Getting Location...";
        StatusLabel.Text = "Retrieving current location...";

        try
        {
            Location? currentLocation =
                await _locationService.GetCurrentLocationAsync();

            if (currentLocation is null)
            {
                StatusLabel.Text =
                    "Unable to retrieve the current location.";

                await DisplayAlertAsync(
                    "Location Error",
                    "Check that location access is enabled.",
                    "OK");

                return;
            }

            var locationRecord = new LocationRecord
            {
                Latitude = currentLocation.Latitude,
                Longitude = currentLocation.Longitude,
                Timestamp = DateTime.Now
            };

            await _databaseService.SaveLocationAsync(
                locationRecord);

            List<LocationRecord> locations =
                await _databaseService.GetLocationsAsync();

            int nearbyVisits = locations.Count(location =>
                Math.Abs(
                    location.Latitude -
                    locationRecord.Latitude) < 0.0005 &&
                Math.Abs(
                    location.Longitude -
                    locationRecord.Longitude) < 0.0005);

            string heatLevel = nearbyVisits switch
            {
                > 5 => "HIGH HEAT",
                > 2 => "MEDIUM HEAT",
                _ => "LOW HEAT"
            };

            StatusLabel.Text =
                $"{heatLevel} | {nearbyVisits} nearby visit(s)";

            string message =
                $"Latitude: {locationRecord.Latitude:F6}" +
                Environment.NewLine +
                $"Longitude: {locationRecord.Longitude:F6}" +
                Environment.NewLine +
                $"Nearby Visits: {nearbyVisits}" +
                Environment.NewLine +
                $"Heat Level: {heatLevel}";

            Console.WriteLine(
                $"{heatLevel}: " +
                $"{locationRecord.Latitude}, " +
                $"{locationRecord.Longitude}");

            await DisplayAlertAsync(
                "Location Saved",
                message,
                "OK");
        }
        catch (Exception exception)
        {
            StatusLabel.Text =
                "The location could not be saved.";

            await DisplayAlertAsync(
                "Application Error",
                exception.Message,
                "OK");
        }
        finally
        {
            SaveLocationButton.IsEnabled = true;
            SaveLocationButton.Text = "Save Current Location";
        }
    }

    private async Task UpdateSavedLocationCountAsync()
    {
        try
        {
            List<LocationRecord> locations =
                await _databaseService.GetLocationsAsync();

            StatusLabel.Text = locations.Count == 0
                ? "No saved locations yet."
                : $"{locations.Count} saved location(s) in SQLite.";
        }
        catch (Exception exception)
        {
            StatusLabel.Text =
                "Unable to load saved locations.";

            Console.WriteLine(exception.Message);
        }
    }
}