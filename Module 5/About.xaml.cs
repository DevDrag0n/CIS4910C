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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CIS4910C_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();
        }
        public void btnShowPane_Click(object sender, RoutedEventArgs e)
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
        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Signoff));
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(home));
        }

        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Archive));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));

        }



    }
}
