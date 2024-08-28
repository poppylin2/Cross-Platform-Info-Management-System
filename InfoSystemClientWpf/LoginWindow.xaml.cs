using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Threading;
using HslCommunication;
using CommonLibrary;
using HslCommunication.BasicFramework;
using Newtonsoft.Json.Linq;
using ClientsLibrary;
using MaterialDesignThemes.Wpf;
using System.IO;

namespace 软件系统客户端Wpf
{
    /// <summary>
    /// logic of LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region Constructor
        
        public LoginWindow()
        {
            InitializeComponent();

            // Load local data
            UserClient.JsonSettings.FileSavePath = AppDomain.CurrentDomain.BaseDirectory + @"JsonSettings.txt";
            UserClient.JsonSettings.LoadByFile();
        }

        #endregion

        #region Window Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowToolTip.Opacity = 0;

            TextBlockSoftName.Text = SoftResources.StringResouce.SoftName;
            TextBlockSoftVersion.Text = UserClient.CurrentVersion.ToString();
            TextBlockSoftCopyright.Text = $"Copyright by{CommonLibrary.SoftResources.StringResouce.SoftCopyRight}";


            // Clear account and password if the last login was more than 7 days ago
            if ((DateTime.Now - UserClient.JsonSettings.LoginTime).TotalDays < UserClient.JsonSettings.PasswordOverdueDays)
            {
                // Load saved data
                NameTextBox.Text = UserClient.JsonSettings.LoginName ?? "";
                PasswordBox.Password = UserClient.JsonSettings.Password ?? "";
                Remember.IsChecked = UserClient.JsonSettings.Password != "";
            }

            // Initialize input focus
            if (UserClient.JsonSettings.Password != "") LoginButton.Focus();
            else if (UserClient.JsonSettings.LoginName != "") PasswordBox.Focus();
            else NameTextBox.Focus();


            // Load the previously saved theme color scheme
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Palette.txt"))
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"Palette.txt", Encoding.UTF8))
                {
                    string temp = sr.ReadToEnd();
                    MaterialDesignThemes.Wpf.Palette obj = JObject.Parse(temp).ToObject<MaterialDesignThemes.Wpf.Palette>();
                    new PaletteHelper().ReplacePalette(obj);
                }
            }
        }

        #endregion

        #region Login Click


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                SetInformationString("Enter the username");
                NameTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                SetInformationString("Enter the password");
                PasswordBox.Focus();
                return;
            }

            SetInformationString("Maintenance status is being verified...");
            UISettings(false);

            UserName = NameTextBox.Text;
            UserPassword = PasswordBox.Password;
            IsChecked = (bool)Remember.IsChecked;

            ThreadAccountLogin = new Thread(ThreadCheckAccount);
            ThreadAccountLogin.IsBackground = true;
            ThreadAccountLogin.Start();
        }


        #endregion

        #region 账户验证的逻辑块

        private string UserName = string.Empty;
        private string UserPassword = string.Empty;
        private bool IsChecked = false;

        /// <summary>
        /// Background thread for account validation
        /// </summary>
        private Thread ThreadAccountLogin = null;
        /// <summary>
        /// Backend for user account validation
        /// </summary>
        private void ThreadCheckAccount()
        {
            //Define delegate
            Action<string> message_show = delegate (string message)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    SetInformationString(message);
                }));
            };
            Action start_update = delegate
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    
                    string update_file_name = AppDomain.CurrentDomain.BaseDirectory + @"软件自动更新.exe";
                    try
                    {
                        System.Diagnostics.Process.Start(update_file_name);
                        Environment.Exit(0);
                    }
                    catch
                    {
                        MessageBox.Show("Update program failed to start. Please check if the file is missing and contact the administrator for further assistance.");
                    }
                }));
            };
            Action thread_finish = delegate
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    UISettings(true);
                }));
            };


            // Start password validation
            if (AccountLogin.AccountLoginServer(
                message_show,
                start_update,
                thread_finish,
                UserName,
                UserPassword,
                IsChecked,
                "wpf"))
            {
                // Launch the main window if validation is successful
                Dispatcher.Invoke(new Action(() =>
                {
                    DialogResult = true;
                    return;
                }));
            }
            
        }


        #endregion

        #region User Interface


        private void UISettings(bool enable)
        {
            NameTextBox.IsEnabled = enable;
            PasswordBox.IsEnabled = enable;
            Remember.IsEnabled = enable;
            LoginButton.IsEnabled = enable;
        }

        private void SetInformationString(string str)
        {
            if (WindowToolTip.Opacity == 1)
            {
                DoubleAnimation hidden = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));
                hidden.Completed += delegate
                {
                    DoubleAnimation show = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(100));
                    WindowToolTip.Text = str;
                    WindowToolTip.BeginAnimation(OpacityProperty, show);
                };
                WindowToolTip.BeginAnimation(OpacityProperty, hidden);
            }
            else
            {
                DoubleAnimation show = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(100));
                WindowToolTip.Text = str;
                WindowToolTip.BeginAnimation(OpacityProperty, show);
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            LoginButton.Focus();
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            LoginButton.Focus();
        }



        private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)  PasswordBox.Focus();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Button_Click(null, new RoutedEventArgs());
        }


        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using (FormShowMachineId form = new FormShowMachineId())
            {
                form.ShowDialog();
            }
        }



        #endregion

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
