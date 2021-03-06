// ******************************************************************
// Copyright (c) William Bradley
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using PlatformBindings;
using PlatformBindings.Controls.MenuLayout;
using PlatformBindings.Enums;
using System.Threading.Tasks;
using Tests.TestGenerator;

namespace Tests.Tests
{
    public class ContextMenuTestPage : TestPage
    {
        public ContextMenuTestPage(ITestPageGenerator PageGenerator) : base("ContextMenu Tests", PageGenerator)
        {
            AddTestItem(new TestTask
            {
                Name = "Show Menu",
                Test = ui => Task.Run(() =>
                {
                    AppServices.Current.UI.ShowMenu(Menu, ui);
                    return (string)null;
                })
            });

            var registered = new TestTask
            {
                Name = "Registered Menu",
                Test = ui => Task.Run(() =>
                {
                    return (string)null;
                })
            };

            registered.UIAttached += (s, e) =>
            {
                AppServices.Current.UI.RegisterMenu(Menu, e);
            };

            AddTestItem(registered);

            if (AppServices.Current.UI.UIPlatform == Platform.UWP)
            {
                AppServices.Current.UI.RegisterMenu(Menu, PageGenerator.UIInstance);
                var prop = new TestProperty("Note");
                prop.UpdateValue("Right Click on any free part of the Page to access the Context Menu");
                AddTestItem(prop);
            }
        }

        private static void Selected(MenuItem Item)
        {
            AppServices.Current.UI.PromptUser("Selected", $"^B^{Item.Label}^B^ Pressed", "OK", null);
            if (Item is ToggleMenuItem tog)
            {
                tog.IsToggled = tog.IsToggled != true;
            }
        }

        private Menu Menu { get; } = new Menu
        {
            new MenuItem{ Label = "Element", Action = Selected },
            new MenuItem { Label = "Disabled", IsEnabled = false, Action = Selected },
            new ToggleMenuItem { Label = "Toggleable", Action = Selected },
            new ToggleMenuItem { Label = "Toggleable Disabled", IsToggled = true, IsEnabled = false, Action = Selected },
            new MenuSeparator(),
            new SubMenu("SubMenu")
            {
                new MenuItem { Label = "SubElement", Action = Selected }
            }
        };
    }
}