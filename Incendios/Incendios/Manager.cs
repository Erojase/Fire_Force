using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.IO;

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
            main.SimPrep.Visibility = Visibility.Collapsed;
            main.Config.Visibility = Visibility.Collapsed;
            main.Login_Register.Visibility = Visibility.Collapsed;
        }

        public void ShowSimPrep()
        {
            main.SimPrep.Visibility = Visibility.Visible;
        }
        public void ShowConfig()
        {
            main.Config.Visibility = Visibility.Visible;
        }
        public void ShowMain()
        {
            main.Title.Visibility = Visibility.Visible;
        }
        public void ShowLogin()
        {
            main.Login_Register.Visibility = Visibility.Visible;
        }

        public string[,] GenerateGrid(int RowNum, int ColNum)
        {
            string[,] table = new string[RowNum, ColNum];
            int rowsOrHeight = table.GetLength(0);
            int colsOrWidth = table.GetLength(1);
            Console.WriteLine(rowsOrHeight);
            Console.WriteLine(colsOrWidth);
            for (int i = 0; i < rowsOrHeight; i++)
            {
                for (int j = 0; j < colsOrWidth; j++)
                {
                    table[i, j] = "o";
                }
            }
            return table;
        }

        public string[,] RandomizeTable(string[,] table)
        {
            Random r = new Random();
            int rowsOrHeight = table.GetLength(0);
            int colsOrWidth = table.GetLength(1);
            for (int i = 0; i < rowsOrHeight; i++)
            {
                for (int j = 0; j < colsOrWidth; j++)
                {
                    switch (r.Next(0, 6))
                    {
                        case 0:
                            table[i, j] = "o";
                            break;
                        case 1:
                            table[i, j] = "g";
                            break;
                        case 2:
                            table[i, j] = "t";
                            break;
                        case 3:
                            table[i, j] = "o";
                            break;
                        case 4:
                            table[i, j] = "w";
                            break;
                        case 5:
                            table[i, j] = "g";
                            break;
                    }
                    
                }
            }
            return table;
        }
        /*
           fertil   ->  g
           arbol    ->  t
           fuego    ->  f
           infertil ->  o
           agua     ->  w
         */


        public string[,] CalculateNext(string[,] table, int fireProb, int treeProb)
        {
            string[,] NewTable = table;
            Random r = new Random();

            int rowsOrHeight = table.GetLength(0);
            int colsOrWidth = table.GetLength(1);
            for (int i = 0; i < rowsOrHeight; i++)
            {
                for (int j = 0; j < colsOrWidth; j++)
                {
                    if (table[i,j] == "g")
                    {

                    }
                    else if (table[i, j] == "t")
                    {
                        NewTable[i, j] = testTree(table, i, j, rowsOrHeight-1, colsOrWidth-1);
                    }
                    else if (table[i, j] == "f")
                    {
                        NewTable[i, j] = testWater(table, i, j, rowsOrHeight - 1, colsOrWidth - 1);
                    }
                    else if (table[i, j] == "o")
                    {
                        NewTable[i, j] = "g";
                    }

                    if (r.Next(fireProb, 50) == 10 && table[i, j] == "t" && fireProb != 0)
                    {
                        NewTable[i, j] = "f";
                    }
                    if (r.Next(treeProb, 50) == 10 && table[i, j] == "g" && treeProb != 0)
                    {
                        NewTable[i, j] = "t";
                    }
                }
            }




            return NewTable;
        }


        private string testTree(string[,] table, int i, int j, int iMax, int jMax)
        {
            if (i == 0 && j == 0)
            {
                if (table[i + 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j + 1] == "f")
                {
                    return "f";
                }
            }
            else if (i == 0 && j != jMax)
            {
                if (table[i + 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j + 1] == "f")
                {
                    return "f";
                }
                else if (table[i, j - 1] == "f")
                {
                    return "f";
                }
            }
            else if (j == 0 && i != iMax)
            {
                if (table[i + 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j + 1] == "f")
                {
                    return "f";
                }
                else if (table[i - 1, j] == "f")
                {
                    return "f";
                }
            }
            else if (i == iMax && j == 0)
            {
                if (table[i - 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j + 1] == "f")
                {
                    return "f";
                }
            }
            else if (i == iMax && j != jMax)
            {
                if (table[i, j - 1] == "f")
                {
                    return "f";
                }
                else if (table[i, j + 1] == "f")
                {
                    return "f";
                }
                else if (table[i - 1, j] == "f")
                {
                    return "f";
                }
            }
            else if (i == 0 && j == jMax)
            {
                if (table[i + 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j - 1] == "f")
                {
                    return "f";
                }
            }
            else if (j == jMax && i != iMax)
            {
                if (table[i, j - 1] == "f")
                {
                    return "f";
                }
                else if (table[i + 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i - 1, j] == "f")
                {
                    return "f";
                }
            }
            else if (i == iMax && j == jMax)
            {
                if (table[i - 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j - 1] == "f")
                {
                    return "f";
                }
            }
            else
            {
                if (table[i, j - 1] == "f")
                {
                    return "f";
                }
                else if (table[i + 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i - 1, j] == "f")
                {
                    return "f";
                }
                else if (table[i, j + 1] == "f")
                {
                    return "f";
                }
            }

            return "t";
        }

        private string testWater(string[,] table, int i, int j, int iMax, int jMax)
        {
            if (i == 0 && j == 0)
            {
                if (table[i + 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j + 1] == "w")
                {
                    return "g";
                }
            }
            else if (i == 0 && j != jMax)
            {
                if (table[i + 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j + 1] == "w")
                {
                    return "g";
                }
                else if (table[i, j - 1] == "w")
                {
                    return "g";
                }
            }
            else if (j == 0 && i != iMax)
            {
                if (table[i + 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j + 1] == "w")
                {
                    return "g";
                }
                else if (table[i - 1, j] == "w")
                {
                    return "g";
                }
            }
            else if (i == iMax && j == 0)
            {
                if (table[i - 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j + 1] == "w")
                {
                    return "g";
                }
            }
            else if (i == iMax && j != jMax)
            {
                if (table[i, j - 1] == "w")
                {
                    return "g";
                }
                else if (table[i, j + 1] == "w")
                {
                    return "g";
                }
                else if (table[i - 1, j] == "w")
                {
                    return "g";
                }
            }
            else if (i == 0 && j == jMax)
            {
                if (table[i + 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j - 1] == "w")
                {
                    return "g";
                }
            }
            else if (j == jMax && i != iMax)
            {
                if (table[i, j - 1] == "w")
                {
                    return "g";
                }
                else if (table[i + 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i - 1, j] == "w")
                {
                    return "g";
                }
            }
            else if (i == iMax && j == jMax)
            {
                if (table[i - 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j - 1] == "w")
                {
                    return "g";
                }
            }
            else
            {
                if (table[i, j - 1] == "w")
                {
                    return "g";
                }
                else if (table[i + 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i - 1, j] == "w")
                {
                    return "g";
                }
                else if (table[i, j + 1] == "w")
                {
                    return "g";
                }
            }

            return "o";
        }

        public void LogTable(string[,] table)
        {
            int rowsOrHeight = table.GetLength(0);
            int colsOrWidth = table.GetLength(1);
            for (int i = 0; i < rowsOrHeight; i++)
            {
                for (int j = 0; j < colsOrWidth; j++)
                {
                    Console.Write(table[i,j] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
