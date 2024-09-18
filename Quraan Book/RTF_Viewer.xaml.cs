using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Interaction logic for RTF_Viewer.xaml
    /// </summary>
    public partial class RTF_Viewer : Window
    {
        System.Windows.Forms.OpenFileDialog OFD = new System.Windows.Forms.OpenFileDialog();
        public RTF_Viewer()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            OFD.Filter = "Rich File Document (*.rtf)|*.rtf";
            if (OFD.ShowDialog().Equals(System.Windows.Forms.DialogResult.OK))
            {
                try
                {
                    TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
                    FileStream FS = new FileStream(OFD.FileName, FileMode.Open, FileAccess.Read);
                    TextRange TR = new TextRange(Viewer.Document.ContentStart, Viewer.Document.ContentEnd);
                    TR.Load(FS, DataFormats.Rtf);
                    TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "خطأ", MessageBoxButton.OK, MessageBoxImage.Error); Close(); }
            }
            else
                this.Close();
        }
    }
}
