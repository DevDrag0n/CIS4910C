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

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Storage;
using System.Net.Http;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CIS4910C_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Archive : Page
    {
        public Archive()
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

        IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();
        MobileServiceCollection<TodoItem, TodoItem> items;




        public class TodoItem
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public bool Complete { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ISBN { get; set; }
            public string PublicationDate { get; set; }


        }

        


        async private void Submit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                TodoItem item = new TodoItem
                {
                    Title = txtBxTitle.Text,
                    FirstName = txtBxFName.Text,
                    LastName = txtBxLName.Text,
                    ISBN = txtBxISBN.Text,
                    PublicationDate = txtBxPubDate.Text,
                    Complete = false
                };
                await App.MobileService.GetTable<TodoItem>().InsertAsync(item);
                var dialog = new MessageDialog("Successful!");
                await dialog.ShowAsync();
            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        

       

        private async Task RefreshISBN2()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts the isbn in descending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderByDescending(todoItem => todoItem.ISBN)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.IsbnSort2.IsEnabled = true;
            }
        }

        private async Task RefreshISBN()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts the isbn in ascending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderBy(todoItem => todoItem.ISBN)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.IsbnSort.IsEnabled = true;
            }
        }



        private async Task RefreshAuthLName2()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts the by authors Last name in Descending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderByDescending(todoItem => todoItem.LastName)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.AuthLSort2.IsEnabled = true;
            }
        }

        private async Task RefreshAuthLName()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts the by authors Last name in ascending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderBy(todoItem => todoItem.LastName)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.AuthLSort.IsEnabled = true;
            }
        }


        private async Task RefreshAuthFName2()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts by author name in descending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderByDescending(todoItem => todoItem.FirstName)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.AuthFSort2.IsEnabled = true;
            }
        }

        private async Task RefreshAuthFName()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts the by authors first name in ascending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderBy(todoItem => todoItem.FirstName)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.AuthFSort.IsEnabled = true;
            }
        }

        private async Task RefreshTitleItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sort by title name in ascending order in the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderBy(todoItem => todoItem.Title)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.TitleSort.IsEnabled = true;
            }
        }

        private async Task RefreshTitleItems2()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code sorts the title in descending order with entries from the todoitem table
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .OrderByDescending(todoItem => todoItem.Title)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.TitleSort2.IsEnabled = true;
            }
        }

        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(TodoItem => TodoItem.Complete == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.btnRefresh.IsEnabled = true;
            }
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;
            await UpdateCheckedTodoItem(item);
        }

        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await todoTable.UpdateAsync(item);
            items.Remove(item);
            ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

            //await SyncAsync(); // offline sync
        }


        async private void ISBNSort_Click_1(object sender, RoutedEventArgs e)
        {
            await RefreshISBN();
        }

        async private void ISBNSort_Click_2(object sender, RoutedEventArgs e)
        {
            await RefreshISBN2();
        }



       

        async private void AuthLstSort_Click_1(object sender, RoutedEventArgs e)
        {
            await RefreshAuthLName();
        }

        async private void AuthLstSort_Click_2(object sender, RoutedEventArgs e)
        {
            await RefreshAuthLName2();
        }

        async private void AuthFstSort_Click_1(object sender, RoutedEventArgs e)
        {
            await RefreshAuthFName();
        }

        async private void AuthFstSort_Click_2(object sender, RoutedEventArgs e)
        {
            await RefreshAuthFName2();
        }

        async private void TitleSort_Click_1(object sender, RoutedEventArgs e)
        {
            await RefreshTitleItems();
        }

        async private void TitleSort_Click_2(object sender, RoutedEventArgs e)
        {
            await RefreshTitleItems2();
        }

        async private void btnRefresh_Click_1(object sender, RoutedEventArgs e)
        {
            await RefreshTodoItems();
        }

        private void TxtBoxItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(home));
        }

        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Archive));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Signoff));
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));

        }
    }
}
