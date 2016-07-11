using IntelequiaWebhookLauncher.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace IntelequiaWebhookLauncher
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class WebhookEdit : Page
    {
        Frame rootFrame = Window.Current.Content as Frame;
        string webId = string.Empty;

        public WebhookEdit()
        {
            this.InitializeComponent();

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested +=
      App_BackRequested;


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Guid)
            {
                using (var db = new WebhookContext())
                {
                    var web = db.Webhooks.Single(x => x.Id == (Guid)e.Parameter);
                    webId = web.Id.ToString();
                    nameText.Text = web.Name;
                    urlText.Text = web.Url;
                    expireCalendarDatePicker.Date = Convert.ToDateTime(web.Expire == string.Empty ? DateTime.Now.ToString() : web.Expire);
                }

                base.OnNavigatedTo(e);
            }

            expireCalendarDatePicker.Date = DateTime.Now;
            if (rootFrame.CanGoBack)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
        }


        private void App_BackRequested(object sender,
Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            using (var db = new WebhookContext())
            {

                if (webId == string.Empty)
                {
                    var webhook = new Webhook { Name = nameText.Text, Url = urlText.Text, Expire = expireCalendarDatePicker.Date.ToString() };
                    db.Webhooks.Add(webhook);
                    db.SaveChanges();
                    this.Frame.GoBack();
                }
                else
                {
                    var oldWeb = db.Webhooks.Single(x => x.Id == new Guid(webId));
                    oldWeb.Name = nameText.Text;
                    oldWeb.Url = urlText.Text;
                    oldWeb.Expire = expireCalendarDatePicker.Date.ToString();
                    db.SaveChanges();
                    this.Frame.GoBack();
                }


            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            using (var db = new WebhookContext())
            {
                if (webId != string.Empty)
                {

                    var deleteWeb = db.Webhooks.Single(x => x.Id == new Guid(webId));
                    db.Webhooks.Remove(deleteWeb);
                    db.SaveChanges();
                    this.Frame.GoBack();
                }

            }
        } 
    }
}
