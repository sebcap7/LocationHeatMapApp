using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace LocationHeatMapApp.Services;

public class LocationService
{
    public async Task<Location?> GetCurrentLocationAsync()
    {
        try
        {
            PermissionStatus permission =
                await Permissions.CheckStatusAsync<
                    Permissions.LocationWhenInUse>();

            if (permission != PermissionStatus.Granted)
            {
                permission =
                    await Permissions.RequestAsync<
                        Permissions.LocationWhenInUse>();
            }

            if (permission != PermissionStatus.Granted)
            {
                return null;
            }

            var request = new GeolocationRequest(
                GeolocationAccuracy.High,
                TimeSpan.FromSeconds(15));

            Location? currentLocation =
                await Geolocation.Default.GetLocationAsync(request);

            if (currentLocation is not null)
            {
                return currentLocation;
            }

            return await Geolocation.Default
                .GetLastKnownLocationAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"Location error: {exception.Message}");

            return null;
        }
    }
}