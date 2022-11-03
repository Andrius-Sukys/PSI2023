﻿using GP3.Client.Refit;
using GP3.Client.Services;
using GP3.Client.ViewModels;
namespace GP3.Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold");
                fonts.AddFont("Sitka.ttc", "Sitka");
            });

        /* Pages and viewmodels should be transient */
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainViewModel>();

        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<RegisterViewModel>();

        builder.Services.AddTransient<HistoryPage>();
        builder.Services.AddTransient<HistoryViewModel>();

		builder.Services.AddTransient<HistoryMonthAddViewModel>();
		builder.Services.AddTransient<HistoryMonthAddPage>();

        builder.Services.AddTransient<StatisticsPage>();
        builder.Services.AddTransient<StatisticsViewModel>();

        /* Services */
        builder.Services.AddSingleton<HistoryService>();
        builder.Services.AddSingleton<AuthService>();

        /* TODO: Make these options configurable */
        var apiRetryCount = 3;
        var apiRetryWait = TimeSpan.FromSeconds(1);
        var apiTimeout = TimeSpan.FromSeconds(10);
        builder.Configuration["ApiURI"] = "https://grupe3.azurewebsites.net/";

        builder.Services
            .AddResilientApi<IPriceApi>(builder.Configuration["ApiURI"], apiRetryCount, apiRetryWait, apiTimeout)
            .AddResilientApi<IReadingApi>(builder.Configuration["ApiURI"], apiRetryCount, apiRetryWait, apiTimeout)
            .AddResilientApi<IIntegrationApi>(builder.Configuration["ApiURI"], apiRetryCount, apiRetryWait, apiTimeout);

        return builder.Build();
	}
}
