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
            // return base.CreateTabBarAppearanceTracker();
            
           return new BadgeShellTabbarAppearanceTracker();
        }

        class BadgeShellTabbarAppearanceTracker : ShellTabBarAppearanceTracker
        {
            private readonly Dictionary<int, UITabBarItem> _trackedTabItems = new();
            private UITabBar? _tabBar;

            public override void UpdateLayout(UITabBarController controller)
            {
                base.UpdateLayout(controller);

                _tabBar = controller.TabBar;

                if (_trackedTabItems.Count == 0)
                {
                    BadgeCounterService.CountChanged += OnCountChanged;

                    TrackTabIndex(2);
                    TrackTabIndex(3);
                }
            }

            private void TrackTabIndex(int index)
            {
                if (_tabBar?.Items?.Length > index && !_trackedTabItems.ContainsKey(index))
                {
                    var item = _tabBar.Items[index];
                    _trackedTabItems[index] = item;
                    UpdateBadge(index, BadgeCounterService.GetCount(index));
                }
            }

            private void OnCountChanged(object? sender, (int index, int count) e)
            {
                UpdateBadge(e.index, e.count);
            }

            private void UpdateBadge(int index, int count)
            {
                if (_trackedTabItems.TryGetValue(index, out var tabItem))
                {
                    tabItem.BadgeValue = count > 0 ? count.ToString() : null;
                    tabItem.BadgeColor = Colors.Red.ToPlatform();
                }
            }
        }

    }
}
