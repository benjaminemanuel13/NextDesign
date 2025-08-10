using Colors.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Colors
{
    public partial class Tile8x8Form : Form
    {
        const int CellSize = 20;
        const int GridSize = 8;

        public byte[] colors = new byte[64];

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pallette Pallette { get; set; }

        public Tile8x8Form()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int i = 0; i < 64; i++)
            {
                int x = (i % GridSize) * CellSize;
                int y = (i / GridSize) * CellSize;

                var index = Pallette.Colors[colors[i]];

                //var (r, gVal, b) = ConvertRGB332ToRGB888((byte)colors[i]);
                var (r, gVal, b) = ConvertRGB332ToRGB888((byte)index);
                using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                {
                    g.FillRectangle(brush, x, y, CellSize, CellSize);
                    g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
                }
            }

            for (int i = 0; i < 16; i++)
            {
                var (r, gVal, b) = ConvertRGB332ToRGB888((byte)Pallette.Colors[i]);
                using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                {
                    g.FillRectangle(brush, (i * 20) + 8, 350, CellSize, CellSize);
                    g.DrawRectangle(Pens.Black, (i * 20) + 8, 350, CellSize, CellSize);
                }
            }
        }

        private static (byte r, byte g, byte b) ConvertRGB332ToRGB888(byte rgb332)
        {
            byte r3 = (byte)((rgb332 >> 5) & 0b00000111);
            byte g3 = (byte)((rgb332 >> 2) & 0b00000111);
            byte b2 = (byte)(rgb332 & 0b00000011);

            byte r = (byte)((r3 * 255) / 7);
            byte g = (byte)((g3 * 255) / 7);
            byte b = (byte)((b2 * 255) / 3);

            return (r, g, b);
        }

        private void generate_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            string line = "";

            for (var y = 2; y <= 64; y += 2)
            {
                if (y % 8 == 0 && y > 0)
                {
                    sb.Append("db ");

                    var hex1 = colors[y - 2].ToString("X2");
                    var hex2 = colors[y - 1].ToString("X2");

                    line += hex1.Substring(1, 1);
                    line += hex2.Substring(1, 1);
                    sb.Append(line + "\r\n");
                    line = "";
                }
                else
                {
                    var hex1 = colors[y - 2].ToString("X2");
                    var hex2 = colors[y - 1].ToString("X2");
                    line += hex1.Substring(1, 1) + hex2.Substring(1, 1) + ", ";
                }
            }

            text.Text = sb.ToString().TrimEnd(',');
        }
    }
}
