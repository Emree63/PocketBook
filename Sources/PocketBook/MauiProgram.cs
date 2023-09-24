using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace PocketBook;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
                fonts.AddFont("SF-Pro-Display-Bold.otf", "SF Pro Display Bold");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SF-Pro-Text-Heavy.otf", "SFProHeavy");
                fonts.AddFont("SF-Pro-Text-Semibold.otf", "SFProSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
