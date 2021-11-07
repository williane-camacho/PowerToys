using Microsoft.UI.Xaml;
using System.Globalization;
using Microsoft.PowerToys.Settings.UI.Library;

// TODO(stefan)
using WindowsUI = Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Microsoft.PowerToys.Settings.UI.WinUI3
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            //m_window.ExtendsContentIntoTitleBar = true;
            //m_window.SetTitleBar(m_window.CustomTitleBar); // This should work according to docs, but it doesn't.... So disabling for now together with ExtendsContentIntoTitleBar
            m_window.Title = "PowerToys Settings";
            m_window.Activate();
        }

        public static bool IsDarkTheme()
        {
            var selectedTheme = SettingsRepository<GeneralSettings>.GetInstance(settingsUtils).SettingsConfig.Theme.ToUpper(CultureInfo.InvariantCulture);
            var defaultTheme = new WindowsUI.ViewManagement.UISettings();
            var uiTheme = defaultTheme.GetColorValue(WindowsUI.ViewManagement.UIColorType.Background).ToString(System.Globalization.CultureInfo.InvariantCulture);
            return selectedTheme == "DARK" || (selectedTheme == "SYSTEM" && uiTheme == "#FF000000");
        }

        private Window m_window;
        private static ISettingsUtils settingsUtils = new SettingsUtils();
    }
}
