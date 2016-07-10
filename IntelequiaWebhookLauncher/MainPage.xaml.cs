using IntelequiaWebhookLauncher.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IntelequiaWebhookLauncher
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new WebhookContext())
            {
                Webhooks.ItemsSource = db.Webhooks.ToList();
                
            }
        }

        private void NewWebhook_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WebhookEdit));
            //using (var db = new WebhookContext())
            //{
            //    var webhook = new Webhook {Url ="a" };
            //    db.Webhooks.Add(webhook);
            //    db.SaveChanges();

            //    Webhooks.ItemsSource = db.Webhooks.ToList();
            //}
        }

        private async void Launch(object sender, RoutedEventArgs e)
        {
            Webhook web = (Webhook)(sender as Button).DataContext;
            Grid webPanel = (Grid)((StackPanel) (sender as Button).Parent).Parent;

            Uri requestUri = new Uri(web.Url);

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
           
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                if(httpResponse.StatusCode == Windows.Web.Http.HttpStatusCode.Ok)
                {
                    webPanel.Background = new SolidColorBrush(Colors.LimeGreen);
                    
                    await Task.Delay(3000);
                    webPanel.Background = null;
                }
                else
                {
                    webPanel.Background = new SolidColorBrush(Colors.Red);
                    await Task.Delay(3000);
                    webPanel.Background = null;
                }
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                webPanel.Background = new SolidColorBrush(Colors.Red);
                await Task.Delay(3000);
                webPanel.Background = null;
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            Webhook web = (Webhook)(sender as Button).DataContext;
            this.Frame.Navigate(typeof(WebhookEdit), web.Id);
        }
    }
}
