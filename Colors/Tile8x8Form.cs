using Colors.Assistant.Plugin.Models;
using Colors.Models;
using SKcode.Data;
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
        private readonly ProjectDBContext _context;

        private Tile8x8 CurrentTile { get; set; }

        const int CellSize = 20;
        const int GridSize = 8;

        public int oldHighlight = -1;
        public int highlight = -1;

        const int PalletteGridSize = 16;

        public byte[] colors = new byte[64];

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Pallette Pallette { get; set; }

        private byte currentIndex = 0;
        public byte theIndex = 0;
        private byte drawByte = 0;

        public Tile8x8Form()
        {
            InitializeComponent();

            _context = ProjectDBContext.Project;
        }

        public void SetTile(Tile8x8 tile)
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

            for (int i = 0; i < 64; i++)
            {
                int x = (i % GridSize) * CellSize;
                int y = (i / GridSize) * CellSize;

                var index = Pallette.Colors[colors[i]];

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
                    g.FillRectangle(brush, (i * 20) + 8, 320, CellSize, CellSize);
                    g.DrawRectangle(Pens.Black, (i * 20) + 8, 320, CellSize, CellSize);
                    g.DrawString(i.ToString(), this.Font, Brushes.Black, (i * 20) + 8, 320);
                }
            }

            if(highlight != -1)
            {
                DrawHighlight();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int col = 0;
            if (e.Y > 319)
            {
                col = (e.X - 8) / CellSize;
            }
            else
            {
                col = e.X / CellSize;
            }

            int row = e.Y / CellSize;

            if (row == 16)
            {
                if (col >= 0 && col < PalletteGridSize)
                {
                    int index = row * PalletteGridSize + col;
                    var (r, g, b) = ConvertRGB332ToRGB888((byte)index);

                    string tip = $"Index: 0x{index:X2}\nRGB888: ({r}, {g}, {b})";
                    currentIndex = (byte)index;
                }
            }
            else if (col >= 0 && col < GridSize && row >= 0 && row < GridSize)
            {
                int index = row * GridSize + col;
                var (r, g, b) = ConvertRGB332ToRGB888((byte)index);

                string tip = $"Index: 0x{index:X2}\nRGB888: ({r}, {g}, {b})";
                //tooltip.SetToolTip(this, tip);
                drawByte = (byte)index;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int col = 0;
            if (e.Y > 319)
            {
                col = (e.X - 8) / CellSize;
            }
            else
            {
                col = e.X / CellSize;
            }

            int row = e.Y / CellSize;

            if (row == 16)
            {
                theIndex = currentIndex;
            }
            else if (col >= 0 && col < GridSize && row >= 0 && row < GridSize)
            {
                colors[drawByte] = theIndex;
            }

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

        private void save_Click(object sender, EventArgs e)
        {
            var tile = _context.Tiles8.Where(x => x.Id == CurrentTile.Id).First();
            tile.Pixels = new byte[CurrentTile.Pixels.Length];

            for (int i = 0; i < CurrentTile.Pixels.Length; i++)
            {
                tile.Pixels[i] = colors[i];
            }

            bool hasChanges = _context.ChangeTracker.HasChanges();
            int affected = _context.SaveChanges(); // should be > 0
        }

        private void select_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;

            var tile = _context.Tiles8.Where(x => x.Id == CurrentTile.Id).First();

            MainForm.TileMapForm.CurrentTile = tile;
        }

        public void MoveSelection(SelectionMove move)
        {

        }

        public void DrawHighlight()
        {
            if(highlight == -1)
            {
                highlight = 0;
            }

            var g = this.CreateGraphics();

            if (oldHighlight != -1)
            {
                int ax = (oldHighlight % GridSize) * CellSize;
                int ay = (oldHighlight / GridSize) * CellSize;

                g.DrawRectangle(Pens.Black, ax, ay, CellSize, CellSize);    
            }

            oldHighlight = highlight;

            int x = (highlight % GridSize) * CellSize;
            int y = (highlight / GridSize) * CellSize;

            g.DrawRectangle(Pens.Yellow, x, y, CellSize, CellSize);
        }

        public void StopHighlight()
        {
            if (oldHighlight != -1)
            {
                var g = this.CreateGraphics();
                int ax = (oldHighlight % GridSize) * CellSize;
                int ay = (oldHighlight / GridSize) * CellSize;
                g.DrawRectangle(Pens.Black, ax, ay, CellSize, CellSize);
            }
            oldHighlight = -1;
            highlight = -1;
        }

        public void SetHighlight(SelectionMove move)
        {
            if (move.Direction == SelectionMoveDirection.Up)
            {
            }
            else if (move.Direction == SelectionMoveDirection.Down)
            {
            }
            else if (move.Direction == SelectionMoveDirection.Left)
            {
            }
            else if (move.Direction == SelectionMoveDirection.Right)
            {
            }

            DrawHighlight();
        }
    }
}
