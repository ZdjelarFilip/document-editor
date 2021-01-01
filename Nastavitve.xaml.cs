using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace urejevalnikBesedil_Zdjelar
{
    /// <summary>
    /// Interaction logic for Nastavitve.xaml
    /// </summary>
    public partial class Nastavitve : Window
    {
        public Nastavitve()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnShrani_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dfg = new Microsoft.Win32.SaveFileDialog();
            if (dfg.ShowDialog() == true)
            {

                string font = fontCbx.SelectedItem.ToString();
                Properties.Settings.Default.PrivzetFont = font;

                string barvaFonta = barvaFontaCbx.SelectedItem.ToString();
                Properties.Settings.Default.PrivzetaBarvaFonta = barvaFonta;

                var velikostFonta = (ComboBoxItem)(velikostFontaCbx.SelectedItem);

                Properties.Settings.Default.PrivzetaVelikostFonta = Convert.ToInt32(velikostFonta.Content);
                Properties.Settings.Default.Save();

                this.Hide();
                this.Close();
            }
        }

        private void zadnjiDoc_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.OdpriPrejsnjiDoc = true;
        }

        private void noviDoc_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.OdpriNovDoc = true;
        }

        private void btnShraniDirektorij_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult r = dialog.ShowDialog();
            if (r.ToString() == "OK")
            {
                Properties.Settings.Default.PrivzetiDirektorij = dialog.SelectedPath;
                Properties.Settings.Default.Save();
                lokacijaShranjevanja.Text = dialog.SelectedPath.ToString();
            }            
        }

        private void barvaFontaCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void fontCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
