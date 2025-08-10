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

namespace Colors
{
    public partial class Tile16x16Form : Form
    {
        const int CellSize = 20;
        const int GridSize = 16;

        public byte[] colors = new byte[256];

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pallette Pallette { get; set; }

        public Tile16x16Form()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            for (int i = 0; i < 256; i++)
            {
                int x = (i % GridSize) * CellSize;
                int y = (i / GridSize) * CellSize;

                var (r, gVal, b) = ConvertRGB332ToRGB888((byte)Pallette.Colors[colors[i]]);
                using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                {
                    g.FillRectangle(brush, x, y, CellSize, CellSize);
                    g.DrawRectangle(Pens.WhiteSmoke, x, y, CellSize, CellSize);
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
    }
}
