using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace urejevalnikBesedil_Zdjelar
{
    public class ListViewFont
    {
        public int FontSize { get; internal set; }
        public FontFamily FontFamily { get; internal set; }
        public SolidColorBrush Foreground { get; internal set; }
        public string Content { get; internal set; }

        public override string ToString()
        {
            return "Font: " + this.FontFamily + " je velik " + this.FontSize + " px";
        }
    }
}
