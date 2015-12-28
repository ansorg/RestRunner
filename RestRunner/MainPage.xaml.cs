using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace RestRunner
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private App App;

        public MainPage()
        {
            this.InitializeComponent();
            this.App = (App)Application.Current;
            if (App.appSettings != null)
            {
                UrlInput.Text = App.appSettings.Url;
                comboBox.SelectedValue = App.appSettings.Method;
            }
            else
            {
                App.appSettings = new RestRunnerSettings("{Url:'http://www.google.de'}");
            }
            LocalDatabase.CreateDatabase();
        }


        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Windows.Web.Http.HttpClient webClient = new Windows.Web.Http.HttpClient();
            try
            {
                var result = await webClient.GetAsync(new Uri(UrlInput.Text));
                ResultOutput.Text = result.Content.ToString();
                App.appSettings.Url = UrlInput.Text;
                App.appSettings.Method = comboBox.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                ResultOutput.Text = ex.ToString();
            }
        }

        private void cbWrap_Checked(object sender, RoutedEventArgs e)
        {
            ResultOutput.TextWrapping = TextWrapping.Wrap;
        }

        private void cbWrap_Unchecked(object sender, RoutedEventArgs e)
        {
            ResultOutput.TextWrapping = TextWrapping.NoWrap;
        }
    }
}
