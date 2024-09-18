using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace Quraan_Book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Find find_window;
        List<int> indexes = new List<int>();
        int search_result = 0;
        public static string[][] Ayaat = new string[114][];
        public static string[] Sura_Name_With_Index = new string[114], Sura_Name = { "الفاتحة", "البقرة", "آل عمران", "النساء", "المائدة", "الأنعام", "الأعراف", "الأنفال", "التوبة", "يونس", "هود", "يوسف", "الرعد", "إبراهيم", "الحجر", "النحل", "الاسراء", "الكهف", "مريم", "طه", "الأنبياء", "الحج", "المؤمنون", "النور", "الفرقان", "الشعراء", "النمل", "القصص", "العنكبوت", "الروم", "لقمان", "السجدة", "الأحزاب", "سبأ", "فاطر", "يس", "الصافات", "ص", "الزمر", "غافر", "فصلت", "الشورى", "الزخرف", "الدخان", "الجاثية", "الأحقاف", "محمد", "الفتح", "الحجرات", "ق", "الذاريات", "الطور", "النجم", "القمر", "الرحمن", "الواقعة", "الحديد", "المجادلة", "الحشر", "الممتحنة", "الصف", "الجمعة", "المنافقون", "التغابن", "الطلاق", "التحريم", "الملك", "القلم", "الحاقة", "المعارج", "نوح", "الحن", "المزمل", "المدثر", "القيامة", "الإنسان", "المرسلات", "النبأ", "النازعات", "عبس", "التكوير", "الانفطار", "المطففين", "الانشقاق", "البروج", "الطارق", "الأعلى", "الغاشية", "الفجر", "البلد", "الشمس", "الليل", "الضحى", "الشرح", "التين", "العلق", "القدر", "البينة", "الزلزلة", "العاديات", "القارعة", "التكاثر", "العصر", "الهمزة", "الفيل", "قريش", "الماعون", "الكوثر", "الكافرون", "النصر", "المسد", "الاخلاص", "الفلق", "الناس" };
        public static string Font_Name, Sura = "";
        public static int Font_Color, Font_Name_Index = -1, Sura_Index = -1;
        public static double Font_Size;
        public static Brush[] WMBrushes = { Brushes.AliceBlue, Brushes.AntiqueWhite, Brushes.Aqua, Brushes.Aquamarine, Brushes.Azure, Brushes.Beige, Brushes.Bisque, Brushes.Black, Brushes.BlanchedAlmond, Brushes.Blue, Brushes.BlueViolet, Brushes.Brown, Brushes.BurlyWood, Brushes.CadetBlue, Brushes.Chartreuse, Brushes.Chocolate, Brushes.Coral, Brushes.CornflowerBlue, Brushes.Cornsilk, Brushes.Crimson, Brushes.Cyan, Brushes.DarkBlue, Brushes.DarkCyan, Brushes.DarkGoldenrod, Brushes.DarkGray, Brushes.DarkGreen, Brushes.DarkKhaki, Brushes.DarkMagenta, Brushes.DarkOliveGreen, Brushes.DarkOrange, Brushes.DarkOrchid, Brushes.DarkRed, Brushes.DarkSalmon, Brushes.DarkSeaGreen, Brushes.DarkSlateBlue, Brushes.DarkSlateGray, Brushes.DarkTurquoise, Brushes.DarkViolet, Brushes.DeepPink, Brushes.DeepSkyBlue, Brushes.DimGray, Brushes.DodgerBlue, Brushes.Firebrick, Brushes.FloralWhite, Brushes.ForestGreen, Brushes.Fuchsia, Brushes.Gainsboro, Brushes.GhostWhite, Brushes.Gold, Brushes.Goldenrod, Brushes.Gray, Brushes.Green, Brushes.GreenYellow, Brushes.Honeydew, Brushes.HotPink, Brushes.IndianRed, Brushes.Indigo, Brushes.Ivory, Brushes.Khaki, Brushes.Lavender, Brushes.LavenderBlush, Brushes.LawnGreen, Brushes.LemonChiffon, Brushes.LightBlue, Brushes.LightCoral, Brushes.LightCyan, Brushes.LightGoldenrodYellow, Brushes.LightGray, Brushes.LightGreen, Brushes.LightPink, Brushes.LightSalmon, Brushes.LightSeaGreen, Brushes.LightSkyBlue, Brushes.LightSlateGray, Brushes.LightSteelBlue, Brushes.LightYellow, Brushes.Lime, Brushes.LimeGreen, Brushes.Linen, Brushes.Magenta, Brushes.Maroon, Brushes.MediumAquamarine, Brushes.MediumBlue, Brushes.MediumOrchid, Brushes.MediumPurple, Brushes.MediumSeaGreen, Brushes.MediumSlateBlue, Brushes.MediumSpringGreen, Brushes.MediumTurquoise, Brushes.MediumVioletRed, Brushes.MidnightBlue, Brushes.MintCream, Brushes.MistyRose, Brushes.Moccasin, Brushes.NavajoWhite, Brushes.Navy, Brushes.OldLace, Brushes.Olive, Brushes.OliveDrab, Brushes.Orange, Brushes.OrangeRed, Brushes.Orchid, Brushes.PaleGoldenrod, Brushes.PaleGreen, Brushes.PaleTurquoise, Brushes.PaleVioletRed, Brushes.PapayaWhip, Brushes.PeachPuff, Brushes.Peru, Brushes.Pink, Brushes.Plum, Brushes.PowderBlue, Brushes.Purple, Brushes.Red, Brushes.RosyBrown, Brushes.RoyalBlue, Brushes.SaddleBrown, Brushes.Salmon, Brushes.SandyBrown, Brushes.SeaGreen, Brushes.SeaShell, Brushes.Sienna, Brushes.Silver, Brushes.SkyBlue, Brushes.SlateBlue, Brushes.SlateGray, Brushes.Snow, Brushes.SpringGreen, Brushes.SteelBlue, Brushes.Tan, Brushes.Teal, Brushes.Thistle, Brushes.Tomato, Brushes.Turquoise, Brushes.Violet, Brushes.Wheat, Brushes.White, Brushes.WhiteSmoke, Brushes.Yellow, Brushes.YellowGreen };
        public static bool? bold = false, italic = false;
        List<bool> DecodedList;
        public MainWindow()
        {
            InitializeComponent();
            Image b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/select.png"));
            select.Icon = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/find.png"));
            find.Icon = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/copy.png"));
            copy.Icon = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/select_font.png"));
            font.Icon = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/about.png"));
            about.Icon = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/rtf_viewer.png"));
            rtf_viewer.Icon = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/go-next.png"));
            go_next.Content = b;
            b = new Image();
            b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan Book;component/Images/go-previous.png"));
            go_pevious.Content = b;
            //b = new Image();
            //b.Source = new BitmapImage(new Uri("pack://application:,,,/Quraan_Book;component/Images/Find.png"));
            //help.Icon = b;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            bool done = false;
            string Quraan = String.Empty;
            for (int i = 0; i < Sura_Name.Length; i++)
                Sura_Name_With_Index[i] = String.Concat(i + 1, "- ", Sura_Name[i]);
            try
            {
                BinaryFormatter deserlizer = new BinaryFormatter();
                Stream Reader = File.OpenRead(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Quraan Book\\QurranFile.dat");
                Ayaat = (string[][])deserlizer.Deserialize(Reader);
                Reader.Close();
                done = true;
                if (!Ayaat[0][0].Contains("ب"))
                    done = false;
            }
            catch (Exception) { done = false; }
            try
            {
                if (!done)
                {
                    Quraan = Read_data_strucure();
                    var Sura = Quraan.Split('#');
                    for (int i = 0; i < 114; i++)
                        Ayaat[i] = Sura[i].Split('@');
                    Quraan = null;
                    for (int i = 0; i < 114; i++)
                        Sura[i] = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            try
            {
                Viewer.FontFamily = new FontFamily(new Uri("file://" + System.Environment.CurrentDirectory + "\\mry_KacstQurn.ttf"), "mry_KacstQurn");
                for (int i = 0; i < System.Drawing.FontFamily.Families.Length; i++)
                    if (System.Drawing.FontFamily.Families[i].Name == "mry_KacstQurn")
                    {
                        Font_Name_Index = i;
                        break;
                    }
            }
            catch (Exception ex1) { MessageBox.Show(ex1.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            if (Sura_Index < 1)
                go_pevious.IsEnabled = false;
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
                Help_Click(null, null);
            else if (e.Key == Key.F2)
                Choose_Click(null, null);
            else if (e.Key == Key.F3)
                Search.Focus();
            else if (e.Key == Key.F4)
                Copy_Click(null, null);
            else if (e.Key == Key.F5)
                Font_Click(null, null);
            else if (e.Key == Key.F6)
                rtf_viewer_Click(null, null);
        }

        private void Window_PreviewKeyDown_1(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            //{
            //    is_ctrl_down = true;
            //    return;
            //}
            //if (Keyboard.IsKeyDown(Key.F) && is_ctrl_down)
            //    Find_Click(null, null);
            //is_ctrl_down = false;
        }

        internal void ChangeSura()
        {
            Search.Text = "";
            Par.Inlines.Clear();
            Par.Inlines.Add(basmala);
            if (Sura_Index > -1)
            {
                if (Sura_Index < 1)
                    go_pevious.IsEnabled = false;
                else if (Sura_Index > 0)
                    go_pevious.IsEnabled = true;
                if (Sura_Index > 112)
                    go_next.IsEnabled = false;
                else if (Sura_Index < 113)
                    go_next.IsEnabled = true;
                if (Sura_Index == 8 || Sura_Index == 0)
                    basmala.Text = String.Empty;
                else
                    basmala.Text = "بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ" + "\n";
                StringBuilder tmp = new StringBuilder();
                for (int i = 0; i < Ayaat[Sura_Index].Length - 1; i++)
                {
                    tmp.Append(" " + Ayaat[Sura_Index][i] + "(" + (i + 1) + ")");
                    Par.Inlines.Add(new Run(" " + Ayaat[Sura_Index][i] + "(" + (i + 1) + ")"));
                }
                s_name.Text = "سورة: " + Sura_Name[Sura_Index];
                s_sort.Text = (Sura_Index + 1).ToString();
                s_aya_count.Text = "عدد آياتها: " + (Ayaat[Sura_Index].Length - 1);
                Sura = tmp.ToString();
                Viewer.ScrollToHome();
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            Choose_Sura window = new Choose_Sura();
            window.ShowDialog();
            ChangeSura();
        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            if (Font_Name == null)
            {
                for (int i = 0; i < WMBrushes.Length; i++)
                    if (WMBrushes[i] == Par.Foreground)
                    {
                        Font_Color = i;
                        break;
                    }
                Font_Size = 22;
                Font_Name = Viewer.FontFamily.ToString();
                bold = false;
                italic = false;
            }
            Fonts window = new Fonts();
            window.ShowDialog();
            Par.Foreground = WMBrushes[Font_Color];
            Viewer.FontSize = Font_Size;
            Viewer.FontFamily = new FontFamily(Font_Name);
            if (italic == true)
                Viewer.FontStyle = FontStyles.Italic;
            else
                Viewer.FontStyle = FontStyles.Normal;
            if (bold == true)
                Viewer.FontWeight = FontWeights.Bold;
            else
                Viewer.FontWeight = FontWeights.Regular;
        }

        private string Read_data_strucure()
        {
            int count;
            DecodedList = new List<bool>();
            List<UInt16> DecodedText = new List<UInt16>();
            StringBuilder DecompredText = new StringBuilder(String.Empty);
            BinaryReader DeSerializer = new BinaryReader(File.OpenRead("Quraan_File.ctxt"));
            DeSerializer.ReadString();
            DeSerializer.ReadBoolean();
            DeSerializer.ReadString();
            DeSerializer.ReadBoolean();
            DeSerializer.ReadString();
            DeSerializer.ReadBoolean();
            count = DeSerializer.ReadInt32();
            DeSerializer.ReadBoolean();
            for (int i = 0; i < count; i++)
                DecodedText.Add(DeSerializer.ReadUInt16());
            foreach (var i in DecodedText)
                Int16toBinary(i);
            for (int i = 0; i < DecodedList.Count / 6; i++)
            {
                if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append(' ');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ا');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ب');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ت');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ث');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ج');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ح');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('خ');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('د');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ذ');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ر');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ز');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('س');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ش');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ص');
                else if (!DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ض');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ط');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ظ');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ع');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('غ');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ف');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ق');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ك');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ل');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('م');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ن');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ه');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('و');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ي');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('أ');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('إ');
                else if (!DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('آ');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ى');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ة');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ء');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ؤ');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ئ');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append(',');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('.');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append(')');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('(');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('0');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('1');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('2');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('3');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('4');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('5');
                else if (DecodedList[i * 6] && !DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('6');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('7');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('8');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('9');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('َ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ُ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ِ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ْ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && !DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ً');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ٌ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('ْ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('ّ');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && !DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('\n');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('@');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && !DecodedList[i * 6 + 4] && DecodedList[i * 6 + 5])
                    DecompredText.Append('#');
                else if (DecodedList[i * 6] && DecodedList[i * 6 + 1] && DecodedList[i * 6 + 2] && DecodedList[i * 6 + 3] && DecodedList[i * 6 + 4] && !DecodedList[i * 6 + 5])
                    DecompredText.Append('\t');
            }
            return DecompredText.ToString();
        }

        private void Int16toBinary(ushort n)
        {
            bool[] tmp = new bool[16];
            tmp[15] = (n % 2 == 1); n /= 2;
            tmp[14] = (n % 2 == 1); n /= 2;
            tmp[13] = (n % 2 == 1); n /= 2;
            tmp[12] = (n % 2 == 1); n /= 2;
            tmp[11] = (n % 2 == 1); n /= 2;
            tmp[10] = (n % 2 == 1); n /= 2;
            tmp[9] = (n % 2 == 1); n /= 2;
            tmp[8] = (n % 2 == 1); n /= 2;
            tmp[7] = (n % 2 == 1); n /= 2;
            tmp[6] = (n % 2 == 1); n /= 2;
            tmp[5] = (n % 2 == 1); n /= 2;
            tmp[4] = (n % 2 == 1); n /= 2;
            tmp[3] = (n % 2 == 1); n /= 2;
            tmp[2] = (n % 2 == 1); n /= 2;
            tmp[1] = (n % 2 == 1); n /= 2;
            tmp[0] = (n % 2 == 1); n /= 2;
            for (int i = 0; i < 16; i++)
                DecodedList.Add(tmp[i]);
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (find_window == null)
            {
                find_window = new Find();
                find_window.Owner = this;
                find_window.Show();
            }
            else
                find_window.Focus();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Copy window = new Copy();
            window.ShowDialog();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            about window = new about();
            window.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            about window = new about();
            window.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            int result_num = 0;
            search_result = 0;
            Search.Background = Brushes.White;
            string[] search_text_array = new string[1];
            search_text_array[0] = Replace_Accent(Search.Text);
            string sura = Replace_Accent(Sura);
            Par.Inlines.Clear();
            Par.Inlines.Add(basmala);
            if (search_text_array[0].Length > 0 && sura.Contains(search_text_array[0]))
            {
                List<int> search_word_indexes = new List<int>();
                indexes = Find.get_identity_index(sura, search_text_array, out search_word_indexes);
                int pointer = 0;
                result_num += indexes.Count;
                Run n;
                for (int i = 0; i < indexes.Count; i++)
                {
                    n = new Run(sura.Substring(pointer, indexes[i] - pointer));
                    Run y = new Run(search_text_array[0]);
                    y.Background = Brushes.PowderBlue;
                    pointer += (indexes[i] - pointer + search_text_array[0].Length);
                    if (n.Text.Length > 0)
                        Par.Inlines.Add(n);
                    Par.Inlines.Add(y);
                }
                n = new Run(sura.Substring(pointer, sura.Length - pointer));
                if (n.Text.Length > 0)
                    Par.Inlines.Add(n);
                Viewer.ScrollToHome();
                Viewer.ScrollToVerticalOffset(indexes[search_result] * (430 / ActualWidth));
            }
            else
            {
                for (int i = 0; i < Ayaat[Sura_Index].Length - 1; i++)
                    Par.Inlines.Add(new Run(" " + Ayaat[Sura_Index][i] + "(" + (i + 1) + ")"));
                if (search_text_array[0].Length == 0)
                { }
                else
                    Search.Background = Brushes.IndianRed;
            }
        }

        private string Replace_Accent(string str)
        {
            return str.Replace("َ", "").Replace("ً", "").Replace("ُ", "").Replace("ٌ", "").Replace("ِ", "").Replace("ٍ", "").Replace("ّ", "").Replace("ْ", "");
        }

        private bool Is_Accent(char ch)
        {
            if (ch == 'َ' || ch == 'ً' || ch == 'ُ' || ch == 'ٌ' || ch == 'ِ' || ch == 'ٍ' || ch == 'ّ' || ch == 'ْ')
                return true;
            return false;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Quraan Book"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Quraan Book");
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Quraan Book\\QurranFile.dat"))
            {
                BinaryFormatter Serlizer = new BinaryFormatter();
                Stream Writer = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Quraan Book\\QurranFile.dat");
                Serlizer.Serialize(Writer, Ayaat);
                Writer.Close();
            }
        }

        private void rtf_viewer_Click(object sender, RoutedEventArgs e)
        {
            new RTF_Viewer().Show();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            //Viewer.Document.PageHeight = 800;
            //Viewer.Document.PageWidth = 750;
            //Document
            //DocumentPaginator m=;
            //Viewer.;
            PrintDialog PD = new PrintDialog();
            //PD.PrintDocument(new FixedDocument(), "طباعة سورة ");
            PD.ShowDialog();
            var i = new System.Windows.Forms.PrintDialog();
            System.Drawing.Printing.PrintDocument k = new System.Drawing.Printing.PrintDocument();
            k.Print();
            //i.Document = Viewer.Document;
        }

        private void go_pevious_Click(object sender, RoutedEventArgs e)
        {
            Sura_Index--;
            ChangeSura();
        }

        private void go_next_Click(object sender, RoutedEventArgs e)
        {
            Sura_Index++;
            ChangeSura();
        }

        private void Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (indexes.Count > 1 && e.Key == Key.Enter)
            {
                if (search_result == indexes.Count - 1)
                    search_result = -1;
                search_result++;
                Viewer.ScrollToHome();
                Viewer.ScrollToVerticalOffset(indexes[search_result] * (430 / ActualHeight));
            }
        }

        private void RichTextBox_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int s;
                if (int.TryParse(s_sort.Text, out s))
                    if (s > 114 || s < 1)
                        if (Sura_Index != -1)
                            s_sort.Text = (Sura_Index + 1).ToString();
                        else
                            s_sort.Text = "";
                    else
                    {
                        Sura_Index = s - 1;
                        ChangeSura();
                    }
                else if (Sura_Index != -1)
                    s_sort.Text = (Sura_Index + 1).ToString();
                else
                    s_sort.Text = "";
            }
        }

        internal void Scroll(int p)
        {
            double offset = (p + 0.0) / (Ayaat[Sura_Index].Length);
            Viewer.ScrollToEnd();
            Viewer.ScrollToVerticalOffset(Viewer.VerticalOffset * offset);
        }
    }
}