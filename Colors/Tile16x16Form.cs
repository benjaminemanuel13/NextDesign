using Colors.Models;
using Microsoft.EntityFrameworkCore;
using SKcode.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Colors
{
    public partial class Tile16x16Form : Form
    {
        private readonly ProjectDBContext _context;

        private Tile16x16 CurrentTile { get; set; }

        const int CellSize = 20;
        const int GridSize = 16;

        public byte[] colors = new byte[256];

        private byte currentIndex = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pallette Pallette { get; set; }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tile8x8Form Tile8X8Form { get; set; }

        public Tile16x16Form()
        {
            InitializeComponent();

            _context = Program.Project;
        }

        public void SetTile(Tile16x16 tile)
        {
            if (tile == null) return;

            for (int i = 0; i < tile.Pixels.Length; i++)
            {
                colors[i] = tile.Pixels[i];
            }

            CurrentTile = tile;

            this.Invalidate();

            save.Enabled = true;
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int col = e.X / CellSize;
            int row = e.Y / CellSize;

            if (col >= 0 && col < GridSize && row >= 0 && row < GridSize)
            {
                int index = row * GridSize + col;
                var (r, g, b) = ConvertRGB332ToRGB888((byte)index);

                string tip = $"Index: 0x{index:X2}\nRGB888: ({r}, {g}, {b})";
                currentIndex = (byte)index;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            colors[currentIndex] = Tile8X8Form.theIndex;

            this.Invalidate();
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

            var count = 0;
            for (int z = 0; z < 4; z++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var line = "db ";
                    for (var y = 0; y < 8; y+= 2)
                    {
                        var hex1 = colors[count].ToString("X2");
                        var hex2 = colors[count + 1].ToString("X2");
                        string final = hex1.Substring(1, 1);
                        final += hex2.Substring(1, 1);
                        final += ", ";
                        line += final;
                        count += 2;
                    }

                    line = line.Substring(0, line.Length - 2);
                    sb.AppendLine(line);
                }
                sb.AppendLine();
            }

            text.Text = sb.ToString().TrimEnd(',');
        }

        private void save_Click(object sender, EventArgs e)
        {
            var tile = _context.Tiles16.Where(x => x.Id == CurrentTile.Id).First();
            tile.Pixels = new byte[CurrentTile.Pixels.Length];

            for (int i = 0; i < CurrentTile.Pixels.Length; i++)
            {
                tile.Pixels[i] = colors[i];
            }

            bool hasChanges = _context.ChangeTracker.HasChanges();
            int affected = _context.SaveChanges(); // should be > 0
        }
    }
}
