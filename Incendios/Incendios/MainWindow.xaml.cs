using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Incendios
{

    public partial class MainWindow : Window
    {

        private Manager mgr;
        private MusicManager MusicMgr;
        private SettingsManager config;
        private DatabaseManager dbManager;
        private IAudioFiles audioFiles;

        private KeyEventHandler AnyKeyHandler;

        private string[,] table;

        public MainWindow()
        {
            InitializeComponent();
            mgr = new Manager(this);
            config = new SettingsManager();
            config.LoadConfig();

            dbManager = new DatabaseManager();

            MusicMgr = new MusicManager();
            audioFiles = new IAudioFiles();

            WindowState = WindowState.Maximized;

            AnyKeyHandler =  new KeyEventHandler(MainWindow_KeyDown);
            KeyDown += AnyKeyHandler;

        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            AnyKey.Visibility = Visibility.Collapsed;
            Opts.Visibility = Visibility.Visible;
            MusicMgr.Play(audioFiles.IntToSong(config.Song));
            KeyDown -= AnyKeyHandler;
        }

        private void btnComenzar_Click(object sender, RoutedEventArgs e)
        {
            Opts.Visibility = Visibility.Collapsed;
            Login.Visibility = Visibility.Visible;
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            MusicMgr.Mute();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();
            mgr.ShowLogin();
        }

        private void btnCreditos_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();
            mgr.ShowCredits();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnPrivate_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();
            mgr.ShowSimPrep();
        }

        private void EnableControls()
        {
            btnPlay.IsEnabled = true;
            TogglePause.IsEnabled = true;
            AutoPlay.IsEnabled = true;
            Step.IsEnabled = true;
        }

        private void ClearGrid()
        {
            Preview.Children.Clear();
            Preview.ColumnDefinitions.Clear();
            Preview.RowDefinitions.Clear();
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();

            table = mgr.GenerateGrid((int)RowNum.Value, (int)ColNum.Value);

            tableToGrid(table);
            EnableControls();
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();

            table = mgr.GenerateGrid((int)RowNum.Value, (int)ColNum.Value);
            table = mgr.RandomizeTable(table);
            tableToGrid(table);
            EnableControls();
        }

        private void tableToGrid(string[,] table)
        {
            int rowsOrHeight = table.GetLength(0);
            int colsOrWidth = table.GetLength(1);
            for (int i = 0; i < rowsOrHeight; i++)
            {
                RowDefinition tmpRow = new RowDefinition();
                Preview.RowDefinitions.Add(tmpRow);
            }
            for (int j = 0; j < colsOrWidth; j++)
            {
                ColumnDefinition tmpCol = new ColumnDefinition();
                Preview.ColumnDefinitions.Add(tmpCol);
            }
            string pictName = "";
            for (int i = 0; i < rowsOrHeight; i++)
            {
                for (int j = 0; j < colsOrWidth; j++)
                {
                    if (table[i, j] == "o")
                    {
                        pictName = "waste";
                    }
                    else if (table[i, j] == "g")
                    {
                        pictName = "weed";
                    }
                    else if (table[i, j] == "t")
                    {
                        pictName = "tree";
                    }
                    else if (table[i, j] == "w")
                    {
                        pictName = "water";
                    }
                    else if (table[i, j] == "f")
                    {
                        pictName = "fire";
                    }
                    Image tmpImg = new Image();

                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri("pack://application:,,,/Images/" + pictName + ".png");
                    logo.EndInit();

                    tmpImg.Source = logo;
                    Grid.SetRow(tmpImg, i);
                    Grid.SetColumn(tmpImg, j);
                    tmpImg.HorizontalAlignment = HorizontalAlignment.Stretch;
                    tmpImg.VerticalAlignment = VerticalAlignment.Stretch;
                    Preview.Children.Add(tmpImg);
                }
            }
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();
            table = mgr.CalculateNext(table, (int)BurnProb.Value, (int)TreeProb.Value);
            tableToGrid(table);
        }

        private void AutoPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToMenu_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();
            mgr.ShowMain();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MusicMgr.Play(audioFiles.IntToSong(SongList.SelectedIndex));
            
        }

        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();
            mgr.ShowMain();
        }

        private void Save_Leave_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();
            config.SaveSettings(Resolutions.SelectedIndex, Difficulty.SelectedIndex, VolumeControl.Value, SongList.SelectedIndex);
            mgr.ShowMain();
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
            mgr.HideAll();

            Resolutions.SelectedIndex = config.Resolution;
            Difficulty.SelectedIndex = config.Difficulty;
            VolumeControl.Value = config.Volume;
            SongList.SelectedIndex = config.Song;

            mgr.ShowConfig();
        }

        private void log_username_Enter(object sender, RoutedEventArgs e)
        {
            if (log_username.Text == "Input user name")
            {
                log_username.Foreground = new SolidColorBrush(Colors.Black);
                log_username.BorderThickness = new Thickness(0, 0, 0, 0);
                log_username.Text = "";
            }
        }
        private void log_username_Exit(object sender, RoutedEventArgs e)
        {
            if (log_username.Text == "")
            {
                log_username.Foreground = new SolidColorBrush(Colors.Gray);
                log_username.BorderThickness = new Thickness(1, 1, 1, 1);
                log_username.Text = "Input user name";
            }
        }

        private void SubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = log_username.Text;
            string password = log_password.Password;

            string result = dbManager.Login(user, password);
            if (result != "0")
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
            else
            {
                mgr.HideAll();
                mgr.ShowSimPrep();
            }
            log_password.Password = "";
            log_username.Text = "";
        }

        private void reg_username_Enter(object sender, RoutedEventArgs e)
        {
            if (reg_username.Text == "Input user name")
            {
                reg_username.Foreground = new SolidColorBrush(Colors.Black);
                reg_username.BorderThickness = new Thickness(0, 0, 0, 0);
                reg_username.Text = "";
            }
        }
        private void reg_username_Exit(object sender, RoutedEventArgs e)
        {
            if (reg_username.Text == "")
            {
                reg_username.Foreground = new SolidColorBrush(Colors.Gray);
                reg_username.BorderThickness = new Thickness(1, 1, 1, 1);
                reg_username.Text = "Input user name";
            }
        }

        private void reg_mail_Enter(object sender, RoutedEventArgs e)
        {
            if (reg_mail.Text == "Input mail")
            {
                reg_mail.Foreground = new SolidColorBrush(Colors.Black);
                reg_mail.BorderThickness = new Thickness(0, 0, 0, 0);
                reg_mail.Text = "";
            }
        }
        private void reg_mail_Exit(object sender, RoutedEventArgs e)
        {
            if (reg_mail.Text == "")
            {
                reg_mail.Foreground = new SolidColorBrush(Colors.Gray);
                reg_mail.BorderThickness = new Thickness(1, 1, 1, 1);
                reg_mail.Text = "Input mail";
            }
        }

        private void SubmitRegister_Click(object sender, RoutedEventArgs e)
        {
            string user = reg_username.Text;
            string password = reg_password.Password;
            string mail = reg_mail.Text;

            if (password != reg_repPassword.Password)
            {
                MessageBox.Show("Las contraseñas introducidas no coinciden");
                return;
            }

            string result = dbManager.Register(user, password, mail);
            if (result != "0")
            {
                MessageBox.Show("No se ha podido registrar");
            }
            else
            {
                RegisterPanel.Visibility = Visibility.Collapsed;
                LoginPanel.Visibility = Visibility.Visible;
            }
            reg_password.Password = "";
            reg_username.Text = "";
        }

        private void VolumeControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MusicMgr.Volume(((double)VolumeControl.Value)/100);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void toLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }
        private void toRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }
    }
}
