using System;
using System.Collections.Generic;
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
        private IAudioFiles audioFiles;

        private KeyEventHandler AnyKeyHandler;

        private string[,] table;

        public MainWindow()
        {
            InitializeComponent();
            mgr = new Manager(this);
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
            MusicMgr.Play(audioFiles.main);
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

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
