using Microsoft.Ext**sions.Logging;

namespace Locatio**eatMapApp;

public static class M**iProgram
{
    public static Maui**p CreateMauiApp()
    {
        v** builder = MauiApp.CreateBuilder(**

        builder
            .Us**auiApp<App>()
            .Config**eFonts(fonts =>
            {
   **           fonts.AddFont(
       **           "OpenSans-Regular.ttf"**                    "OpenSansRegu**r");

                fonts.AddFo**(
                    "OpenSans-S**ibold.ttf",
                    "**enSansSemibold");
            });**#if DEBUG
        builder.Logging**ddDebug();
#endif

        return**uilder.Build();
    }
}