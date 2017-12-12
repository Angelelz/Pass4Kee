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
using Pass4Kee.Views;
using Pass4Kee.Utils;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pass4Kee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            bool IsIt = await Library.IsFileEmpty();
            
            if (IsIt)
            {
                Frame.Navigate(typeof(FirstTime));
            }
            else
            {
                
                string EncPass = await Library.ReadPassFromFile();
                if (await MicrosoftPassportHelper.CreatePassportKeyAsync("User"))
                {
                    
                    string MyPass = await Library.Decrypt(EncPass);
                    DataPackage MyPassData = new DataPackage();
                    MyPassData.SetText(MyPass);
                    Clipboard.SetContent(MyPassData);
                    Application.Current.Exit();

                }
                else
                {
                    ErrorMessage.Text = "Click Here to Set New Password";

                    //Application.Current.Exit();
                }
            }
        }

        private void DeletePass_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Library.SavePassToFile("");
            Frame.Navigate(typeof(FirstTime));
        }

    }
}
