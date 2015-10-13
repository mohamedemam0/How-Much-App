using How_Much_App.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace How_Much_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        MainPageViewModel viewModel = new MainPageViewModel();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            viewModel.LoadData(HistoryList);
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.InsertData(NametxtBox.Text, PricetxtBox.Text);
            Task.Delay(2000);
            viewModel.LoadData(HistoryList);
            NametxtBox.Text = "";
            PricetxtBox.Text = "";
        }

        private async void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("This will clear all of your data, confirm?", "Confirm Clear");
            var okBtn = new UICommand("OK");
            var cancelBtn = new UICommand("Cancel");
            msg.Commands.Add(okBtn);
            msg.Commands.Add(cancelBtn);
            IUICommand result = await msg.ShowAsync();

            if (result != null && result.Label == "OK")
            {
                viewModel.ClearData();
                HistoryList.ItemsSource = null;
                NametxtBox.Text = "";
                PricetxtBox.Text = "";
            }
               
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LoadData(HistoryList);
        }

        private async void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Calculate();
            }
            catch (Exception ex)
            {
               await new MessageDialog(ex.Message).ShowAsync();
            }
            
        }

        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryList.SelectedItems.Count > 0)
            {
                HistoryList.SelectionMode = ListViewSelectionMode.Multiple;
            }
            else
            {
                HistoryList.SelectionMode = ListViewSelectionMode.Single;
            }
        }

        private void HistoryList_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }
    }
}
