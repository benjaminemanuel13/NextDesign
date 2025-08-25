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

        const int CellSize = 16;
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
            select.Enabled = true;
            not.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            var count = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = j * CellSize;
                    int y = i * CellSize;

                    var (r, gVal, b) = ConvertRGB332ToRGB888((byte)Tile8X8Form.Pallette.Colors[colors[count++]]);
                    using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                    {
                        g.FillRectangle(brush, x, y, CellSize, CellSize);
                        g.DrawRectangle(Pens.WhiteSmoke, x, y, CellSize, CellSize);
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = j * CellSize;
                    int y = i * CellSize;
                    var left = x + (CellSize * 8);

                    var (r, gVal, b) = ConvertRGB332ToRGB888((byte)Tile8X8Form.Pallette.Colors[colors[count++]]);
                    using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                    {
                        g.FillRectangle(brush, left, y, CellSize, CellSize);
                        g.DrawRectangle(Pens.WhiteSmoke, left, y, CellSize, CellSize);
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = j * CellSize;
                    int y = i * CellSize;
                    var top = y + (CellSize * 8);

                    var (r, gVal, b) = ConvertRGB332ToRGB888((byte)Tile8X8Form.Pallette.Colors[colors[count++]]);
                    using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                    {
                        g.FillRectangle(brush, x, top, CellSize, CellSize);
                        g.DrawRectangle(Pens.WhiteSmoke, x, top, CellSize, CellSize);
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int x = j * CellSize;
                    int y = i * CellSize;
                    var left = x + (CellSize * 8);
                    var top = y + (CellSize * 8);

                    var (r, gVal, b) = ConvertRGB332ToRGB888((byte)Tile8X8Form.Pallette.Colors[colors[count++]]);
                    using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                    {
                        g.FillRectangle(brush, left, top, CellSize, CellSize);
                        g.DrawRectangle(Pens.WhiteSmoke, left, top, CellSize, CellSize);
                    }
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
                int index = 0;

                if (row < 8 && col < 8)
                {
                    index = row * 8 + col;
                }
                else if (row < 8 && col >= 8)
                {
                    index = row * 8 + (col - 8) + 64;
                }
                else if (row >= 8 && col < 8)
                {
                    index = (row - 8) * 8 + col + 128;
                }
                else
                {
                    index = (row - 8) * 8 + (col - 8) + 192;
                }

                var (r, g, b) = ConvertRGB332ToRGB888((byte)index);

                string tip = $"Index: 0x{index:X2}\nRGB888: ({r}, {g}, {b})";
                currentIndex = (byte)index;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int col = e.X / CellSize;
            int row = e.Y / CellSize;

            if (col >= 0 && col < GridSize && row >= 0 && row < GridSize)
            {
                colors[currentIndex] = Tile8X8Form.theIndex;
                this.Invalidate();
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

            var count = 0;
            for (int z = 0; z < 4; z++)
            {
                if (z == 2)
                {
                    count = 128;
                }
                else if (z == 3)
                {
                    count = 192;
                }

                for (var x = 0; x < 8; x++)
                {
                    var line = "db ";
                    for (var y = 0; y < 8; y += 2)
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

                if (z == 1)
                {
                    count = 8;
                }

                sb.AppendLine();
            }

            text.Text = sb.ToString().TrimEnd(',');
        }

        private void save_Click(object sender, EventArgs e)
        {
            var tile = _context.Tiles16.Where(x => x.Id == CurrentTile.Id).First();
            tile.Pixels = new byte[CurrentTile.Pixels.Length];

            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = i * 8 + j;
                    tile.Pixels[index] = colors[count++];
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = (i * 8 + j) + 64;
                    tile.Pixels[index] = colors[count++];
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = (i * 8 + j) + (64 * 2);
                    tile.Pixels[index] = colors[count++];
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = (i * 8 + j) + (64 * 3);
                    tile.Pixels[index] = colors[count++];
                }
            }

            bool hasChanges = _context.ChangeTracker.HasChanges();
            int affected = _context.SaveChanges(); // should be > 0
        }

        private void select_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;

            var tile = _context.Tiles16.Where(x => x.Id == CurrentTile.Id).First();

            MainForm.TileMapForm.CurrentTile = tile;
        }

        private void Tile16x16Form_Load(object sender, EventArgs e)
        {

        }
    }
}
