using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HelloModule
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        private string _stripProp;
        public string StringProp
        {
            get
            {
                return _stripProp;
            }

            set
            {
                if (value != _stripProp)
                {
                    _stripProp = value;
                    OnPropertyChanged(nameof(StringProp));
                }
            }
        }
    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private async Task<string> LoadSettings()
        {
            try
            {
                var path = UserDataPaths.GetDefault().LocalAppData;
                path += "\\Microsoft\\PowerToys\\Hello World\\settings.json";
                var f = await StorageFile.GetFileFromPathAsync(path);
                return await FileIO.ReadTextAsync(f);

            }
            catch (Exception)
            {
                // from https://stackoverflow.com/a/53533414/657390
                await Launcher.LaunchUriAsync(new Uri("ms-settings:appsfeatures-app"));
                return "Please enable filesystem for your app";
            }

        }

        public MainViewModel ViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MainPage content = btn.FindName("TheMainPage") as MainPage;
            content.ViewModel.StringProp = await LoadSettings();
        }

    }

}
