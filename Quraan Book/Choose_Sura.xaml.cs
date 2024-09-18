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

namespace Quraan_Book
{
    /// <summary>
    /// Interaction logic for Choose_Sura.xaml
    /// </summary>
    public partial class Choose_Sura : Window
    {
        public Choose_Sura()
        {
            InitializeComponent();
        }

        private void Save_Setting(object sender, RoutedEventArgs e)
        {
            MainWindow.Sura_Index = ComboBox1.SelectedIndex;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 114; i++)
                ComboBox1.Items.Add(MainWindow.Sura_Name[i]);
            ComboBox1.SelectedIndex = MainWindow.Sura_Index;
            ComboBox1.SelectedIndex = MainWindow.Sura_Index;
            if (ComboBox1.SelectedIndex == -1)
                ComboBox1.SelectedIndex = 0;
            ComboBox1.Focus();
        }

        private void Window_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
