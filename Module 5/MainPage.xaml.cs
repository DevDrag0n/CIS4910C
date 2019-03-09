using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Windows.Storage;
using System.Net.Http;
using Newtonsoft.Json;


using System.ComponentModel;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CIS4910C_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }




        private void btnShowPane_Click(object sender, RoutedEventArgs e)
        {
            if (MySplitview.IsPaneOpen == false)
            {
                MySplitview.IsPaneOpen = true;
                btnShowPane.Content = "\uE00F";
                btnShowPane.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                MySplitview.IsPaneOpen = false;
                btnShowPane.Content = "\uE00F";
                btnShowPane.HorizontalAlignment = HorizontalAlignment.Left;

            }

        }


        public int IsAuth { get; set; }

        //[DataTable("User_Cred")]
        public class User_Cred
        {
            public string id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }

        }



       private IMobileServiceSyncTable<User_Cred> todoGetTable = App.MobileService.GetSyncTable<User_Cred>();



        private async Task InitLocalStoreAsync()
        {
            if (!App.MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("CIS4910C-DB");
                store.DefineTable<User_Cred>();
                await App.MobileService.SyncContext.InitializeAsync(store);
            }
            await SyncAsync();
        }

        private async Task SyncAsync()
        {
            await App.MobileService.SyncContext.PushAsync();
            await todoGetTable.PullAsync("User_Cred", todoGetTable.CreateQuery());
        }



        async public void submitAuthBtn_Click(object sender, RoutedEventArgs e)
        {
            await InitLocalStoreAsync();
            
            GetAuthentication();

        }

        async public void GetAuthentication()
        {
            try
            {

                //IMobileServiceTable<User_Cred> todoTable = App.MobileService.GetTable<User_Cred>();

                List<User_Cred> items = await todoGetTable
                    .Where(User_Cred => User_Cred.Username == textBoxUsername.Text)
                    .ToListAsync();

                IsAuth = items.Count();



                // Return a List UI control value back to the form

                foreach (var value in items)
                {
                    
                    var dialog = new MessageDialog("Welcome Back  " + value.Username);
                    await dialog.ShowAsync();
                    this.Frame.Navigate(typeof(home));
                }


                if (IsAuth > 0)
                {
                    var dialog = new MessageDialog("You are Authenticated");
                    await dialog.ShowAsync();


                }
                else
                {
                    var dialog = new MessageDialog(" Account Does Not Exist, please Register to get Started.");
                    await dialog.ShowAsync();
                }
            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        async private void submitAuthBtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User_Cred itemReg = new User_Cred
                {
                    Username = textBoxUsername.Text,
                    Password = textBoxPassword.Text

                };
                await App.MobileService.GetTable<User_Cred>().InsertAsync(itemReg);
                var dialog = new MessageDialog("Thank you for Registering! Now just hit log in");
                await dialog.ShowAsync();

            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " +em.Message);
    
                await dialog.ShowAsync();
            }
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }
    }
}

