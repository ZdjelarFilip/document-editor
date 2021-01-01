using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace urejevalnikBesedil_Zdjelar
{
    /// <summary>
    /// Interaction logic for Uporabniski_Gradnik.xaml
    /// </summary>
    public partial class Uporabniski_Gradnik : UserControl
    {
        public static MainWindow mainWindow;
        public Uporabniski_Gradnik()
        {
            InitializeComponent();
        }

        public void buttonKrepko_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow == null)
            {
                return;
            }

            mainWindow.richTextBox.Selection.ApplyPropertyValue(FontWeightProperty, "Bold");
        }

        private void buttonLežeče_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow == null)
            {
                return;
            }
            mainWindow.richTextBox.Selection.ApplyPropertyValue(FontStyleProperty, "Italic");
        }

        private void buttonPodčrtano_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow == null)
            {
                return;
            }

            mainWindow.richTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void buttonUredi_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow == null)
            {
                return;
            }
            mainWindow.richTextBox.Selection.ApplyPropertyValue(FontStyleProperty, "Normal");
            mainWindow.richTextBox.Selection.ApplyPropertyValue(FontWeightProperty, "Normal");
            mainWindow.richTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);

            string stgfontBarvaa = Properties.Settings.Default.PrivzetaBarvaFonta;
            SolidColorBrush barvaa = (SolidColorBrush)new BrushConverter().ConvertFromString(stgfontBarvaa);
            mainWindow.richTextBox.Foreground = barvaa;

            string stgfontDruzina = Properties.Settings.Default.PrivzetFont;
            System.Windows.Media.FontFamily fontDruzina = new System.Windows.Media.FontFamily(stgfontDruzina);
            mainWindow.richTextBox.FontFamily = fontDruzina;


        }

        private void listViewBarva_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            if (mainWindow == null)
            {
                return;
            }

            var selected = listViewBarva.SelectedColor;

            var brush = new SolidColorBrush();
            brush.Color = System.Windows.Media.Color.FromArgb(selected.Value.A, selected.Value.R, selected.Value.G, selected.Value.B);

            mainWindow.richTextBox.Foreground = brush;
        }

        private void fontCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow == null)
            {
                return;
            }
            string stgfontDruzina = fontCbx.SelectedItem.ToString();

            System.Windows.Media.FontFamily fontDruzina = new System.Windows.Media.FontFamily(stgfontDruzina);
            mainWindow.richTextBox.FontFamily = fontDruzina;
        }

        private void velikostFontaCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainWindow == null)
            {
                return;
            }

            var item = (ComboBoxItem)velikostFontaCbx.SelectedValue;
            var content = (string)item.Content;

            mainWindow.richTextBox.FontSize = double.Parse(content);
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {

        }
    }
}


