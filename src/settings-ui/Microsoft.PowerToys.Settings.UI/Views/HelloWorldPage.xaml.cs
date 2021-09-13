// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Microsoft.PowerToys.Settings.UI.Helpers;
using Microsoft.PowerToys.Settings.UI.Library;
using Microsoft.PowerToys.Settings.UI.Library.Interfaces;
using Microsoft.PowerToys.Settings.UI.Library.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Microsoft.PowerToys.Settings.UI.Library.ViewModels
{
    public class HelloWorldViewModel : Observable
    {
        private ISettingsUtils SettingsUtils { get; set; }

        private GeneralSettings GeneralSettingsConfig { get; set; }

        private HelloWorldSettings Settings { get; set; }

        private const string ModuleName = HelloWorldSettings.ModuleName;

        private Func<string, int> SendConfigMSG { get; }

        private string _settingsConfigFileFolder = string.Empty;
        private string _stringProp;

        public HelloWorldViewModel(ISettingsUtils settingsUtils, ISettingsRepository<GeneralSettings> settingsRepository, ISettingsRepository<HelloWorldSettings> moduleSettingsRepository, Func<string, int> ipcMSGCallBackFunc, string configFileSubfolder = "")
        {
            SettingsUtils = settingsUtils;

            // Update Settings file folder:
            _settingsConfigFileFolder = configFileSubfolder;

            // To obtain the general PowerToys settings.
            if (settingsRepository == null)
            {
                throw new ArgumentNullException(nameof(settingsRepository));
            }

            GeneralSettingsConfig = settingsRepository.SettingsConfig;

            // To obtain the shortcut guide settings, if the file exists.
            // If not, to create a file with the default settings and to return the default configurations.
            if (moduleSettingsRepository == null)
            {
                throw new ArgumentNullException(nameof(moduleSettingsRepository));
            }

            Settings = moduleSettingsRepository.SettingsConfig;

            // set the callback functions value to hangle outgoing IPC message.
            SendConfigMSG = ipcMSGCallBackFunc;

            _isEnabled = GeneralSettingsConfig.Enabled.HelloWorld;
            _stringProp = Settings.Properties.StringProp.Value;
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;

                    // To update the status of shortcut guide in General PowerToy settings.
                    GeneralSettingsConfig.Enabled.HelloWorld = value;
                    OutGoingGeneralSettings snd = new OutGoingGeneralSettings(GeneralSettingsConfig);

                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        public string StringProp
        {
            get
            {
                return _stringProp;
            }

            set
            {
                if (value != _stringProp)
                {
                    _stringProp = value;
                    Settings.Properties.StringProp.Value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GetSettingsSubPath()
        {
            return _settingsConfigFileFolder + "\\" + ModuleName;
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);

            SettingsUtils.SaveSettings(Settings.ToJsonString(), ModuleName);
        }
    }
}

namespace Microsoft.PowerToys.Settings.UI.Library
{
    public class HelloWorldProperties
    {
        public HelloWorldProperties()
        {
            StringProp = new StringProperty();
        }

        [JsonPropertyName("string_prop")]
        public StringProperty StringProp { get; set; }
    }

    public class HelloWorldSettings : BasePTModuleSettings, ISettingsConfig
    {
        public const string ModuleName = "Hello World";

        [JsonPropertyName("properties")]
        public HelloWorldProperties Properties { get; set; }

        public HelloWorldSettings()
        {
            Name = ModuleName;
            Properties = new HelloWorldProperties();
            Version = "1.0";
        }

        public string GetModuleName()
        {
            return Name;
        }

        // This can be utilized in the future if the settings.json file is to be modified/deleted.
        public bool UpgradeSettingsConfiguration()
        {
            return false;
        }
    }
}

namespace Microsoft.PowerToys.Settings.UI.ViewModels
{
    public class HelloWorldViewModel : Observable
    {
        private ISettingsUtils SettingsUtils { get; set; }

        private GeneralSettings GeneralSettingsConfig { get; set; }

        private HelloWorldSettings Settings { get; set; }

        private const string ModuleName = HelloWorldSettings.ModuleName;

        private Func<string, int> SendConfigMSG { get; }

        private string _settingsConfigFileFolder = string.Empty;
        private string _stringProp;

        public HelloWorldViewModel(ISettingsUtils settingsUtils, ISettingsRepository<GeneralSettings> settingsRepository, ISettingsRepository<HelloWorldSettings> moduleSettingsRepository, Func<string, int> ipcMSGCallBackFunc, string configFileSubfolder = "")
        {
            SettingsUtils = settingsUtils;

            // Update Settings file folder:
            _settingsConfigFileFolder = configFileSubfolder;

            // To obtain the general PowerToys settings.
            if (settingsRepository == null)
            {
                throw new ArgumentNullException(nameof(settingsRepository));
            }

            GeneralSettingsConfig = settingsRepository.SettingsConfig;

            // To obtain the shortcut guide settings, if the file exists.
            // If not, to create a file with the default settings and to return the default configurations.
            if (moduleSettingsRepository == null)
            {
                throw new ArgumentNullException(nameof(moduleSettingsRepository));
            }

            Settings = moduleSettingsRepository.SettingsConfig;

            // set the callback functions value to hangle outgoing IPC message.
            SendConfigMSG = ipcMSGCallBackFunc;

            _isEnabled = GeneralSettingsConfig.Enabled.HelloWorld;
            _stringProp = Settings.Properties.StringProp.Value;
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;

                    // To update the status of shortcut guide in General PowerToy settings.
                    GeneralSettingsConfig.Enabled.HelloWorld = value;
                    OutGoingGeneralSettings snd = new OutGoingGeneralSettings(GeneralSettingsConfig);

                    SendConfigMSG(snd.ToString());
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        public string DisabledApps
        {
            get
            {
                return _stringProp;
            }

            set
            {
                if (value != _stringProp)
                {
                    _stringProp = value;
                    Settings.Properties.StringProp.Value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GetSettingsSubPath()
        {
            return _settingsConfigFileFolder + "\\" + ModuleName;
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);

            SettingsUtils.SaveSettings(Settings.ToJsonString(), ModuleName);
        }
    }

}


namespace Microsoft.PowerToys.Settings.UI.Views
{
    public sealed partial class HelloWorldPage : Page
    {
        private HelloWorldViewModel ViewModel { get; set; }

        public HelloWorldPage()
        {
            InitializeComponent();

            var settingsUtils = new SettingsUtils();
            ViewModel = new HelloWorldViewModel(settingsUtils, SettingsRepository<GeneralSettings>.GetInstance(settingsUtils), SettingsRepository<HelloWorldSettings>.GetInstance(settingsUtils), ShellPage.SendDefaultIPCMessage);
            DataContext = ViewModel;
        }

        private void OpenColorsSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Helpers.StartProcessHelper.Start(Helpers.StartProcessHelper.ColorsSettings);
        }
    }
}
