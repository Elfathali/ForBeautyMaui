using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Syncfusion.Maui.Core.Hosting;

namespace ForBeautyMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(h =>
                {
#if IOS
                    h.AddHandler<Shell, ForBeautyMaui.Platforms.iOS.TabbarBadgeRender>();
#endif


                });
#if DEBUG
                    builder.Logging.AddDebug();
#endif
            EntryHandler.Mapper.AppendToMapping("BorderlessEntry", (handler, view) =>
            {
#if IOS

    if (view is ForBeautyMaui.Renders.BorderLessEntry)
    {
        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
    }
#endif
            });


            return builder.Build();
        }
    }
}
