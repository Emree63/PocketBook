using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Model;
using PocketBook.Pages;
using PocketBook.ViewModels;
using StubLib;
using ViewModel;

namespace PocketBook;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Services
            .AddSingleton<IUserLibraryManager, UserLibraryStub>()
            .AddSingleton<ILibraryManager, LibraryStub>()
            .AddSingleton<ManagerVM>()
            .AddSingleton<Manager>()
            .AddSingleton<MainPageVM>()
            .AddSingleton<NavigationVM>();

        builder.Services
            .AddTransient<MainPage>()
			.AddTransient<BooksPage>();

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
