using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace urejevalnikBesedil_Zdjelar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ListViewFont> seznamFontov = new ObservableCollection<ListViewFont>();

        public MainWindow()
        {
            InitializeComponent();
            Uporabniski_Gradnik.mainWindow = this;

            string stgfontDruzina = Properties.Settings.Default.PrivzetFont;
            FontFamily fontDruzina = new FontFamily(stgfontDruzina);
            richTextBox.FontFamily = fontDruzina;

            string stgfontBarva = Properties.Settings.Default.PrivzetaBarvaFonta;
            SolidColorBrush barva = (SolidColorBrush)new BrushConverter().ConvertFromString(stgfontBarva);
            richTextBox.Foreground = barva;

            int fontVelikost = Properties.Settings.Default.PrivzetaVelikostFonta;
            richTextBox.FontSize = fontVelikost;

            var barvaFontaAqua = "Aqua";
            var barvaFontaRed = "Red";

            seznamFontov.Add(new ListViewFont()
            {
                FontSize = 13,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(barvaFontaAqua)
            });

            seznamFontov.Add(new ListViewFont()
            {
                FontSize = 13,
                FontFamily = new FontFamily("Times New Roman"),
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(barvaFontaRed)
            });

            listView.ItemsSource = seznamFontov;


            //Avtomatsko nalganje pod. iz dat.
            if (Properties.Settings.Default.OdpriPrejsnjiDoc == true)
            {
                Nastavitve nastavitve = new Nastavitve();

                string yx = Properties.Settings.Default.PrivzetiDirektorij;

                if (Properties.Settings.Default.OdpriNovDoc == true)
                {
                    string stgfontDruzinaa = Properties.Settings.Default.PrivzetFont;
                    FontFamily fontDruzinaa = new FontFamily(stgfontDruzinaa);
                    richTextBox.FontFamily = fontDruzinaa;

                    string stgfontBarvaa = Properties.Settings.Default.PrivzetaBarvaFonta;
                    SolidColorBrush barvaa = (SolidColorBrush)new BrushConverter().ConvertFromString(stgfontBarvaa);
                    richTextBox.Foreground = barvaa;

                    int fontVelikosta = Properties.Settings.Default.PrivzetaVelikostFonta;
                    richTextBox.FontSize = fontVelikosta;
                }

                else if (Properties.Settings.Default.OdpriPrejsnjiDoc == true)
                {
                    var doc = new XmlDocument();
                    if (yx != "")
                    {
                        doc.Load(yx);

                        XmlElement root = doc.DocumentElement;
                        string rootFontFamily = root.Attributes["FontFamily"].Value;
                        string rootFontSize = root.Attributes["FontSize"].Value;
                        string rootForeground = root.Attributes["Foreground"].Value;

                        //var rootFontStyle = root.Attributes["FontStyle"].Value;
                        //var rootFontWeight = root.Attributes["FontWeight"].Value;
                        string rootText = root.InnerText;

                        FontFamily fontDruzinaa = new FontFamily(rootFontFamily);
                        SolidColorBrush barvaa = (SolidColorBrush)new BrushConverter().ConvertFromString(rootForeground);

                        richTextBox.Foreground = barvaa;
                        richTextBox.FontFamily = fontDruzinaa;
                        richTextBox.FontSize = int.Parse(rootFontSize);
                        //richTextBox.FontStyle = rootFontStyle;
                        //richTextBox.FontWeight = rootFontWeight;

                        richTextBox.AppendText(rootText);
                    }
                    
                }                
            }      
        }        

        private void Datoteka_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Uredi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Orodja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Izhod_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.Close();
            Environment.Exit(0);
        }
        private void Shrani_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Odpri_Click(object sender, RoutedEventArgs e)
        {

        }

        private void richTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {      
        }

        private void buttonBarva_Click(object sender, RoutedEventArgs e)
        {
            buttonBarva.Focus();

            var rnd = new Random();
            var properties = typeof(Brushes).GetProperties();
            var count = properties.Count();

            var color = properties.Select(x => new { Property = x, Index = rnd.Next(count) })
                                  .OrderBy(x => x.Index)
                                  .First();

            richTextBox.Foreground = (SolidColorBrush)color.Property.GetValue(color, null);
        }

        private void richTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void richTextBox_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private void richTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            labelStatus.Text = "Vnašanje...";
        }

        private void richTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string rtb = StringFromRichTextBox(richTextBox);
            int count = 0;

            foreach (char item in rtb)
            {
                if (Char.IsLetterOrDigit(item) || !Char.IsWhiteSpace(item))
                {
                    count++;
                }
            }

            labelStatus.Text = "Št znakov: " + count;
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );
            return textRange.Text;
        }

        private void buttonKopiraj_Click(object sender, RoutedEventArgs e)
        {
            buttonKopiraj.Focus();
            string text = StringFromRichTextBox(richTextBox);

            Clipboard.SetText(text);
        }

        private void vse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            vse.Focus();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Nastavitve nastavitve = new Nastavitve();           


            if (nastavitve.ShowDialog() == true)
            {
                if (nastavitve.zadnjiDoc.IsChecked == true)
                {
                    Properties.Settings.Default.OdpriNovDoc = false;
                    Properties.Settings.Default.OdpriPrejsnjiDoc = true;
                    Properties.Settings.Default.Save();
                }

                else if (nastavitve.noviDoc.IsChecked == true == true)
                {
                    Properties.Settings.Default.OdpriPrejsnjiDoc = false;
                    Properties.Settings.Default.OdpriNovDoc = true;
                    Properties.Settings.Default.Save();
                }

                else if (nastavitve.fontCbx.SelectedItem != null || nastavitve.barvaFontaCbx.SelectedItem != null || nastavitve.velikostFontaCbx.SelectedItem != null)
                {
                    string font = nastavitve.fontCbx.SelectedItem.ToString();
                    FontFamily fontFamily = new FontFamily(font);
                    Properties.Settings.Default.PrivzetFont = font;

                    string barvaFonta = ((ComboBoxItem)nastavitve.barvaFontaCbx.SelectedItem).Content.ToString();
                    SolidColorBrush barva = (SolidColorBrush)new BrushConverter().ConvertFromString(barvaFonta);
                    Properties.Settings.Default.PrivzetaBarvaFonta = barvaFonta;

                    var velikostFonta = (ComboBoxItem)(nastavitve.velikostFontaCbx.SelectedItem);

                    Properties.Settings.Default.PrivzetaVelikostFonta = Convert.ToInt32(velikostFonta.Content);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            Stil stil = new Stil();

            if (stil.ShowDialog() == true)
            {
                string font = stil.fontiCbx.SelectedItem.ToString();
                FontFamily fontFamily = new FontFamily(font);

                string barvaFonta = ((ComboBoxItem)stil.barvaFontaCbx.SelectedItem).Content.ToString();
                SolidColorBrush barva = (SolidColorBrush)new BrushConverter().ConvertFromString(barvaFonta);

                var velikostFonta = (ComboBoxItem)(stil.velikostFontaCbx.SelectedItem);
                var a = velikostFonta.Content;

                ListViewFont neke = new ListViewFont
                {
                    FontFamily = fontFamily,
                    Foreground = barva,
                    FontSize = Convert.ToInt32(a),          
                };

                seznamFontov.Add(neke);
            }
        }

        private void buttonUredi_Click(object sender, RoutedEventArgs e)
        {
            Stil stil = new Stil();

            if (stil.ShowDialog() == true)
            {
                string font = stil.fontiCbx.SelectedItem.ToString();
                FontFamily fontFamily = new FontFamily(font);

                string barvaFonta = ((ComboBoxItem)stil.barvaFontaCbx.SelectedItem).Content.ToString();
                SolidColorBrush barva = (SolidColorBrush)new BrushConverter().ConvertFromString(barvaFonta);

                var velikostFonta = (ComboBoxItem)(stil.velikostFontaCbx.SelectedItem);
                var a = velikostFonta.Content;

                ListViewFont neke = new ListViewFont
                {
                    FontFamily = fontFamily,
                    Foreground = barva,
                    FontSize = Convert.ToInt32(a)                  
                };

                seznamFontov.Add(neke);
            }
        }

        private void listView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var a = listView.SelectedItem as ListViewFont;

            if ( a == null )
            {
                string stgfontDruzina = Properties.Settings.Default.PrivzetFont;
                FontFamily fontDruzina = new FontFamily(stgfontDruzina);
                richTextBox.FontFamily = fontDruzina;

                string stgfontBarva = Properties.Settings.Default.PrivzetaBarvaFonta;
                SolidColorBrush barva = (SolidColorBrush)new BrushConverter().ConvertFromString(stgfontBarva);
                richTextBox.Foreground = barva;

                int fontVelikost = Properties.Settings.Default.PrivzetaVelikostFonta;
                richTextBox.FontSize = fontVelikost;
            }
            else
            {
                richTextBox.FontFamily = a.FontFamily;
                richTextBox.Foreground = a.Foreground;
                richTextBox.FontSize = a.FontSize;
            }
        }        

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
        }

        #region Useless
        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void toolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void Razveljavi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Razveljavi_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Ponovi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void richTextBox_SelectionChanged_1(object sender, DataTransferEventArgs e)
        {
        }

        private void labelVnasanje_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        #endregion

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Uvozi_Click(object sender, RoutedEventArgs e)
        {           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Document (*.xml)|*.xml|RTF files (*.rtf)|*.rtf";
            var doc = new XmlDocument();
            if (openFileDialog.ShowDialog() == true)
            {
                doc.Load(openFileDialog.FileName);

                XmlElement root = doc.DocumentElement;
                string rootFontFamily = root.Attributes["FontFamily"].Value;
                string rootFontSize = root.Attributes["FontSize"].Value;
                string rootForeground = root.Attributes["Foreground"].Value;
                string rootText = root.InnerText;

                FontFamily fontDruzina = new FontFamily(rootFontFamily);
                SolidColorBrush barva = (SolidColorBrush)new BrushConverter().ConvertFromString(rootForeground);

                richTextBox.Foreground = barva;
                richTextBox.FontFamily = fontDruzina;
                richTextBox.FontSize = int.Parse(rootFontSize);

                //richTextBox.FontStyle = ;
                //richTextBox.FontWeight = fontDruzina;

                richTextBox.AppendText(rootText);             
            }            
        }

        private void Izvozi_Click(object sender, RoutedEventArgs e)
        {
            string savedRichTextBox = "";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Document (*.xml)|*.xml|RTF files (*.rtf)|*.rtf";
            if (saveFileDialog.ShowDialog() == true)
            {
                savedRichTextBox = XamlWriter.Save(richTextBox);
                File.WriteAllText(saveFileDialog.FileName, savedRichTextBox);
            }
            Properties.Settings.Default.PrivzetiDirektorij = saveFileDialog.FileName;
            Properties.Settings.Default.Save();
        }

        private void Uporabniski_gradnik_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Uporabniski_gradnik_Expanded(object sender, RoutedEventArgs e)
        {
        }
    }
}
