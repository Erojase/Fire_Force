using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace Incendios
{
    class Manager
    {

        private MainWindow main;

        public Manager(MainWindow window)
        {
            main = window;
        }

        public void HideAll()
        {
            main.Title.Visibility = Visibility.Collapsed;
            main.GameGrid.Visibility = Visibility.Collapsed;
            main.SimPrep.Visibility = Visibility.Collapsed;
        }

        public void ShowGrid()
        {
            main.GameGrid.Visibility = Visibility.Visible;
        }

        public void ShowSimPrep()
        {
            main.SimPrep.Visibility = Visibility.Visible;
        }
    }
}
