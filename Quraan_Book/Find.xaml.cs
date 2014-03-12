using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Find.xaml
    /// </summary>
    public partial class Find : Window
    {
        BackgroundWorker worker;
        List<int> Search_In = new List<int>(114);
        List<int> Resulti = new List<int>();
        List<int> Resultj = new List<int>();
        int Result_Index = 0, result_num = 0;
        bool Hamza = false, Word = false, Accent = true;
        public Find()
        {
            InitializeComponent();
            TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            CheckBox ch1 = new CheckBox();
            ch1.Content = "كامل المصحف";
            ch1.Checked += ch2_Checked;
            ch1.Unchecked += ch2_Unchecked;
            Suras_List.SelectedIndex = 0;
            Suras_List.Items.Add(ch1);
            for (int i = 0; i < 114; i++)
            {
                ch1 = new CheckBox();
                ch1.Content = MainWindow.Sura_Name[i];
                ch1.Checked += ch1_Checked;
                ch1.Unchecked += ch1_Unchecked;
                Suras_List.Items.Add(ch1);
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            result_num = 0;
            TaskbarItemInfo.ProgressValue = 0;
            Get_Search_In();
            if (Search_Word.Text.Equals(String.Empty))
                MessageBox.Show("أدخل على الأقل كلمة واحدة للبحث عنها!", "البحث", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (Search_Word.Text.Length < 2)
                MessageBox.Show("كلمة البحث قصيرة جدا!", "البحث", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (Search_In.Count > 0)
            {
                Sura_Name.Text = "";
                Aya_Number.Text = "";
                Resulti.Clear();
                Resultj.Clear();
                Search_Results.SelectedIndex = -1;
                Sura_Name.Text = "";
                Aya_Number.Text = "";
                Search_Results.Items.Clear();
                Result_Index = 0;
                string[] search_word = new string[1];
                bool? and = null;
                if (Search_Word.Text.Contains("&") && Search_Word.Text.Contains("|"))
                {
                    MessageBox.Show("لا يمكن ادخال المعاملين '&' و '|' معاً!", "البحث", MessageBoxButton.OK, MessageBoxImage.Error);
                    search_word[0] = Search_Word.Text;
                }
                else if (Search_Word.Text.Contains("&"))
                {
                    search_word = Search_Word.Text.Split('&');
                    and = true;
                }
                else if (Search_Word.Text.Contains("|"))
                {
                    search_word = Search_Word.Text.Split('|');
                    and = false;
                }
                else
                    search_word[0] = Search_Word.Text;
                search_word = Optimize(search_word,and);
                if (search_word.Length == 0)
                    MessageBox.Show("كلمات البحث قصيرة جدا!", "البحث", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    TaskbarItemInfo.ProgressValue = 0.05;
                    bool found;
                    foreach (var i in Search_In)
                        for (int j = 0; j < MainWindow.Ayaat[i].Length; j++)
                        {
                            if (Word)
                                found = Search_By_Word(MainWindow.Ayaat[i][j], search_word, and);
                            else
                                found = Search_By_Text(MainWindow.Ayaat[i][j], search_word, and);
                            if (found)
                            {
                                Resulti.Add(i);
                                Resultj.Add(j);
                                Result_Index++;
                            }
                            TaskbarItemInfo.ProgressValue += (0.1 / (MainWindow.Ayaat[i].Length * Search_In.Count));
                        }
                    if (Result_Index != 0)
                    {
                        for (int i = 0; i < Result_Index; i++)
                        {
                            Search_Results.Items.Add(Make_Grid(search_word, Resulti[i], Resultj[i]));
                            TaskbarItemInfo.ProgressValue += (0.8 / Result_Index);
                        }
                        Result_Number.Text = result_num.ToString();
                        Ayat_Number.Text = Result_Index.ToString();
                        Search_Results.ScrollIntoView(Search_Results.Items[0]);
                    }
                    else
                    {
                        MessageBox.Show("لا يوجد نتائج!", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading);
                        Result_Number.Text = "0";
                        Ayat_Number.Text = "0";
                    }
                }
            }
            else
                MessageBox.Show("اختر مكان للبحث!", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            TaskbarItemInfo.ProgressValue = 1;
        }

        private string[] Optimize(string[] search_word, bool? and)
        {
            List<string> search_word_list = search_word.ToList<string>();
            int count = search_word_list.Count;
            for (int i = 0; i < search_word_list.Count - 1; i++)
                for (int j = i + 1; j < search_word_list.Count; j++)
                    if ((search_word_list[i] == search_word_list[j]) || (Reverse(search_word_list[i]) == search_word_list[j]))
                    {
                        search_word_list.Remove(search_word_list[j]);
                        j--;
                    }
            for (int i = 0; i < search_word_list.Count; i++)
                for (int j = 0; j < search_word_list.Count; j++)
                    if (i == j)
                        continue;
                    else
                    {
                        if (search_word_list[i].Contains(search_word_list[j]))
                        {
                            search_word_list.Remove(search_word_list[j]);
                            j--;
                            if (j < i)
                                i--;
                            continue;
                        }
                        if (search_word_list[i][0] == search_word_list[j][search_word_list[j].Length - 1])
                        {
                            search_word_list.Remove(search_word_list[j]);
                            j--;
                            if (j < i)
                                i--;
                        }
                    }
            for (int i = 0; i < search_word_list.Count; i++)
                if (search_word_list[i].Length < 2)
                {
                    search_word_list.Remove(search_word_list[i]);
                    i--;
                }
            if (count != search_word_list.Count)
                MessageBox.Show("تم ازالة بعض كلمات البحث!", "البحث", MessageBoxButton.OK, MessageBoxImage.Warning);
            StringBuilder temp = new StringBuilder("");
            foreach (var i in search_word_list)
            {
                temp.Append(i);
                if (i != search_word_list[search_word_list.Count - 1])
                    if (and != false)
                        temp.Append("&");
                    else
                        temp.Append("|");
            }
            Search_Word.Text = temp.ToString();
            return search_word_list.ToArray();
        }

        private string Reverse(string p)
        {
            StringBuilder n = new StringBuilder();
            for (int i = p.Length - 1; i > -1; i--)
                n.Append(p[i]);
            return n.ToString();
        }

        private void Get_Search_In()
        {
            Search_In.Clear();
            for (int i = 1; i < Suras_List.Items.Count; i++)
                if (((CheckBox)(Suras_List.Items[i])).IsChecked == true)
                    Search_In.Add(i - 1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Search_Word.Focus();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            for (int i = 0; i < Search_Results.Items.Count; i++)
                ((TextBlock)((Grid)(((ListViewItem)Search_Results.Items[i]).Content)).Children[0]).Width = Search_Results.ActualWidth - 157;
        }

        private void ch2_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < Suras_List.Items.Count; i++)
                ((CheckBox)(Suras_List.Items[i])).IsChecked = true;
        }

        private void ch2_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < Suras_List.Items.Count; i++)
                ((CheckBox)(Suras_List.Items[i])).IsChecked = false;
        }

        private void ch1_Checked_Unchecked(int num, bool bo)
        {
            int num1 = 0;
            for (int i = 1; i < Suras_List.Items.Count; i++)
                if (((CheckBox)(Suras_List.Items[i])).IsChecked == true)
                    num1++;
            if (num1 == num)
                ((CheckBox)(Suras_List.Items[0])).IsChecked = bo;
            else
                ((CheckBox)(Suras_List.Items[0])).IsChecked = null;
        }

        private void ch1_Unchecked(object sender, RoutedEventArgs e)
        {
            ch1_Checked_Unchecked(0, false);
        }

        private void ch1_Checked(object sender, RoutedEventArgs e)
        {
            ch1_Checked_Unchecked(114, true);
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Search_Results.SelectedIndex < Search_Results.Items.Count & Search_Results.SelectedIndex != -1)
            {
                Sura_Name.Text = MainWindow.Sura_Name[Resulti[Search_Results.SelectedIndex]];
                Aya_Number.Text = (Resultj[Search_Results.SelectedIndex] + 1).ToString();
            }
        }

        private bool Search_By_Word(string In_Text, string[] Find_Text, bool? and)
        {
            string text = Replace_Hamza(Replace_Accent(In_Text));
            string[] search_text = Replace_Hamza(Replace_Accent(Find_Text));
            string[] Words;
            Words = text.Split(' ');
            bool found = true;
            for (int i = 0; i < Words.Length; i++)
                if (and == true || and == null)
                {
                    for (int j = 0; j < search_text.Length; j++)
                        if (!Words.Contains(search_text[j]))
                            found = false;
                }
                else if (and == false)
                {
                    found = false;
                    for (int j = 0; j < search_text.Length; j++)
                        if (Words.Contains(search_text[j]))
                            found = true;
                }
            return found;
        }

        private bool Search_By_Text(string In_Text, string[] Find_Text, bool? and)
        {
            string text = Replace_Hamza(Replace_Accent(In_Text));
            string[] search_text = Replace_Hamza(Replace_Accent(Find_Text));
            bool found = true;
            if (and == true || and == null)
            {
                for (int j = 0; j < search_text.Length; j++)
                    if (!(text.Contains(search_text[j])))
                        found = false;
            }
            else if (and == false)
            {
                found = false;
                for (int j = 0; j < search_text.Length; j++)
                    if (text.Contains(search_text[j]))
                        found = true;
            }
            return found;
        }

        private string[] Replace_Accent(string[] Find_Text)
        {
            for (int i = 0; i < Find_Text.Length; i++)
                Find_Text[i] = Replace_Accent(Find_Text[i]);
            return Find_Text;
        }

        private string[] Replace_Hamza(string[] Find_Text)
        {
            for (int i = 0; i < Find_Text.Length; i++)
                Find_Text[i] = Replace_Hamza(Find_Text[i]);
            return Find_Text;
        }

        private string Replace_Hamza(string str)
        {
            if (Hamza == true)
                return str.Replace("أ", "ا").Replace("إ", "ا").Replace("آ", "ا");
            else
                return str;
        }

        private string Replace_Accent(string str)
        {
            if (Accent == true)
                return str.Replace("َ", "").Replace("ً", "").Replace("ً", "").Replace("ُ", "").Replace("ٌ", "").Replace("ِ", "").Replace("ٍ", "").Replace("ّ", "").Replace("ْ", "");
            else
                return str;
        }

        private void Window_PreviewKeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private ListViewItem Make_Grid(string[] search_word, int sura_index, int aya_index)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Quraan_Book;component/Images/searchbackground.png")));
            rectangle.Margin = new Thickness(0, 0, 0, 0);
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Width = 110;
            rectangle.Height = 100;
            TextBlock sura_name = new TextBlock();
            sura_name.Text = MainWindow.Sura_Name[sura_index];
            sura_name.Foreground = Brushes.Beige;
            sura_name.RenderTransform = new RotateTransform(30);
            sura_name.HorizontalAlignment = HorizontalAlignment.Left;
            sura_name.VerticalAlignment = VerticalAlignment.Top;
            sura_name.Margin = new Thickness(10, 30, 0, 0);
            sura_name.Width = 95;
            sura_name.FontSize = 24;
            sura_name.TextAlignment = TextAlignment.Center;
            sura_name.FontWeight = FontWeights.Bold;
            sura_name.RenderTransformOrigin = new Point(0.5, 0.5);
            Grid grid = new Grid();
            grid.Height = 100;
            grid.Children.Add(Create_Paragraph(MainWindow.Ayaat[sura_index][aya_index] + "(" + (aya_index + 1) + ")", search_word));
            grid.Children.Add(rectangle);
            grid.Children.Add(sura_name);
            ListViewItem item = new ListViewItem();
            item.Content = grid;
            item.MouseDoubleClick += GotoResult_DoubleClick;
            ContextMenu menu = new System.Windows.Controls.ContextMenu();
            MenuItem menu_item = new MenuItem();
            menu_item.Header = "نسخ الآية";
            menu_item.Click += Menu_Item_Click;
            menu.Items.Add(menu_item);
            item.ContextMenu = menu;
            return item;
        }

        private void Menu_Item_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(((TextBlock)((Grid)(((ListViewItem)Search_Results.SelectedItem).Content)).Children[0]).Tag.ToString());
        }

        private void GotoResult_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Sura_Index = Resulti[Search_Results.SelectedIndex];
            MainWindow window = (MainWindow)Owner;
            window.ChangeSura();
            window.Scroll(Resultj[Search_Results.SelectedIndex]);
        }

        private void CheckBox1_Click(object sender, RoutedEventArgs e)
        {
            if (Ignore_Hamza.IsChecked == true)
                Hamza = true;
            else
                Hamza = false;
        }

        private void CheckBox2_Click(object sender, RoutedEventArgs e)
        {
            if (Full_Word.IsChecked == true)
                Word = true;
            else
                Word = false;
        }

        private void CheckBox3_Click(object sender, RoutedEventArgs e)
        {
            if (Ignore_Accent.IsChecked == true)
                Accent = true;
            else
                Accent = false;
        }

        private TextBlock Create_Paragraph(string text, string[] search)
        {
            TextBlock aya_text = new TextBlock();
            aya_text.Margin = new Thickness(115, 10, 10, 10);
            aya_text.HorizontalAlignment = HorizontalAlignment.Stretch;
            aya_text.VerticalAlignment = VerticalAlignment.Stretch;
            aya_text.FontSize = 14;
            aya_text.TextTrimming = TextTrimming.WordEllipsis;
            aya_text.Width = Search_Results.ActualWidth - 157;
            aya_text.TextWrapping = TextWrapping.Wrap;
            aya_text.Height = 80;
            aya_text.Tag = text;
            string[] Search_Text = Replace_Accent(search);
            string Sura = Replace_Accent(text);
            if (Hamza)
            {
                Search_Text = Replace_Hamza(Search_Text);
                Sura = Replace_Hamza(Sura);
            }
            List<int> indexes = new List<int>();
            List<int> search_word_indexex = new List<int>();
            indexes = get_identity_index(Sura, Search_Text, out search_word_indexex);
            int pointer = 0;
            result_num += indexes.Count;
            Run n;
            for (int i = 0; i < indexes.Count; i++)
            {
                n = new Run();
                try
                { n = new Run(Sura.Substring(pointer, indexes[i] - pointer)); }
                catch (Exception) { }
                Run y = new Run(Search_Text[search_word_indexex[i]]);
                n.Foreground = Brushes.Black;
                y.Foreground = Brushes.Tomato;
                pointer += (indexes[i] - pointer + Search_Text[search_word_indexex[i]].Length);
                        if (n.Text.Length > 0)
                            aya_text.Inlines.Add(n);
                aya_text.Inlines.Add(y);
            }
            n = new Run(Sura.Substring(pointer, Sura.Length - pointer));
            n.Foreground = Brushes.Black;
            if (n.Text.Length > 0)
                aya_text.Inlines.Add(n);
            return aya_text;
        }

        public static List<int> get_identity_index(string text, string[] search, out List<int> search_word_indexes)
        {
            List<int> indexes = new List<int>();
            search_word_indexes = new List<int>();
            for (int i = 0; i < search.Length; i++)
            {
                int j = 0;
                j = text.IndexOf(search[i], j) + search[i].Length;
                while (j < text.Length && j - search[i].Length > -1)
                {
                    indexes.Add(j - search[i].Length);
                    search_word_indexes.Add(i);
                    j = text.IndexOf(search[i], j) + search[i].Length;
                }
            }
            if (indexes.Count > 1)
                Quick_Sort(indexes, search_word_indexes, 0, indexes.Count - 1);
            return indexes;
        }

        public static void Quick_Sort(List<int> indexes, List<int> search_word_indexex, int left, int right)
        {
            int pivot;
            if (right > left)
            {
                pivot = Partition(indexes, search_word_indexex, left, right);
                Quick_Sort(indexes, search_word_indexex, left, pivot - 1);
                Quick_Sort(indexes, search_word_indexex, pivot + 1, right);
            }
        }

        public static int Partition(List<int> indexes, List<int> search_word_indexex, int left, int right)
        {
            int swapPos;
            int pivotPos = left + (right - left) / 2;
            Swap(indexes, left, pivotPos);
            Swap(search_word_indexex, left, pivotPos);
            swapPos = left + 1;
            for (int i = swapPos; i <= right; i++)
                if (indexes[i] < indexes[left])
                {
                    Swap(indexes, i, swapPos);
                    Swap(search_word_indexex, i, swapPos);
                    swapPos++;
                }
            Swap(indexes, swapPos - 1, left);
            Swap(search_word_indexex, swapPos - 1, left);
            return swapPos - 1;
        }

        public static void Swap(List<int> A, int first, int last)
        {
            int tmp = A[last];
            A[last] = A[first];
            A[first] = tmp;
        }

        private void find_window_Closed(object sender, EventArgs e)
        {
            MainWindow window = (MainWindow)Owner;
            window.find_window = null;
        }
    }
}