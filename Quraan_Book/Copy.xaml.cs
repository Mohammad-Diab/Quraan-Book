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
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Quraan_Book
{
    /// <summary>
    /// Interaction logic for Copy.xaml
    /// </summary>
    public partial class Copy : Window
    {
        int saved_file_count = 1;
        List<CheckBox>[] CheckBoxes = new List<CheckBox>[114];
        bool go_ahead = true;
        public Copy()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            for(int i=0;i< MainWindow.Ayaat.Length;i++)
            {
                CheckBoxes[i]=new List<CheckBox>();
                for (int j = 0; j < MainWindow.Ayaat[i].Length - 1; j++)
                {
                    CheckBox ch = new CheckBox();
                    ch.ContentStringFormat = i.ToString();
                    ch.Content = "الآية " + (j + 1);
                    ch.Checked += Aya_Checked;
                    ch.Unchecked += Aya_Unchecked;
                    CheckBoxes[i].Add(ch);
                }
            }
            CheckBox item = new CheckBox();
            item.Content = "كامل المصحف";
            item.Checked += All_Checked;
            item.Unchecked += All_Unchecked;
            Sura.Items.Add(item);
            for (int i = 0; i < 114; i++)
            {
                var ch = new CheckBox();
                ch.Content = MainWindow.Sura_Name[i];
                ch.ContentStringFormat = i.ToString();
                ch.Checked += Sura_Checked;
                ch.Unchecked += Sura_Unchecked;
                Sura.Items.Add(ch);
            }
        }

        private void Sura_Checked(object sender, RoutedEventArgs e)
        {
            int b;
            int.TryParse(((CheckBox)sender).ContentStringFormat, out b);
            Sura_Checked_Unchecked(114, true, b);
        }

        private void Sura_Unchecked(object sender, RoutedEventArgs e)
        {
            int b;
            int.TryParse(((CheckBox)sender).ContentStringFormat, out b);
            Sura_Checked_Unchecked(0, false, b);
        }

        private void Sura_Checked_Unchecked(int refrance, bool bo, int sender)
        {
            if (go_ahead)
            {
                go_ahead = false;
                int True = 0, Null = 0;
                for (int i = 0; i < CheckBoxes[sender].Count; i++)
                    CheckBoxes[sender][i].IsChecked = bo;
                for (int i = 1; i < Sura.Items.Count; i++)
                    if (((CheckBox)Sura.Items[i]).IsChecked == true)
                        True++;
                    else if (((CheckBox)Sura.Items[i]).IsChecked == null)
                        Null++;
                if (Null > 0)
                    ((CheckBox)Sura.Items[0]).IsChecked = null;
                else if (True == refrance)
                    ((CheckBox)Sura.Items[0]).IsChecked = bo;
                else
                    ((CheckBox)Sura.Items[0]).IsChecked = null;
                go_ahead = true;
            }
        }

        private void Aya_Checked(object sender, RoutedEventArgs e)
        {
            Aya_Checked_Unchecked(CheckBoxes[int.Parse(((CheckBox)sender).ContentStringFormat)].Count, true, int.Parse(((CheckBox)sender).ContentStringFormat));
        }

        private void Aya_Unchecked(object sender, RoutedEventArgs e)
        {
            Aya_Checked_Unchecked(0, false, int.Parse(((CheckBox)sender).ContentStringFormat));
        }

        private void Aya_Checked_Unchecked(int refrance, bool bo,int sura_index)
        {
            if (go_ahead)
            {
                int num = 0;
                for (int j = 0; j < CheckBoxes[sura_index].Count; j++)
                    if (((CheckBox)(CheckBoxes[sura_index][j])).IsChecked == true)
                        num++;
                if (num == refrance)
                    ((CheckBox)(Sura.Items[sura_index + 1])).IsChecked = bo;
                else
                    ((CheckBox)(Sura.Items[sura_index + 1])).IsChecked = null;
                num = 0;
                for (int i = 0; i < CheckBoxes.Length; i++)
                    for (int j = 0; j < CheckBoxes[i].Count; j++)
                        if (((CheckBox)(CheckBoxes[i][j])).IsChecked == true)
                            num++;
                if (num == 6235)
                    ((CheckBox)(Sura.Items[0])).IsChecked = true;
                else if (num == 0)
                    ((CheckBox)(Sura.Items[0])).IsChecked = false;
                else
                    ((CheckBox)(Sura.Items[0])).IsChecked = null;
            }
        }

        private void All_Checked(object sender, RoutedEventArgs e)
        {
            All_Checked_Unchecked(true);
        }

        private void All_Unchecked(object sender, RoutedEventArgs e)
        {
            All_Checked_Unchecked(false);
        }

        private void All_Checked_Unchecked(bool bo)
        {
            if (go_ahead)
            {
                go_ahead = false;
                for (int i = 0; i < CheckBoxes.Length; i++)
                    for (int j = 0; j < CheckBoxes[i].Count; j++)
                        CheckBoxes[i][j].IsChecked = bo;
                for (int i = 1; i < Sura.Items.Count; i++)
                    ((CheckBox)Sura.Items[i]).IsChecked = bo;
                go_ahead = true;
            }
        }

        private string Replace_Accent(string str)
        {
            if (Without_Accent.IsChecked == true)
                return str.Replace("َ", "").Replace("اً", "").Replace("ً", "").Replace("ُ", "").Replace("ٌ", "").Replace("ِ", "").Replace("ٍ", "").Replace("ّ", "").Replace("ْ", "");
            else
                return str;
        }

        private void Window_PreviewKeyUp_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void Sura_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sura.SelectedIndex > 0)
            {
                var item = new ListViewItem();
                Aya.Items.Clear();
                item.Content = "سورة " + MainWindow.Sura_Name[Sura.SelectedIndex - 1];
                item.ContentStringFormat = (Sura.SelectedIndex - 1).ToString();
                Aya.Items.Add(item);
                for (int i = 0; i < CheckBoxes[Sura.SelectedIndex - 1].Count; i++)
                    Aya.Items.Add(CheckBoxes[Sura.SelectedIndex - 1][i]);
                Aya.ScrollIntoView(Aya.Items[0]);
            }
            else if (Sura.SelectedIndex == 0)
                Aya.Items.Clear();
        }

        private void Copy_to_Clipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Make_Text_File());
            if (Clipboard.GetText().Length > 1)
                MessageBox.Show("تم نسخ النص الى الحافظة.", "تم", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("لم تقم بتحديد أي شيء لنسخه!", "تم", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Save_to_File_Click(object sender, RoutedEventArgs e)
        {
            bool complete = true;
            string text = Make_Text_File();
            if (text.Length > 1)
            {
                System.Windows.Forms.SaveFileDialog SFD = new System.Windows.Forms.SaveFileDialog();
                SFD.Filter = "HTML File (*.htm; *.html)|*.htm;*.html|Word Document (*.docx)|*.docx|XPS Document (*.xps)|*.xps|Open Office Document (*.odt)|*.odt|Rich File Document (*.rtf)|*.rtf|Plain Text File (*.txt)|*.txt";
                SFD.FileName = "File" + saved_file_count;
                if (SFD.ShowDialog().Equals(System.Windows.Forms.DialogResult.OK))
                {
                    saved_file_count++;
                    try
                    {
                        if (new FileInfo(SFD.FileName).Extension.ToLower().Equals(".rtf"))
                        {
                            File.Open(SFD.FileName, FileMode.Create).Close();
                            FileStream FS = new FileStream(SFD.FileName, FileMode.Open);
                            FlowDocument file = new FlowDocument();
                            file = Make_RTF_File();
                            TextRange TR = new TextRange(file.ContentStart, file.ContentEnd);
                            TR.Save(FS, DataFormats.Rtf);
                            FS.Close();
                        }
                        else if (new FileInfo(SFD.FileName).Extension.ToLower().Equals(".htm") || new FileInfo(SFD.FileName).Extension.ToLower().Equals(".html"))
                        {
                            StreamWriter SW = new StreamWriter(File.Open(SFD.FileName, FileMode.Create), Encoding.Unicode);
                            SW.Write(Make_HTML_File());
                            SW.Close();
                        }
                        else if (new FileInfo(SFD.FileName).Extension.ToLower().Equals(".docx"))
                            complete = Make_Docx_File(SFD.FileName);
                        else if (new FileInfo(SFD.FileName).Extension.ToLower().Equals(".odt"))
                            complete = Make_ODT_File(SFD.FileName);
                        else
                        {
                            StreamWriter SW = new StreamWriter(File.Open(SFD.FileName, FileMode.Create), Encoding.Unicode);
                            SW.Write(text);
                            SW.Close();
                        }
                        if (complete)
                            MessageBox.Show("تم حفظ الملف الى المسار: \n" + SFD.FileName, "تم", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error); saved_file_count--; }
                }
            }
            else
                MessageBox.Show("لم تقم بتحديد أي شيء لحفظه!", "تم", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private string Make_Text_File()
        {
            StringBuilder text = new StringBuilder("");
            for (int i = 0; i < CheckBoxes.Length; i++)
                if (((CheckBox)Sura.Items[i + 1]).IsChecked != false)
                {
                    text.Append("سورة ");
                    text.Append(MainWindow.Sura_Name[i]);
                    text.Append("\r\n");
                    if (i != 0 && i != 8)
                        text.Append("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ\r\n");
                    int old_j = -1;
                    for (int j = 0; j < CheckBoxes[i].Count; j++)
                        if (CheckBoxes[i][j].IsChecked == true)
                        {
                            if (old_j != j - 1)
                                text.Append("\r\n");
                            text.Append(Replace_Accent(MainWindow.Ayaat[i][j]));
                            text.Append("(" + (j + 1) + ") ");
                            old_j = j;
                        }
                        else if (text.Length == 0)
                            old_j++;
                    if (i != 113) text.Append("\r\n");
                }
            return Replace_Accent(text.ToString());
        }

        private string Make_HTML_File()
        {
            StringBuilder html_file = new StringBuilder("<html>\r\n	<title>Created By Electronic Quraan Book 2 (APPS PREVIEW)</title>\n	<body>\r\n");
            for (int i = 0; i < CheckBoxes.Length; i++)
                if (((CheckBox)Sura.Items[i + 1]).IsChecked != false)
                {
                    html_file.Append("		<p style='text-align:center;direction:rtl;font-family:\"Courier New\"'>\r\n");
                    html_file.Append("			<span style='font-size:18.0pt;line-height:125%;color:green;mso-bidi-language:AR-SY;'>سورة " + MainWindow.Sura_Name[i] + "</span><br>\r\n");
                    if (i != 0 && i != 8)
                        html_file.Append("			<span style='font-size:18.0pt;line-height:125%;color:red;mso-bidi-language:AR-SY'>" + Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ") + "</span><br>\r\n");
                    int old_j = -1;
                    StringBuilder text = new StringBuilder("");
                    for (int j = 0; j < CheckBoxes[i].Count; j++)
                        if (CheckBoxes[i][j].IsChecked == true)
                        {
                            if (old_j != j - 1)
                                text.Append("<br>");
                            text.Append(Replace_Accent(MainWindow.Ayaat[i][j]));
                            text.Append("(" + (j + 1) + ") ");
                            old_j = j;
                        }
                        else if (text.Length == 0)
                            old_j++;
                    html_file.Append("			<span style='font-size:14.0pt;line-height:200%;color:black;mso-bidi-language:AR-SY'>" + Replace_Accent(text.ToString()) + "</span>\r\n		</p>\r\n");
                }
            html_file.Append("	</body>\r\n</html>");
            return html_file.ToString();
        }

        private FlowDocument Make_RTF_File()
        {
            FlowDocument doc = new FlowDocument();
            for (int i = 0; i < CheckBoxes.Length; i++)
                if (((CheckBox)Sura.Items[i + 1]).IsChecked != false)
                {
                    Paragraph par = new Paragraph();
                    par.Language = Language;
                    par.TextAlignment = TextAlignment.Center;
                    par.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                    par.FlowDirection = FlowDirection.RightToLeft;
                    par.FontSize = 14;
                    Run run1 = new Run("سورة " + MainWindow.Sura_Name[i]);
                    run1.Foreground = Brushes.Green;
                    run1.FontSize = 18;
                    par.Inlines.Add(run1);
                    par.Inlines.Add(new LineBreak());
                    if (i != 0 && i != 8)
                    {
                        Run run2 = new Run(Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"));
                        run2.Foreground = Brushes.Red;
                        run2.FontSize = 18;
                        par.Inlines.Add(run2);
                        par.Inlines.Add(new LineBreak());
                    }
                    int old_j = -1;
                    for (int j = 0; j < CheckBoxes[i].Count; j++)
                        if (CheckBoxes[i][j].IsChecked == true)
                        {
                            if (old_j != j - 1)
                                par.Inlines.Add(new LineBreak());
                            par.Inlines.Add(new Run(Replace_Accent(MainWindow.Ayaat[i][j]) + "(" + (j + 1) + ") "));
                            old_j = j;
                        }
                        else if (((Run)par.Inlines.LastInline).Text.Length == 0)
                            old_j++;
                    doc.Blocks.Add(par);
                }
            return doc;
        }

        private bool Make_Docx_File(string path)
        {
            DateTime now = DateTime.Now;
            int rand = new Random((int)now.Ticks).Next();
            string directory_name = Directory.CreateDirectory(path + rand).FullName;
            try
            {
                string datetime = now.Year + "-" + Orginize_Date(now.Month, 10) + "-" + Orginize_Date(now.Day, 10) + "T" + Orginize_Date(now.Hour, 10) + ":" + Orginize_Date(now.Minute, 10) + ":00Z";
                string Content_Types = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\"><Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/><Default Extension=\"xml\" ContentType=\"application/xml\"/><Override PartName=\"/word/document.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml\"/><Override PartName=\"/word/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml\"/><Override PartName=\"/word/settings.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml\"/><Override PartName=\"/word/webSettings.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.webSettings+xml\"/><Override PartName=\"/word/fontTable.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml\"/><Override PartName=\"/word/theme/theme1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.theme+xml\"/><Override PartName=\"/docProps/core.xml\" ContentType=\"application/vnd.openxmlformats-package.core-properties+xml\"/><Override PartName=\"/docProps/app.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.extended-properties+xml\"/></Types>";
                string _rels__rels = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\"><Relationship Id=\"rId3\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties\" Target=\"docProps/app.xml\"/><Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties\" Target=\"docProps/core.xml\"/><Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"word/document.xml\"/></Relationships>";
                string docProps_apps = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<Properties xmlns=\"http://schemas.openxmlformats.org/officeDocument/2006/extended-properties\" xmlns:vt=\"http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes\"><Template>Normal.dotm</Template><TotalTime>1</TotalTime><Pages>1</Pages><Words>0</Words><Characters>0</Characters><Application>Quraan Book</Application><DocSecurity>0</DocSecurity><Lines>0</Lines><Paragraphs>0</Paragraphs><ScaleCrop>false</ScaleCrop><HeadingPairs><vt:vector size=\"2\" baseType=\"variant\"><vt:variant><vt:lpstr>العنوان</vt:lpstr></vt:variant><vt:variant><vt:i4>1</vt:i4></vt:variant></vt:vector></HeadingPairs><TitlesOfParts><vt:vector size=\"1\" baseType=\"lpstr\"><vt:lpstr></vt:lpstr></vt:vector></TitlesOfParts><Company>Legend</Company><LinksUpToDate>false</LinksUpToDate><CharactersWithSpaces>0</CharactersWithSpaces><SharedDoc>false</SharedDoc><HyperlinksChanged>false</HyperlinksChanged><AppVersion>2.00</AppVersion></Properties>";
                string docProps_core = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<cp:coreProperties xmlns:cp=\"http://schemas.openxmlformats.org/package/2006/metadata/core-properties\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:dcterms=\"http://purl.org/dc/terms/\" xmlns:dcmitype=\"http://purl.org/dc/dcmitype/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><dc:title></dc:title><dc:subject></dc:subject><dc:creator>Dr. Mohammad</dc:creator><cp:keywords></cp:keywords><dc:description></dc:description><cp:lastModifiedBy>Dr. Mohammad</cp:lastModifiedBy><cp:revision>2</cp:revision><dcterms:created xsi:type=\"dcterms:W3CDTF\">" + datetime + "</dcterms:created><dcterms:modified xsi:type=\"dcterms:W3CDTF\">" + datetime + "</dcterms:modified></cp:coreProperties>";
                string word__rels_document_xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\"><Relationship Id=\"rId3\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings\" Target=\"webSettings.xml\"/><Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings\" Target=\"settings.xml\"/><Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/><Relationship Id=\"rId5\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme\" Target=\"theme/theme1.xml\"/><Relationship Id=\"rId4\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable\" Target=\"fontTable.xml\"/></Relationships>";
                string word_thems_thems1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<a:theme xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\" name=\"Office Theme\"><a:themeElements><a:clrScheme name=\"Office\"><a:dk1><a:sysClr val=\"windowText\" lastClr=\"000000\"/></a:dk1><a:lt1><a:sysClr val=\"window\" lastClr=\"FFFFFF\"/></a:lt1><a:dk2><a:srgbClr val=\"44546A\"/></a:dk2><a:lt2><a:srgbClr val=\"E7E6E6\"/></a:lt2><a:accent1><a:srgbClr val=\"5B9BD5\"/></a:accent1><a:accent2><a:srgbClr val=\"ED7D31\"/></a:accent2><a:accent3><a:srgbClr val=\"A5A5A5\"/></a:accent3><a:accent4><a:srgbClr val=\"FFC000\"/></a:accent4><a:accent5><a:srgbClr val=\"4472C4\"/></a:accent5><a:accent6><a:srgbClr val=\"70AD47\"/></a:accent6><a:hlink><a:srgbClr val=\"0563C1\"/></a:hlink><a:folHlink><a:srgbClr val=\"954F72\"/></a:folHlink></a:clrScheme><a:fontScheme name=\"Office\"><a:majorFont><a:latin typeface=\"Calibri Light\" panose=\"020F0302020204030204\"/><a:ea typeface=\"\"/><a:cs typeface=\"\"/><a:font script=\"Jpan\" typeface=\"ＭＳ ゴシック\"/><a:font script=\"Hang\" typeface=\"맑은 고딕\"/><a:font script=\"Hans\" typeface=\"宋体\"/><a:font script=\"Hant\" typeface=\"新細明體\"/><a:font script=\"Arab\" typeface=\"Times New Roman\"/><a:font script=\"Hebr\" typeface=\"Times New Roman\"/><a:font script=\"Thai\" typeface=\"Angsana New\"/><a:font script=\"Ethi\" typeface=\"Nyala\"/><a:font script=\"Beng\" typeface=\"Vrinda\"/><a:font script=\"Gujr\" typeface=\"Shruti\"/><a:font script=\"Khmr\" typeface=\"MoolBoran\"/><a:font script=\"Knda\" typeface=\"Tunga\"/><a:font script=\"Guru\" typeface=\"Raavi\"/><a:font script=\"Cans\" typeface=\"Euphemia\"/><a:font script=\"Cher\" typeface=\"Plantagenet Cherokee\"/><a:font script=\"Yiii\" typeface=\"Microsoft Yi Baiti\"/><a:font script=\"Tibt\" typeface=\"Microsoft Himalaya\"/><a:font script=\"Thaa\" typeface=\"MV Boli\"/><a:font script=\"Deva\" typeface=\"Mangal\"/><a:font script=\"Telu\" typeface=\"Gautami\"/><a:font script=\"Taml\" typeface=\"Latha\"/><a:font script=\"Syrc\" typeface=\"Estrangelo Edessa\"/><a:font script=\"Orya\" typeface=\"Kalinga\"/><a:font script=\"Mlym\" typeface=\"Kartika\"/><a:font script=\"Laoo\" typeface=\"DokChampa\"/><a:font script=\"Sinh\" typeface=\"Iskoola Pota\"/><a:font script=\"Mong\" typeface=\"Mongolian Baiti\"/><a:font script=\"Viet\" typeface=\"Times New Roman\"/><a:font script=\"Uigh\" typeface=\"Microsoft Uighur\"/><a:font script=\"Geor\" typeface=\"Sylfaen\"/></a:majorFont><a:minorFont><a:latin typeface=\"Calibri\" panose=\"020F0502020204030204\"/><a:ea typeface=\"\"/><a:cs typeface=\"\"/><a:font script=\"Jpan\" typeface=\"ＭＳ 明朝\"/><a:font script=\"Hang\" typeface=\"맑은 고딕\"/><a:font script=\"Hans\" typeface=\"宋体\"/><a:font script=\"Hant\" typeface=\"新細明體\"/><a:font script=\"Arab\" typeface=\"Arial\"/><a:font script=\"Hebr\" typeface=\"Arial\"/><a:font script=\"Thai\" typeface=\"Cordia New\"/><a:font script=\"Ethi\" typeface=\"Nyala\"/><a:font script=\"Beng\" typeface=\"Vrinda\"/><a:font script=\"Gujr\" typeface=\"Shruti\"/><a:font script=\"Khmr\" typeface=\"DaunPenh\"/><a:font script=\"Knda\" typeface=\"Tunga\"/><a:font script=\"Guru\" typeface=\"Raavi\"/><a:font script=\"Cans\" typeface=\"Euphemia\"/><a:font script=\"Cher\" typeface=\"Plantagenet Cherokee\"/><a:font script=\"Yiii\" typeface=\"Microsoft Yi Baiti\"/><a:font script=\"Tibt\" typeface=\"Microsoft Himalaya\"/><a:font script=\"Thaa\" typeface=\"MV Boli\"/><a:font script=\"Deva\" typeface=\"Mangal\"/><a:font script=\"Telu\" typeface=\"Gautami\"/><a:font script=\"Taml\" typeface=\"Latha\"/><a:font script=\"Syrc\" typeface=\"Estrangelo Edessa\"/><a:font script=\"Orya\" typeface=\"Kalinga\"/><a:font script=\"Mlym\" typeface=\"Kartika\"/><a:font script=\"Laoo\" typeface=\"DokChampa\"/><a:font script=\"Sinh\" typeface=\"Iskoola Pota\"/><a:font script=\"Mong\" typeface=\"Mongolian Baiti\"/><a:font script=\"Viet\" typeface=\"Arial\"/><a:font script=\"Uigh\" typeface=\"Microsoft Uighur\"/><a:font script=\"Geor\" typeface=\"Sylfaen\"/></a:minorFont></a:fontScheme><a:fmtScheme name=\"Office\"><a:fillStyleLst><a:solidFill><a:schemeClr val=\"phClr\"/></a:solidFill><a:gradFill rotWithShape=\"1\"><a:gsLst><a:gs pos=\"0\"><a:schemeClr val=\"phClr\"><a:lumMod val=\"110000\"/><a:satMod val=\"105000\"/><a:tint val=\"67000\"/></a:schemeClr></a:gs><a:gs pos=\"50000\"><a:schemeClr val=\"phClr\"><a:lumMod val=\"105000\"/><a:satMod val=\"103000\"/><a:tint val=\"73000\"/></a:schemeClr></a:gs><a:gs pos=\"100000\"><a:schemeClr val=\"phClr\"><a:lumMod val=\"105000\"/><a:satMod val=\"109000\"/><a:tint val=\"81000\"/></a:schemeClr></a:gs></a:gsLst><a:lin ang=\"5400000\" scaled=\"0\"/></a:gradFill><a:gradFill rotWithShape=\"1\"><a:gsLst><a:gs pos=\"0\"><a:schemeClr val=\"phClr\"><a:satMod val=\"103000\"/><a:lumMod val=\"102000\"/><a:tint val=\"94000\"/></a:schemeClr></a:gs><a:gs pos=\"50000\"><a:schemeClr val=\"phClr\"><a:satMod val=\"110000\"/><a:lumMod val=\"100000\"/><a:shade val=\"100000\"/></a:schemeClr></a:gs><a:gs pos=\"100000\"><a:schemeClr val=\"phClr\"><a:lumMod val=\"99000\"/><a:satMod val=\"120000\"/><a:shade val=\"78000\"/></a:schemeClr></a:gs></a:gsLst><a:lin ang=\"5400000\" scaled=\"0\"/></a:gradFill></a:fillStyleLst><a:lnStyleLst><a:ln w=\"6350\" cap=\"flat\" cmpd=\"sng\" algn=\"ctr\"><a:solidFill><a:schemeClr val=\"phClr\"/></a:solidFill><a:prstDash val=\"solid\"/><a:miter lim=\"800000\"/></a:ln><a:ln w=\"12700\" cap=\"flat\" cmpd=\"sng\" algn=\"ctr\"><a:solidFill><a:schemeClr val=\"phClr\"/></a:solidFill><a:prstDash val=\"solid\"/><a:miter lim=\"800000\"/></a:ln><a:ln w=\"19050\" cap=\"flat\" cmpd=\"sng\" algn=\"ctr\"><a:solidFill><a:schemeClr val=\"phClr\"/></a:solidFill><a:prstDash val=\"solid\"/><a:miter lim=\"800000\"/></a:ln></a:lnStyleLst><a:effectStyleLst><a:effectStyle><a:effectLst/></a:effectStyle><a:effectStyle><a:effectLst/></a:effectStyle><a:effectStyle><a:effectLst><a:outerShdw blurRad=\"57150\" dist=\"19050\" dir=\"5400000\" algn=\"ctr\" rotWithShape=\"0\"><a:srgbClr val=\"000000\"><a:alpha val=\"63000\"/></a:srgbClr></a:outerShdw></a:effectLst></a:effectStyle></a:effectStyleLst><a:bgFillStyleLst><a:solidFill><a:schemeClr val=\"phClr\"/></a:solidFill><a:solidFill><a:schemeClr val=\"phClr\"><a:tint val=\"95000\"/><a:satMod val=\"170000\"/></a:schemeClr></a:solidFill><a:gradFill rotWithShape=\"1\"><a:gsLst><a:gs pos=\"0\"><a:schemeClr val=\"phClr\"><a:tint val=\"93000\"/><a:satMod val=\"150000\"/><a:shade val=\"98000\"/><a:lumMod val=\"102000\"/></a:schemeClr></a:gs><a:gs pos=\"50000\"><a:schemeClr val=\"phClr\"><a:tint val=\"98000\"/><a:satMod val=\"130000\"/><a:shade val=\"90000\"/><a:lumMod val=\"103000\"/></a:schemeClr></a:gs><a:gs pos=\"100000\"><a:schemeClr val=\"phClr\"><a:shade val=\"63000\"/><a:satMod val=\"120000\"/></a:schemeClr></a:gs></a:gsLst><a:lin ang=\"5400000\" scaled=\"0\"/></a:gradFill></a:bgFillStyleLst></a:fmtScheme></a:themeElements><a:objectDefaults/><a:extraClrSchemeLst/><a:extLst><a:ext uri=\"{05A4C25C-085E-4340-85A3-A5531E510DB2}\"><thm15:themeFamily xmlns:thm15=\"http://schemas.microsoft.com/office/thememl/2012/main\" name=\"Office Theme\" id=\"{62F939B6-93AF-4DB8-9C6B-D6C7DFDC589F}\" vid=\"{4A3C46E8-61CC-4603-A589-7422A47A8E4A}\"/></a:ext></a:extLst></a:theme>";
                StringBuilder word_document = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<w:document xmlns:wpc=\"http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas\" xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp14=\"http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" xmlns:w14=\"http://schemas.microsoft.com/office/word/2010/wordml\" xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" xmlns:wpg=\"http://schemas.microsoft.com/office/word/2010/wordprocessingGroup\" xmlns:wpi=\"http://schemas.microsoft.com/office/word/2010/wordprocessingInk\" xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\" xmlns:wps=\"http://schemas.microsoft.com/office/word/2010/wordprocessingShape\" mc:Ignorable=\"w14 w15 wp14\"><w:body>");
                string word_fontTable = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<w:fonts xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" xmlns:w14=\"http://schemas.microsoft.com/office/word/2010/wordml\" xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" mc:Ignorable=\"w14 w15\"><w:font w:name=\"Calibri\"><w:panose1 w:val=\"020F0502020204030204\"/><w:charset w:val=\"00\"/><w:family w:val=\"swiss\"/><w:pitch w:val=\"variable\"/><w:sig w:usb0=\"E00002FF\" w:usb1=\"4000ACFF\" w:usb2=\"00000001\" w:usb3=\"00000000\" w:csb0=\"0000019F\" w:csb1=\"00000000\"/></w:font><w:font w:name=\"Arial\"><w:panose1 w:val=\"020B0604020202020204\"/><w:charset w:val=\"00\"/><w:family w:val=\"swiss\"/><w:pitch w:val=\"variable\"/><w:sig w:usb0=\"E0002AFF\" w:usb1=\"C0007843\" w:usb2=\"00000009\" w:usb3=\"00000000\" w:csb0=\"000001FF\" w:csb1=\"00000000\"/></w:font><w:font w:name=\"Times New Roman\"><w:panose1 w:val=\"02020603050405020304\"/><w:charset w:val=\"00\"/><w:family w:val=\"roman\"/><w:pitch w:val=\"variable\"/><w:sig w:usb0=\"E0002EFF\" w:usb1=\"C0007843\" w:usb2=\"00000009\" w:usb3=\"00000000\" w:csb0=\"000001FF\" w:csb1=\"00000000\"/></w:font><w:font w:name=\"Calibri Light\"><w:panose1 w:val=\"020F0302020204030204\"/><w:charset w:val=\"00\"/><w:family w:val=\"swiss\"/><w:pitch w:val=\"variable\"/><w:sig w:usb0=\"A00002EF\" w:usb1=\"4000207B\" w:usb2=\"00000000\" w:usb3=\"00000000\" w:csb0=\"0000019F\" w:csb1=\"00000000\"/></w:font></w:fonts>";
                string word_setting = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<w:settings xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" xmlns:w14=\"http://schemas.microsoft.com/office/word/2010/wordml\" xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" xmlns:sl=\"http://schemas.openxmlformats.org/schemaLibrary/2006/main\" mc:Ignorable=\"w14 w15\"><w:zoom w:percent=\"100\"/><w:proofState w:spelling=\"clean\" w:grammar=\"clean\"/><w:defaultTabStop w:val=\"720\"/><w:characterSpacingControl w:val=\"doNotCompress\"/><w:compat><w:compatSetting w:name=\"compatibilityMode\" w:uri=\"http://schemas.microsoft.com/office/word\" w:val=\"15\"/><w:compatSetting w:name=\"overrideTableStyleFontSizeAndJustification\" w:uri=\"http://schemas.microsoft.com/office/word\" w:val=\"1\"/><w:compatSetting w:name=\"enableOpenTypeFeatures\" w:uri=\"http://schemas.microsoft.com/office/word\" w:val=\"1\"/><w:compatSetting w:name=\"doNotFlipMirrorIndents\" w:uri=\"http://schemas.microsoft.com/office/word\" w:val=\"1\"/><w:compatSetting w:name=\"differentiateMultirowTableHeaders\" w:uri=\"http://schemas.microsoft.com/office/word\" w:val=\"1\"/></w:compat><w:rsids><w:rsidRoot w:val=\"00516929\"/><w:rsid w:val=\"00516929\"/><w:rsid w:val=\"00990529\"/><w:rsid w:val=\"00AE0E56\"/><w:rsid w:val=\"00E9607A\"/></w:rsids><m:mathPr><m:mathFont m:val=\"Cambria Math\"/><m:brkBin m:val=\"before\"/><m:brkBinSub m:val=\"--\"/><m:smallFrac m:val=\"0\"/><m:dispDef/><m:lMargin m:val=\"0\"/><m:rMargin m:val=\"0\"/><m:defJc m:val=\"centerGroup\"/><m:wrapIndent m:val=\"1440\"/><m:intLim m:val=\"subSup\"/><m:naryLim m:val=\"undOvr\"/></m:mathPr><w:themeFontLang w:val=\"en-US\" w:bidi=\"ar-SA\"/><w:clrSchemeMapping w:bg1=\"light1\" w:t1=\"dark1\" w:bg2=\"light2\" w:t2=\"dark2\" w:accent1=\"accent1\" w:accent2=\"accent2\" w:accent3=\"accent3\" w:accent4=\"accent4\" w:accent5=\"accent5\" w:accent6=\"accent6\" w:hyperlink=\"hyperlink\" w:followedHyperlink=\"followedHyperlink\"/><w:shapeDefaults><o:shapedefaults v:ext=\"edit\" spidmax=\"1026\"/><o:shapelayout v:ext=\"edit\"><o:idmap v:ext=\"edit\" data=\"1\"/></o:shapelayout></w:shapeDefaults><w:decimalSymbol w:val=\".\"/><w:listSeparator w:val=\";\"/><w15:chartTrackingRefBased/><w15:docId w15:val=\"{28719760-65DC-4452-B64B-5E9EFD497193}\"/></w:settings>";
                string word_styles = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<w:styles xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" xmlns:w14=\"http://schemas.microsoft.com/office/word/2010/wordml\" xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" mc:Ignorable=\"w14 w15\"><w:docDefaults><w:rPrDefault><w:rPr><w:rFonts w:asciiTheme=\"minorHAnsi\" w:eastAsiaTheme=\"minorHAnsi\" w:hAnsiTheme=\"minorHAnsi\" w:cstheme=\"minorBidi\"/><w:sz w:val=\"22\"/><w:szCs w:val=\"22\"/><w:lang w:val=\"en-US\" w:eastAsia=\"en-US\" w:bidi=\"ar-SA\"/></w:rPr></w:rPrDefault><w:pPrDefault><w:pPr><w:spacing w:after=\"160\" w:line=\"259\" w:lineRule=\"auto\"/></w:pPr></w:pPrDefault></w:docDefaults><w:latentStyles w:defLockedState=\"0\" w:defUIPriority=\"99\" w:defSemiHidden=\"0\" w:defUnhideWhenUsed=\"0\" w:defQFormat=\"0\" w:count=\"371\"><w:lsdException w:name=\"Normal\" w:uiPriority=\"0\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 1\" w:uiPriority=\"9\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 2\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 3\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 4\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 5\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 6\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 7\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 8\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"heading 9\" w:semiHidden=\"1\" w:uiPriority=\"9\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"index 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 6\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 7\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 8\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index 9\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 1\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 2\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 3\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 4\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 5\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 6\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 7\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 8\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toc 9\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Normal Indent\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"footnote text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"annotation text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"header\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"footer\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"index heading\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"caption\" w:semiHidden=\"1\" w:uiPriority=\"35\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"table of figures\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"envelope address\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"envelope return\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"footnote reference\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"annotation reference\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"line number\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"page number\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"endnote reference\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"endnote text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"table of authorities\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"macro\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"toa heading\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Bullet\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Number\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Bullet 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Bullet 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Bullet 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Bullet 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Number 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Number 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Number 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Number 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Title\" w:uiPriority=\"10\" w:qFormat=\"1\"/><w:lsdException w:name=\"Closing\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Signature\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Default Paragraph Font\" w:semiHidden=\"1\" w:uiPriority=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text Indent\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Continue\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Continue 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Continue 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Continue 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"List Continue 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Message Header\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Subtitle\" w:uiPriority=\"11\" w:qFormat=\"1\"/><w:lsdException w:name=\"Salutation\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Date\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text First Indent\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text First Indent 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Note Heading\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text Indent 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Body Text Indent 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Block Text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Hyperlink\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"FollowedHyperlink\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Strong\" w:uiPriority=\"22\" w:qFormat=\"1\"/><w:lsdException w:name=\"Emphasis\" w:uiPriority=\"20\" w:qFormat=\"1\"/><w:lsdException w:name=\"Document Map\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Plain Text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"E-mail Signature\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Top of Form\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Bottom of Form\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Normal (Web)\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Acronym\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Address\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Cite\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Code\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Definition\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Keyboard\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Preformatted\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Sample\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Typewriter\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"HTML Variable\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Normal Table\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"annotation subject\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"No List\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Outline List 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Outline List 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Outline List 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Simple 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Simple 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Simple 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Classic 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Classic 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Classic 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Classic 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Colorful 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Colorful 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Colorful 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Columns 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Columns 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Columns 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Columns 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Columns 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 6\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 7\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid 8\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 4\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 5\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 6\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 7\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table List 8\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table 3D effects 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table 3D effects 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table 3D effects 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Contemporary\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Elegant\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Professional\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Subtle 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Subtle 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Web 1\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Web 2\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Web 3\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Balloon Text\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Table Grid\" w:uiPriority=\"39\"/><w:lsdException w:name=\"Table Theme\" w:semiHidden=\"1\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"Placeholder Text\" w:semiHidden=\"1\"/><w:lsdException w:name=\"No Spacing\" w:uiPriority=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"Light Shading\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Medium List 2\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Light Shading Accent 1\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List Accent 1\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid Accent 1\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1 Accent 1\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2 Accent 1\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1 Accent 1\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Revision\" w:semiHidden=\"1\"/><w:lsdException w:name=\"List Paragraph\" w:uiPriority=\"34\" w:qFormat=\"1\"/><w:lsdException w:name=\"Quote\" w:uiPriority=\"29\" w:qFormat=\"1\"/><w:lsdException w:name=\"Intense Quote\" w:uiPriority=\"30\" w:qFormat=\"1\"/><w:lsdException w:name=\"Medium List 2 Accent 1\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1 Accent 1\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2 Accent 1\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3 Accent 1\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List Accent 1\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading Accent 1\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List Accent 1\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid Accent 1\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Light Shading Accent 2\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List Accent 2\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid Accent 2\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1 Accent 2\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2 Accent 2\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1 Accent 2\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Medium List 2 Accent 2\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1 Accent 2\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2 Accent 2\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3 Accent 2\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List Accent 2\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading Accent 2\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List Accent 2\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid Accent 2\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Light Shading Accent 3\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List Accent 3\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid Accent 3\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1 Accent 3\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2 Accent 3\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1 Accent 3\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Medium List 2 Accent 3\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1 Accent 3\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2 Accent 3\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3 Accent 3\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List Accent 3\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading Accent 3\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List Accent 3\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid Accent 3\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Light Shading Accent 4\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List Accent 4\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid Accent 4\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1 Accent 4\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2 Accent 4\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1 Accent 4\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Medium List 2 Accent 4\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1 Accent 4\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2 Accent 4\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3 Accent 4\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List Accent 4\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading Accent 4\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List Accent 4\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid Accent 4\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Light Shading Accent 5\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List Accent 5\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid Accent 5\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1 Accent 5\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2 Accent 5\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1 Accent 5\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Medium List 2 Accent 5\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1 Accent 5\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2 Accent 5\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3 Accent 5\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List Accent 5\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading Accent 5\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List Accent 5\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid Accent 5\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Light Shading Accent 6\" w:uiPriority=\"60\"/><w:lsdException w:name=\"Light List Accent 6\" w:uiPriority=\"61\"/><w:lsdException w:name=\"Light Grid Accent 6\" w:uiPriority=\"62\"/><w:lsdException w:name=\"Medium Shading 1 Accent 6\" w:uiPriority=\"63\"/><w:lsdException w:name=\"Medium Shading 2 Accent 6\" w:uiPriority=\"64\"/><w:lsdException w:name=\"Medium List 1 Accent 6\" w:uiPriority=\"65\"/><w:lsdException w:name=\"Medium List 2 Accent 6\" w:uiPriority=\"66\"/><w:lsdException w:name=\"Medium Grid 1 Accent 6\" w:uiPriority=\"67\"/><w:lsdException w:name=\"Medium Grid 2 Accent 6\" w:uiPriority=\"68\"/><w:lsdException w:name=\"Medium Grid 3 Accent 6\" w:uiPriority=\"69\"/><w:lsdException w:name=\"Dark List Accent 6\" w:uiPriority=\"70\"/><w:lsdException w:name=\"Colorful Shading Accent 6\" w:uiPriority=\"71\"/><w:lsdException w:name=\"Colorful List Accent 6\" w:uiPriority=\"72\"/><w:lsdException w:name=\"Colorful Grid Accent 6\" w:uiPriority=\"73\"/><w:lsdException w:name=\"Subtle Emphasis\" w:uiPriority=\"19\" w:qFormat=\"1\"/><w:lsdException w:name=\"Intense Emphasis\" w:uiPriority=\"21\" w:qFormat=\"1\"/><w:lsdException w:name=\"Subtle Reference\" w:uiPriority=\"31\" w:qFormat=\"1\"/><w:lsdException w:name=\"Intense Reference\" w:uiPriority=\"32\" w:qFormat=\"1\"/><w:lsdException w:name=\"Book Title\" w:uiPriority=\"33\" w:qFormat=\"1\"/><w:lsdException w:name=\"Bibliography\" w:semiHidden=\"1\" w:uiPriority=\"37\" w:unhideWhenUsed=\"1\"/><w:lsdException w:name=\"TOC Heading\" w:semiHidden=\"1\" w:uiPriority=\"39\" w:unhideWhenUsed=\"1\" w:qFormat=\"1\"/><w:lsdException w:name=\"Plain Table 1\" w:uiPriority=\"41\"/><w:lsdException w:name=\"Plain Table 2\" w:uiPriority=\"42\"/><w:lsdException w:name=\"Plain Table 3\" w:uiPriority=\"43\"/><w:lsdException w:name=\"Plain Table 4\" w:uiPriority=\"44\"/><w:lsdException w:name=\"Plain Table 5\" w:uiPriority=\"45\"/><w:lsdException w:name=\"Grid Table Light\" w:uiPriority=\"40\"/><w:lsdException w:name=\"Grid Table 1 Light\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful\" w:uiPriority=\"52\"/><w:lsdException w:name=\"Grid Table 1 Light Accent 1\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2 Accent 1\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3 Accent 1\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4 Accent 1\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark Accent 1\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful Accent 1\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful Accent 1\" w:uiPriority=\"52\"/><w:lsdException w:name=\"Grid Table 1 Light Accent 2\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2 Accent 2\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3 Accent 2\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4 Accent 2\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark Accent 2\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful Accent 2\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful Accent 2\" w:uiPriority=\"52\"/><w:lsdException w:name=\"Grid Table 1 Light Accent 3\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2 Accent 3\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3 Accent 3\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4 Accent 3\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark Accent 3\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful Accent 3\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful Accent 3\" w:uiPriority=\"52\"/><w:lsdException w:name=\"Grid Table 1 Light Accent 4\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2 Accent 4\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3 Accent 4\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4 Accent 4\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark Accent 4\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful Accent 4\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful Accent 4\" w:uiPriority=\"52\"/><w:lsdException w:name=\"Grid Table 1 Light Accent 5\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2 Accent 5\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3 Accent 5\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4 Accent 5\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark Accent 5\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful Accent 5\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful Accent 5\" w:uiPriority=\"52\"/><w:lsdException w:name=\"Grid Table 1 Light Accent 6\" w:uiPriority=\"46\"/><w:lsdException w:name=\"Grid Table 2 Accent 6\" w:uiPriority=\"47\"/><w:lsdException w:name=\"Grid Table 3 Accent 6\" w:uiPriority=\"48\"/><w:lsdException w:name=\"Grid Table 4 Accent 6\" w:uiPriority=\"49\"/><w:lsdException w:name=\"Grid Table 5 Dark Accent 6\" w:uiPriority=\"50\"/><w:lsdException w:name=\"Grid Table 6 Colorful Accent 6\" w:uiPriority=\"51\"/><w:lsdException w:name=\"Grid Table 7 Colorful Accent 6\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light Accent 1\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2 Accent 1\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3 Accent 1\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4 Accent 1\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark Accent 1\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful Accent 1\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful Accent 1\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light Accent 2\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2 Accent 2\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3 Accent 2\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4 Accent 2\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark Accent 2\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful Accent 2\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful Accent 2\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light Accent 3\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2 Accent 3\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3 Accent 3\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4 Accent 3\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark Accent 3\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful Accent 3\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful Accent 3\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light Accent 4\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2 Accent 4\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3 Accent 4\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4 Accent 4\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark Accent 4\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful Accent 4\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful Accent 4\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light Accent 5\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2 Accent 5\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3 Accent 5\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4 Accent 5\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark Accent 5\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful Accent 5\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful Accent 5\" w:uiPriority=\"52\"/><w:lsdException w:name=\"List Table 1 Light Accent 6\" w:uiPriority=\"46\"/><w:lsdException w:name=\"List Table 2 Accent 6\" w:uiPriority=\"47\"/><w:lsdException w:name=\"List Table 3 Accent 6\" w:uiPriority=\"48\"/><w:lsdException w:name=\"List Table 4 Accent 6\" w:uiPriority=\"49\"/><w:lsdException w:name=\"List Table 5 Dark Accent 6\" w:uiPriority=\"50\"/><w:lsdException w:name=\"List Table 6 Colorful Accent 6\" w:uiPriority=\"51\"/><w:lsdException w:name=\"List Table 7 Colorful Accent 6\" w:uiPriority=\"52\"/></w:latentStyles><w:style w:type=\"paragraph\" w:default=\"1\" w:styleId=\"a\"><w:name w:val=\"Normal\"/><w:qFormat/><w:pPr><w:bidi/></w:pPr></w:style><w:style w:type=\"character\" w:default=\"1\" w:styleId=\"a0\"><w:name w:val=\"Default Paragraph Font\"/><w:uiPriority w:val=\"1\"/><w:semiHidden/><w:unhideWhenUsed/></w:style><w:style w:type=\"table\" w:default=\"1\" w:styleId=\"a1\"><w:name w:val=\"Normal Table\"/><w:uiPriority w:val=\"99\"/><w:semiHidden/><w:unhideWhenUsed/><w:tblPr><w:tblInd w:w=\"0\" w:type=\"dxa\"/><w:tblCellMar><w:top w:w=\"0\" w:type=\"dxa\"/><w:left w:w=\"108\" w:type=\"dxa\"/><w:bottom w:w=\"0\" w:type=\"dxa\"/><w:right w:w=\"108\" w:type=\"dxa\"/></w:tblCellMar></w:tblPr></w:style><w:style w:type=\"numbering\" w:default=\"1\" w:styleId=\"a2\"><w:name w:val=\"No List\"/><w:uiPriority w:val=\"99\"/><w:semiHidden/><w:unhideWhenUsed/></w:style></w:styles>";
                string word_webSettings = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<w:webSettings xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" xmlns:w14=\"http://schemas.microsoft.com/office/word/2010/wordml\" xmlns:w15=\"http://schemas.microsoft.com/office/word/2012/wordml\" mc:Ignorable=\"w14 w15\"><w:optimizeForBrowser/><w:allowPNG/></w:webSettings>";
                for (int i = 0; i < CheckBoxes.Length; i++)
                    if (((CheckBox)Sura.Items[i + 1]).IsChecked != false)
                    {
                        word_document.Append("<w:p w:rsidR=\"0027662C\" w:rsidRDefault=\"00282EED\" w:rsidP=\"0027662C\"><w:pPr><w:widowControl w:val=\"0\"/><w:autoSpaceDE w:val=\"0\"/><w:autoSpaceDN w:val=\"0\"/><w:adjustRightInd w:val=\"0\"/><w:spacing w:after=\"0\" w:line=\"240\" w:lineRule=\"auto\"/><w:jc w:val=\"center\"/><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"000000\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/><w:rtl/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"008000\"/><w:sz w:val=\"27\"/><w:szCs w:val=\"27\"/><w:rtl/></w:rPr><w:t>سورة ");
                        word_document.Append(MainWindow.Sura_Name[i]);
                        word_document.Append("</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"000000\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/></w:rPr><w:br/></w:r>");
                        if (i != 0 && i != 8)
                        {
                            word_document.Append("<w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"FF0000\"/><w:sz w:val=\"27\"/><w:szCs w:val=\"21\"/><w:rtl/></w:rPr><w:t>");
                            word_document.Append(Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"));
                            word_document.Append("</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"000000\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/></w:rPr><w:br/></w:r>");
                        }
                        int old_j = -1;
                        bool is_changed = false;
                        for (int j = 0; j < CheckBoxes[i].Count; j++)
                            if (CheckBoxes[i][j].IsChecked == true)
                            {
                                if (!is_changed) is_changed = true;
                                if (old_j != j - 1)
                                    word_document.Append("<w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"000000\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/></w:rPr><w:br/></w:r>");
                                word_document.Append("<w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\"/><w:color w:val=\"000000\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/><w:rtl/></w:rPr><w:t>");
                                word_document.Append(Replace_Accent(MainWindow.Ayaat[i][j]));
                                word_document.Append("(" + (j + 1) + ") ");
                                word_document.Append("</w:t></w:r><w:r><w:rPr><w:rFonts w:ascii=\"Courier New\" w:eastAsia=\"Times New Roman\" w:hAnsi=\"Courier New\" w:cs=\"Courier New\" w:hint=\"cs\"/><w:color w:val=\"000000\"/><w:sz w:val=\"21\"/><w:szCs w:val=\"21\"/><w:rtl/></w:rPr><w:t xml:space=\"preserve\"> </w:t></w:r>");
                                old_j = j;
                            }
                            else if (!is_changed)
                                old_j++;
                        word_document.Append("</w:p>");
                    }
                word_document.Append("<w:sectPr w:rsidR=\"0027662C\" w:rsidRPr=\"0027662C\" w:rsidSect=\"00E9607A\"><w:pgSz w:w=\"11906\" w:h=\"16838\"/><w:pgMar w:top=\"1440\" w:right=\"1800\" w:bottom=\"1440\" w:left=\"1800\" w:header=\"708\" w:footer=\"708\" w:gutter=\"0\"/><w:cols w:space=\"708\"/><w:bidi/><w:rtlGutter/><w:docGrid w:linePitch=\"360\"/></w:sectPr></w:body></w:document>");
                Directory.CreateDirectory(directory_name + "\\_rels");
                Directory.CreateDirectory(directory_name + "\\docProps");
                Directory.CreateDirectory(directory_name + "\\word");
                Directory.CreateDirectory(directory_name + "\\word\\_rels");
                Directory.CreateDirectory(directory_name + "\\word\\theme");
                StreamWriter SW = new StreamWriter(directory_name + "\\[Content_Types].xml");
                SW.Write(Content_Types);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\_rels\\.rels");
                SW.Write(_rels__rels);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\docProps\\app.xml");
                SW.Write(docProps_apps);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\docProps\\core.xml");
                SW.Write(docProps_core);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\_rels\\document.xml.rels");
                SW.Write(word__rels_document_xml);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\theme\\theme1.xml");
                SW.Write(word_thems_thems1);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\fontTable.xml");
                SW.Write(word_fontTable);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\document.xml");
                SW.Write(word_document);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\settings.xml");
                SW.Write(word_setting);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\styles.xml");
                SW.Write(word_styles);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\word\\webSettings.xml");
                SW.Write(word_webSettings);
                SW.Close();
                string file_list = directory_name + "\\_rels\\\n" + directory_name + "\\docProps\\\n" + directory_name + "\\word\\\n" + directory_name + "\\[Content_Types].xml";
                SW = new StreamWriter(directory_name + "\\filelist.txt");
                SW.Write(file_list);
                SW.Close();
                var PSI = new System.Diagnostics.ProcessStartInfo("7za.exe", " a -tzip \"" + path + "\" " + "@" + "\"" + directory_name + "\\filelist.txt" + "\"");
                System.Diagnostics.Process.Start(PSI).WaitForExit();
                new DirectoryInfo(directory_name).Delete(true);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                try { new DirectoryInfo(directory_name).Delete(true); }
                catch (Exception) { }
                return false;
            }
        }

        private bool Make_ODT_File(string path)
        {
            int rand = new Random((int)DateTime.Now.Ticks).Next();
            string directory_name = Directory.CreateDirectory(path + rand).FullName;
            try
            {
                string META_INF_manifest = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<manifest:manifest xmlns:manifest=\"urn:oasis:names:tc:opendocument:xmlns:manifest:1.0\" manifest:version=\"1.2\">\n <manifest:file-entry manifest:media-type=\"application/vnd.oasis.opendocument.text\" manifest:version=\"1.2\" manifest:full-path=\"/\"/>\n <manifest:file-entry manifest:media-type=\"text/xml\" manifest:full-path=\"content.xml\"/>\n <manifest:file-entry manifest:media-type=\"\" manifest:full-path=\"Configurations2/accelerator/current.xml\"/>\n <manifest:file-entry manifest:media-type=\"application/vnd.sun.xml.ui.configuration\" manifest:full-path=\"Configurations2/\"/>\n</manifest:manifest>";
                StringBuilder content = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<office:document-content xmlns:office=\"urn:oasis:names:tc:opendocument:xmlns:office:1.0\" xmlns:style=\"urn:oasis:names:tc:opendocument:xmlns:style:1.0\" xmlns:text=\"urn:oasis:names:tc:opendocument:xmlns:text:1.0\" xmlns:table=\"urn:oasis:names:tc:opendocument:xmlns:table:1.0\" xmlns:draw=\"urn:oasis:names:tc:opendocument:xmlns:drawing:1.0\" xmlns:fo=\"urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:meta=\"urn:oasis:names:tc:opendocument:xmlns:meta:1.0\" xmlns:number=\"urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0\" xmlns:svg=\"urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0\" xmlns:chart=\"urn:oasis:names:tc:opendocument:xmlns:chart:1.0\" xmlns:dr3d=\"urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0\" xmlns:math=\"http://www.w3.org/1998/Math/MathML\" xmlns:form=\"urn:oasis:names:tc:opendocument:xmlns:form:1.0\" xmlns:script=\"urn:oasis:names:tc:opendocument:xmlns:script:1.0\" xmlns:ooo=\"http://openoffice.org/2004/office\" xmlns:ooow=\"http://openoffice.org/2004/writer\" xmlns:oooc=\"http://openoffice.org/2004/calc\" xmlns:dom=\"http://www.w3.org/2001/xml-events\" xmlns:xforms=\"http://www.w3.org/2002/xforms\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:rpt=\"http://openoffice.org/2005/report\" xmlns:of=\"urn:oasis:names:tc:opendocument:xmlns:of:1.2\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns:grddl=\"http://www.w3.org/2003/g/data-view#\" xmlns:tableooo=\"http://openoffice.org/2009/table\" xmlns:field=\"urn:openoffice:names:experimental:ooo-ms-interop:xmlns:field:1.0\" xmlns:formx=\"urn:openoffice:names:experimental:ooxml-odf-interop:xmlns:form:1.0\" xmlns:css3t=\"http://www.w3.org/TR/css3-text/\" office:version=\"1.2\"><office:scripts/><office:font-face-decls><style:font-face style:name=\"Tahoma1\" svg:font-family=\"Tahoma\" style:font-family-generic=\"swiss\"/><style:font-face style:name=\"Courier New\" svg:font-family=\"&apos;Courier New&apos;\" style:font-family-generic=\"modern\" style:font-pitch=\"fixed\"/><style:font-face style:name=\"Times New Roman\" svg:font-family=\"&apos;Times New Roman&apos;\" style:font-family-generic=\"roman\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Arial\" svg:font-family=\"Arial\" style:font-family-generic=\"swiss\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Segoe UI\" svg:font-family=\"&apos;Segoe UI&apos;\" style:font-family-generic=\"swiss\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Microsoft YaHei\" svg:font-family=\"&apos;Microsoft YaHei&apos;\" style:font-family-generic=\"system\" style:font-pitch=\"variable\"/><style:font-face style:name=\"SimSun\" svg:font-family=\"SimSun\" style:font-family-generic=\"system\" style:font-pitch=\"variable\"/><style:font-face style:name=\"Tahoma\" svg:font-family=\"Tahoma\" style:font-family-generic=\"system\" style:font-pitch=\"variable\"/></office:font-face-decls><office:automatic-styles><style:style style:name=\"P1\" style:family=\"paragraph\" style:parent-style-name=\"Standard\"><style:paragraph-properties fo:text-align=\"center\" style:justify-single-word=\"false\"/><style:text-properties style:font-name-complex=\"Segoe UI\"/></style:style><style:style style:name=\"T1\" style:family=\"text\"><style:text-properties fo:color=\"#ff0000\"/></style:style><style:style style:name=\"T2\" style:family=\"text\"><style:text-properties fo:color=\"#00ae00\" fo:font-size=\"14pt\" fo:font-weight=\"normal\" style:font-size-asian=\"14pt\" style:font-weight-asian=\"normal\" style:font-size-complex=\"14pt\" style:font-weight-complex=\"normal\"/></style:style><style:style style:name=\"T3\" style:family=\"text\"><style:text-properties fo:color=\"#00ae00\" fo:font-size=\"14pt\" style:font-size-asian=\"14pt\" style:font-size-complex=\"14pt\"/></style:style></office:automatic-styles><office:body><office:text><text:sequence-decls><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Illustration\"/><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Table\"/><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Text\"/><text:sequence-decl text:display-outline-level=\"0\" text:name=\"Drawing\"/></text:sequence-decls>");
                for (int i = 0; i < CheckBoxes.Length; i++)
                    if (((CheckBox)Sura.Items[i + 1]).IsChecked != false)
                    {
                        content.Append("<text:p text:style-name=\"P1\"><text:span text:style-name=\"T2\">سورة ");
                        content.Append(MainWindow.Sura_Name[i]);
                        content.Append("<text:line-break/></text:span>");
                        if (i != 0 && i != 8)
                            content.Append("<text:span text:style-name=\"T1\">" + Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ") + "<text:line-break/></text:span>");
                        int old_j = -1;
                        bool is_changed = false;
                        for (int j = 0; j < CheckBoxes[i].Count; j++)
                            if (CheckBoxes[i][j].IsChecked == true)
                            {
                                if (!is_changed) is_changed = true;
                                if (old_j != j - 1)
                                    content.Append("<text:line-break/>");
                                content.Append(Replace_Accent(MainWindow.Ayaat[i][j]));
                                content.Append("(" + (j + 1) + ") ");
                                old_j = j;
                            }
                            else if (!is_changed)
                                old_j++;
                        content.Append("</text:p>");
                    }
                content.Append("</office:text></office:body></office:document-content>");
                Directory.CreateDirectory(directory_name + "\\META-INF");
                StreamWriter SW = new StreamWriter(directory_name + "\\META-INF\\manifest.xml");
                SW.Write(META_INF_manifest);
                SW.Close();
                SW = new StreamWriter(directory_name + "\\content.xml");
                SW.Write(content);
                SW.Close();
                string file_list = directory_name + "\\content.xml\n" + directory_name + "\\META-INF\\\n";
                SW = new StreamWriter(directory_name + "\\filelist.txt");
                SW.Write(file_list);
                SW.Close();
                var PSI = new System.Diagnostics.ProcessStartInfo("7za.exe", " a -tzip \"" + path + "\" " + "@" + "\"" + directory_name + "\\filelist.txt" + "\"");
                System.Diagnostics.Process.Start(PSI).WaitForExit();
                new DirectoryInfo(directory_name).Delete(true);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                try { new DirectoryInfo(directory_name).Delete(true); }
                catch (Exception) { }
                return false;
            }
        }

        private string Orginize_Date(int parameter, int max)
        {
            if (parameter < max)
                return "0" + parameter;
            else
                return parameter.ToString();
        }
    }
}