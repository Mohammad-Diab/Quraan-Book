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
    /// Interaction logic for Fonts.xaml
    /// </summary>
    public partial class Fonts : Window
    {
        public Fonts()
        {
            InitializeComponent();
        }

        string[] BrushesNames = { "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue", "BlueViolet", "Brown", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk", "Crimson", "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenrod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange", "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray", "DodgerBlue", "Firebrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro", "GhostWhite", "Gold", "Goldenrod", "Gray", "Green", "GreenYellow", "Honeydew", "HotPink", "IndianRed", "Indigo", "Ivory", "Khaki", "Lavender", "LavenderBlush", "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenrodYellow", "LightGray", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen", "LightSkyBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", "Maroon", "MediumAquamarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", "MidnightBlue", "MintCream", "MistyRose", "Moccasin", "NavajoWhite", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", "OrangeRed", "Orchid", "PaleGoldenrod", "PaleGreen", "PaleTurquoise", "PaleVioletRed", "PapayaWhip", "PeachPuff", "Peru", "Pink", "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown", "SeaGreen", "SeaShell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen", "SteelBlue", "Tan", "Teal", "Thistle", "Tomato", "Turquoise", "Violet", "Wheat", "White", "WhiteSmoke", "Yellow", "YellowGreen" };
        private void Save_Setting(object sender, RoutedEventArgs e)
        {
            MainWindow.Font_Name = System.Drawing.FontFamily.Families[FontsNames.SelectedIndex].Name;
            MainWindow.Font_Name_Index = FontsNames.SelectedIndex;
            MainWindow.Font_Size = FontSizes.Value;
            MainWindow.Font_Color = FontColors.SelectedIndex;
            MainWindow.italic = Italic.IsChecked;
            MainWindow.bold = Bold.IsChecked;
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            foreach (var i in System.Drawing.FontFamily.Families)
            {
                ListBoxItem tmp = new ListBoxItem();
                tmp.Content = i.Name +  "  (أبجد هوز حطي كلمن سعفص قرشت ثخذ ضظغ 0123456789)";
                tmp.FontFamily = new FontFamily(i.Name);
                tmp.FontSize = 14;
                FontsNames.Items.Add(tmp);
            }
            FontsNames.SelectedIndex = MainWindow.Font_Name_Index;
            if (FontsNames.SelectedIndex == -1)
                MainWindow.Font_Name_Index = FontsNames.SelectedIndex = 0;
            FontsNames.ScrollIntoView(FontsNames.Items[FontsNames.SelectedIndex]);
            for (int i = 0; i < BrushesNames.Length; i++)
            {
                ListBoxItem tmp = new ListBoxItem();
                tmp.Foreground = MainWindow.WMBrushes[i];
                tmp.Content = BrushesNames[i];
                FontColors.Items.Add(tmp);
            }
            FontColors.SelectedIndex = MainWindow.Font_Color;
            FontColors.ScrollIntoView(FontColors.Items[FontColors.SelectedIndex]);
            FontSizes.Value = MainWindow.Font_Size;
            TextTest.FontSize = FontSizes.Value;
            Bold.IsChecked = MainWindow.bold;
            Italic.IsChecked = MainWindow.italic;
        }

        private void FontSizes_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TextTest != null)
                TextTest.FontSize = e.NewValue;
        }

        private void FontsNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextTest.FontFamily = new System.Windows.Media.FontFamily(System.Drawing.FontFamily.Families[FontsNames.SelectedIndex].Name);
        }

        private void FontColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextTest.Foreground = MainWindow.WMBrushes[FontColors.SelectedIndex];
        }

        private void Window_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
