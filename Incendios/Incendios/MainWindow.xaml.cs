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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Incendios
{

    public partial class MainWindow : Window
    {

        private Manager mgr;
        private MusicManager MusicMgr;
        private IAudioFilles audioFiles;

        private KeyEventHandler AnyKeyHandler;

        public MainWindow()
        {
            InitializeComponent();
            mgr = new Manager(this);
            MusicMgr = new MusicManager();
            audioFiles = new IAudioFilles();


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
            mgr.ShowOptionSelector();
        }
    }
}
