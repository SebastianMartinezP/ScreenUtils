using HandyControl.Controls;
using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
namespace ScreenUtils
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupInitialValues();
        }


        public void SetupInitialValues()
        {
            try
            {
                Slider.Value = 0;
                SliderLabel.Content = 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PreviewSlider slider = (PreviewSlider)sender;
            SliderLabel.Content = slider.Value;
            SetNightLight((int)slider.Value);
        }

        private void ToggleHDR_Checked(object sender, RoutedEventArgs e) => SetHDR(1);
        private void ToggleHDR_Unchecked(object sender, RoutedEventArgs e) => SetHDR(0);


        public void SetNightLight(int ammount)
        {
            try
            {
                // Open the registry key for the Night Light settings
                
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\CloudStore\Store\DefaultAccount\Current\default$windows.data.bluelightreduction.bluelightreductionstate\windows.data.bluelightreduction.bluelightreductionstate", true))
                {
                    // Set the new Night Light value
                    key.SetValue("data", ammount, RegistryValueKind.DWord);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SetHDR(int HDR)
        {
            try
            {
                // Set the desired HDR value (0 = off, 1 = on)
                int value = 1;

                // Open the registry key for the HDR settings
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager", true);

                // Set the new HDR value
                key.SetValue("EnableHDR", value, RegistryValueKind.DWord);

                // Close the registry key
                key.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
