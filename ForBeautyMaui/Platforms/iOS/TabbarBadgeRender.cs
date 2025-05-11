using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ForBeautyMaui.ViewPages.MainTappedPage.ProfilePages;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;


namespace ForBeautyMaui.Platforms.iOS
{
    public class TabbarBadgeRender : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return base.CreateTabBarAppearanceTracker();
        }

        class BadgeShellTabbarAppearanceTracker : ShellTabBarAppearanceTracker
        {
            private UITabBarItem? _carTabbarItem;
            public override void UpdateLayout(UITabBarController controller)
            {
                base.UpdateLayout(controller);
                if (_carTabbarItem == null)
                {

                    const int cartTabbarItemIndex = 1;

                    var cartTabbarItem = controller.TabBar.Items?[cartTabbarItemIndex];
                    if (cartTabbarItem != null)
                    {
                        UpdateBadge(0);
                        BadgeCounterService.CountChanged += OnCountCganged;
                    }
                }
                 
            }
            private void OnCountCganged(object? sender, int newCount)
            {
                UpdateBadge(newCount);
            }
            private void UpdateBadge(int count)
            {
                if (_carTabbarItem !=null) 
                {
                    if (count <= 0)
                    {
                        _carTabbarItem.BadgeValue = null;
                    }
                    else
                    {
                        _carTabbarItem.BadgeValue = count.ToString();
                        _carTabbarItem.BadgeColor = Colors.Red.ToPlatform();
                    }
                }
            }
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                BadgeCounterService.CountChanged -= OnCountCganged;
            }


        }
    }
}
