using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;

namespace LocationHeatMapApp.Services;

public class LocationService
{
    public async Task<Location?> GetCurrentLocationAsync()
    {
        try
        {
            PermissionStatus permissionStatus =
                await Permissions.CheckStatusAsync<
                    Permissions.LocationWhenInUse>();

            if (permissionStatus !=
                PermissionStatus.Granted)
            {
                permissionStatus =
                    await Permissions.RequestAsync<
                        Permissions.LocationWhenInUse>();
            }

            if (permissionStatus !=
                PermissionStatus.Granted)
            {
                return null;
            }

            var request = new GeolocationRequest(
                GeolocationAccuracy.High,
                TimeSpan.FromSeconds(15));

            Location? location =
                await Geolocation.Default
                    .GetLocationAsync(request);

            if (location is not null)
            {
                return location;
            }

            return await Geolocation.Default
                .GetLastKnownLocationAsync();
        }
        catch (FeatureNotSupportedException)
        {
            return null;
        }
        catch (FeatureNotEnabledException)
        {
            return null;
        }
        catch (PermissionException)
        {
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Location error: {ex.Message}");

            return null;
        }
    }
}