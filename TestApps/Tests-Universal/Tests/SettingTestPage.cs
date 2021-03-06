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
using PlatformBindings.Models.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.TestGenerator;

namespace Tests.Tests
{
    public class SettingTestPage : TestPage
    {
        public SettingTestPage(ITestPageGenerator PageGenerator) : base("Settings Tests", PageGenerator)
        {
            TimesRan.Value++;

            AddTestItem(new TestTask
            {
                Name = "Get all Local Settings",
                Test = ui => Task.Run(() =>
                {
                    CurrentContainer = AppServices.Current.IO.LocalSettings;
                    return GetAllSubValues();
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Get all Roaming Settings",
                Test = ui => Task.Run(() =>
                {
                    CurrentContainer = AppServices.Current.IO.RoamingSettings;
                    return GetAllSubValues();
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Get Container",
                Test = ui => Task.Run(async () =>
                {
                    var name = await AppServices.Current.UI.RequestTextFromUserAsync("Get Container", "Container Name", "OK", "Cancel");
                    if (name == null) return Cancelled;

                    var value = CurrentContainer.GetContainer(name);
                    if (CurrentContainer.GetContainers().Any(item => item.Name == name))
                    {
                        CurrentContainer = value;
                        return GetAllSubValues();
                    }
                    else
                    {
                        return "Container creation failed";
                    }
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Get Container Values",
                Test = ui => Task.Run(() =>
                {
                    return GetAllSubValues();
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Display Entire Setting Tree",
                Test = ui => Task.Run(() =>
                {
                    return GetEntireContainerTree(CurrentContainer, 0);
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Go Parent Container",
                Test = ui => Task.Run(() =>
                {
                    if (CurrentContainer.Parent != null)
                    {
                        CurrentContainer = CurrentContainer.Parent;
                        return GetAllSubValues();
                    }
                    else
                    {
                        return "This is the Root Container";
                    }
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Remove Container",
                Test = ui => Task.Run(() =>
                {
                    var parent = CurrentContainer.Parent;
                    if (parent != null)
                    {
                        CurrentContainer.Remove();
                        CurrentContainer = parent;
                        return "Success";
                    }
                    return "Can't remove Root container";
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Get Setting",
                Test = ui => Task.Run(async () =>
                {
                    var name = await AppServices.Current.UI.RequestTextFromUserAsync("Get Setting", "Setting Name", "OK", "Cancel");
                    if (name == null) return Cancelled;

                    var values = CurrentContainer.GetValues();
                    if (values.ContainsKey(name))
                    {
                        var value = values[name];
                        return $"Name: {name} Value: {value} ValueType: {value.GetType().Name}";
                    }
                    else
                    {
                        return "Setting not Found";
                    }
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Create Setting",
                Test = ui => Task.Run(async () =>
                {
                    var creator = "Create Setting";
                    var name = await AppServices.Current.UI.RequestTextFromUserAsync(creator, "Setting Name", "OK", "Cancel");
                    if (name == null) return Cancelled;
                    var value = await AppServices.Current.UI.RequestTextFromUserAsync(creator, "Setting Value", "OK", "Cancel");
                    if (value == null) return Cancelled;

                    CurrentContainer.SetValue(name, value);
                    var setting = CurrentContainer.GetValue<string>(name);
                    if (setting != null)
                    {
                        if (setting == value)
                        {
                            return "Success";
                        }
                        else
                        {
                            return "Failed: Setting Value incorrect";
                        }
                    }
                    else return "Failed: Setting Storage Failed";
                })
            });

            AddTestItem(new TestTask
            {
                Name = "Remove Setting",
                Test = ui => Task.Run(async () =>
                {
                    var creator = "Create Setting";
                    var name = await AppServices.Current.UI.RequestTextFromUserAsync(creator, "Setting Name", "OK", "Cancel");
                    if (name == null) return Cancelled;

                    if (CurrentContainer.ContainsKey(name))
                    {
                        CurrentContainer.RemoveKey(name);
                        var setting = CurrentContainer.GetValue<string>(name);
                        if (setting != null)
                        {
                            return "Failed: Setting not removed";
                        }
                        else return "Success";
                    }
                    else return "Setting not found";
                })
            });

            AddTestItem(ContainerParentDisplay);
            AddTestItem(CurrentContainerDisplay);
            AddTestItem(TimesRanDisplay);

            TimesRanDisplay.UpdateValue(TimesRan.Value.ToString());

            CurrentContainer = AppServices.Current.IO.LocalSettings;
            SettingsHistory.Add(new ContainerFrame { Name = CurrentContainer.Name, Values = CurrentContainer.GetValues().Select(item => $"Name: {item.Key} Value: {item.Value} ValueType: {item.Value.GetType().Name}").ToList() });
        }

        private string GetAllSubValues()
        {
            string containertext = "Containers: \n";
            var containers = CurrentContainer.GetContainers();
            containertext += containers.Count > 0 ? string.Join("\n", containers.Select(item => item.Name)) : "No Containers";

            var settingtext = $"Settings: \n";
            var settings = CurrentContainer.GetValues();
            settingtext += settings.Count > 0 ? string.Join("\n", settings.Select(item => $"Name: {item.Key} Value: {item.Value} ValueType: {item.Value.GetType().Name}")) : "No Settings";

            return settingtext + "\n\n" + containertext;
        }

        private string GetEntireContainerTree(ISettingsContainer ContainerNode, int indentlevel)
        {
            var indent = Indent(indentlevel);
            var Header = $"^B^{ContainerNode.Name}:^B^ \n";
            indentlevel++;

            string containertext = indent + "Containers: \n";
            var containers = ContainerNode.GetContainers();
            if (containers.Any())
            {
                foreach (var container in containers)
                {
                    containertext += Indent(indentlevel) + GetEntireContainerTree(container, indentlevel);
                }
            }
            else
            {
                containertext += Indent(indentlevel) + "No Containers";
            }

            var settingtext = $"{indent}Settings: \n";
            var settings = ContainerNode.GetValues();
            settingtext += settings.Count > 0 ? string.Join("\n", settings.Select(item => indent + $"Name: {item.Key} Value: {item.Value} ValueType: {item.Value.GetType().Name}")) : indent + "No Settings";

            return Header + settingtext + "\n\n" + containertext + "\n";
        }

        private string Indent(int IndentLevel)
        {
            return new string(' ', IndentLevel * 4);
        }

        private string Cancelled => "Cancelled";

        public ISettingsContainer CurrentContainer
        {
            get { return _CurrentContainer; }
            set
            {
                _CurrentContainer = value;
                CurrentContainerDisplay.UpdateValue(value.Name);

                ContainerParentDisplay.UpdateValue(value.Parent?.Name ?? "None");
            }
        }

        private ISettingsContainer _CurrentContainer;

        public TestProperty ContainerParentDisplay { get; } = new TestProperty("Parent");
        public TestProperty CurrentContainerDisplay { get; } = new TestProperty("Current Container");
        public TestProperty TimesRanDisplay { get; } = new TestProperty("Times Ran");

        public AppSetting<int> TimesRan = new AppSetting<int>();
        private SerialAppSettingList<ContainerFrame> SettingsHistory = new SerialAppSettingList<ContainerFrame>();

        private class ContainerFrame
        {
            public string Name { get; set; }
            public List<string> Values { get; set; }
        }
    }
}