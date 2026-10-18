Location Heat Map App
Overview
The Location Heat Map App is a mobile application developed using C# and .NET MAUI. The application retrieves the user’s current location and stores the latitude, longitude, and timestamp in a local SQLite database.

The application also calculates a heat level based on the number of recorded visits near the current location. A heat-map image demonstrates how the saved location data can be visualized.

Features
Retrieves the current GPS location
Requests location permission from the user
Stores latitude, longitude, and timestamp values
Uses SQLite for local data storage
Retrieves previously saved locations
Counts visits near the current location
Classifies activity as LOW, MEDIUM, or HIGH heat
Displays a heat-map visualization
Shows the current coordinates and heat level
Handles location and application errors
Technologies Used
C#
.NET MAUI
XAML
SQLite
Android SDK
Visual Studio Code
Android Studio Virtual Device
Project Structure
LocationHeatMapApp
├── Models
│   └── LocationRecord.cs
├── Services
│   ├── DatabaseService.cs
│   └── LocationService.cs
├── Platforms
│   └── Android
│       └── AndroidManifest.xml
├── Resources
│   └── Images
│       └── heatmap.png
├── MainPage.xaml
├── MainPage.xaml.cs
├── MauiProgram.cs
├── LocationHeatMapApp.csproj
└── README.md
Application Components
LocationRecord
The LocationRecord model represents one saved location in the SQLite database. Each record contains:

A unique ID
Latitude
Longitude
Timestamp
DatabaseService
The DatabaseService class manages the SQLite database. It performs the following tasks:

Creates the location table
Saves new location records
Retrieves previously saved records
LocationService
The LocationService class manages location access. It performs the following tasks:

Checks the current permission status
Requests location permission when needed
Retrieves the current GPS location
Returns the last known location when available
Handles location-related errors
MainPage
The main page provides the application’s user interface. It includes:

Application title
Save Current Location button
Heat-map visualization
Saved-location status
Location confirmation message
Heat-Level Calculation
The application compares the latitude and longitude of saved records. Locations within approximately 0.0005 degrees of the current position are counted as nearby visits.

The heat levels are:

LOW HEAT: One or two nearby visits
MEDIUM HEAT: Three to five nearby visits
HIGH HEAT: More than five nearby visits
This calculation provides a simple method for identifying frequently visited locations.

SQLite Database
The application stores location information in a local SQLite database named:

locations.db3
The database is created automatically when the application first uses the database service.

Each record contains:

ID
Latitude
Longitude
Timestamp
Android Permissions
The application requires permission to access the device location.

The following permissions are included in AndroidManifest.xml:

<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
<uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION" />
The user must allow location access before the application can retrieve and save the current position.

Requirements
The following software is required:

.NET 10 SDK
.NET MAUI workload
Java Development Kit
Android Studio
Android SDK
Android emulator or physical Android device
Visual Studio Code
C# Dev Kit extension
NuGet Package
The application uses the sqlite-net-pcl package for SQLite database operations.

Install the package with:

dotnet add package sqlite-net-pcl --version 1.11.285
Build Instructions
Open a terminal in the project directory.

Restore the required packages:

dotnet restore
Build the Android application:

dotnet build -f net10.0-android
Start the Android emulator and verify the connection:

adb devices
A connected emulator should appear similar to:

emulator-5554    device
Run the application:

dotnet run -f net10.0-android
Using the Application
Start the Android virtual device.
Run the application.
Select Save Current Location.
Allow location access when prompted.
Wait for the application to retrieve the current position.
Review the displayed latitude and longitude.
Review the nearby-visit count and heat level.
Save the location multiple times to observe the heat-level changes.
The heat level changes according to the following rules:

1–2 visits: LOW HEAT
3–5 visits: MEDIUM HEAT
6 or more visits: HIGH HEAT
Testing
The application was tested using an Android virtual device.

Testing included:

Successful application startup
Runtime location-permission request
GPS location retrieval
SQLite database creation
Location record insertion
Retrieval of saved records
Nearby-location counting
LOW, MEDIUM, and HIGH heat classification
User confirmation messages
Error handling when location information is unavailable
Heat-Map Visualization
The application displays a heat-map image named:

heatmap.png
The image is stored in:

Resources/Images/heatmap.png
The image demonstrates the intended presentation of location-density information. The application independently performs real location retrieval, SQLite storage, nearby-visit counting, and heat-level classification.

Future Improvements
Future improvements could include:

Interactive map integration
Dynamic heat-map overlays
Colored location markers
Background location tracking
Route-history visualization
Location-history filtering
Option to delete saved records
Location data export
Cloud synchronization
User authentication
Database encryption
Additional privacy controls
Privacy and Security
Location data is sensitive information. This application stores location records locally in the application’s private SQLite database.

A production version should also include:

A clear privacy notice
Data-retention settings
Database encryption
An option to delete saved location history
Secure cloud synchronization
Limited background-location access
