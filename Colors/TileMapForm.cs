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
    public partial class TileMapForm : Form
    {
        const int CellSize = 16;
        const int GridSize = 40;

        public int[] tiles = new int[1280];
        private int currentIndex = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TileBase CurrentTile { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tile8x8Form Tile8X8Form { get; set; }

        public TileMapForm()
        {
            InitializeComponent();

            var thesetiles = Program.Project.TilesMaps.First();

            var count = 0;
            foreach(var id in thesetiles.LookupIds)
            {
                tiles[count++] = id;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int i = 0; i < 1280; i++)
            {
                int x = (i % GridSize) * CellSize;
                int y = (i / GridSize) * CellSize;

                //var (r, gVal, b) = ConvertRGB332ToRGB888((byte)colors[i]);
                //using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                //{
                //    g.FillRectangle(brush, x, y, CellSize, CellSize);
                //}

                using (Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                {
                    if (tiles[i] != 0)
                    {
                        //g.DrawString(tiles[i].ToString(), DefaultFont, brush, new Point(x, y));

                        DrawTile(i, x, y, g);
                    }
                }

                g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
            }
        }

        private void DrawTile(int i, int x, int y, Graphics g)
        {
            var id = tiles[i];
            var lookup = Program.Project.TileLookups.Find(id);

            if (lookup.Type == TileType.Tile8x8)
            {
                // Just Draw 8x8
                id = lookup.TileId;

                var tile = Program.Project.Tiles8.Find(id);

                // Draw the sucker

                var count = 0;
                for (int j = 0; j < CellSize; j += 2)
                {
                    for (int k = 0; k < CellSize; k += 2)
                    {
                        byte color = Tile8X8Form.Pallette.Colors[tile.Pixels[count++]];

                        var (r, gr, b) = ConvertRGB332ToRGB888(color);

                        using (Brush pixelbrush = new SolidBrush(Color.FromArgb(r, gr, b)))
                        {
                            g.FillRectangle(pixelbrush, x + k, y + j, 2, 2);
                        }
                    }
                }
            }
            else if (lookup.Type == TileType.Tile16x16)
            {
                // Draw 16x16 Tile
                // Set tiles[i + 1] to -1
                // Set tiles[i + 40] to -1
                // Set tiles[i + 41] to -1
                id = lookup.TileId;

                var tile = Program.Project.Tiles16.Find(id);

                // Draw the sucker

                var count = 0;
                for (int j = 0; j < CellSize; j += 2)
                {
                    for (int k = 0; k < CellSize; k += 2)
                    {
                        byte color = Tile8X8Form.Pallette.Colors[tile.Pixels[count++]];

                        var (r, gr, b) = ConvertRGB332ToRGB888(color);

                        using (Brush pixelbrush = new SolidBrush(Color.FromArgb(r, gr, b)))
                        {
                            g.FillRectangle(pixelbrush, x + k, y + j, 2, 2);
                        }
                    }
                }
                int left = x + CellSize;
                for (int j = 0; j < CellSize; j += 2)
                {
                    for (int k = 0; k < CellSize; k += 2)
                    {
                        byte color = Tile8X8Form.Pallette.Colors[tile.Pixels[count++]];
                        tiles[i + 1] = 0;

                        var (r, gr, b) = ConvertRGB332ToRGB888(color);

                        using (Brush pixelbrush = new SolidBrush(Color.FromArgb(r, gr, b)))
                        {
                            g.FillRectangle(pixelbrush, left + k, y + j, 2, 2);
                        }
                    }
                }

                int top = y + CellSize;
                for (int j = 0; j < CellSize; j += 2)
                {
                    for (int k = 0; k < CellSize; k += 2)
                    {
                        byte color = Tile8X8Form.Pallette.Colors[tile.Pixels[count++]];
                        tiles[i + 40] = 0;

                        var (r, gr, b) = ConvertRGB332ToRGB888(color);

                        using (Brush pixelbrush = new SolidBrush(Color.FromArgb(r, gr, b)))
                        {
                            g.FillRectangle(pixelbrush, x + k, top + j, 2, 2);
                        }
                    }
                }

                for (int j = 0; j < CellSize; j += 2)
                {
                    for (int k = 0; k < CellSize; k += 2)
                    {
                        byte color = Tile8X8Form.Pallette.Colors[tile.Pixels[count++]];
                        tiles[i + 41] = 0;

                        var (r, gr, b) = ConvertRGB332ToRGB888(color);

                        using (Brush pixelbrush = new SolidBrush(Color.FromArgb(r, gr, b)))
                        {
                            g.FillRectangle(pixelbrush, left + k, top + j, 2, 2);
                        }
                    }
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

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (CurrentTile == null) return;

            bool show4 = false;
            if (CurrentTile is Tile8x8)
            {
                show4 = false;
            }
            else if (CurrentTile is Tile16x16)
            {
                show4 = true;
            }

            int col = e.X / CellSize;
            int row = e.Y / CellSize;

            Graphics g = this.CreateGraphics();

            if (col >= 0 && col < GridSize && row >= 0 && row < 32)
            {
                int x = (currentIndex % GridSize) * CellSize;
                int y = (currentIndex / GridSize) * CellSize;

                g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);

                if (x < 39 * CellSize && show4) {
                    g.DrawRectangle(Pens.Black, x + CellSize, y, CellSize, CellSize);
                }
                if (y < 31 * CellSize && show4) {
                    g.DrawRectangle(Pens.Black, x, y + CellSize, CellSize, CellSize);
                }
                if (x < 39 * CellSize && y < 31 * CellSize && show4) {
                    g.DrawRectangle(Pens.Black, x + CellSize, y + CellSize, CellSize, CellSize);
                }
                
                int index = row * GridSize + col;
                currentIndex = index;

                x = (index % GridSize) * CellSize;
                y = (index / GridSize) * CellSize;

                g.DrawRectangle(Pens.LightGoldenrodYellow, x, y, CellSize, CellSize);

                if (x < 39 * CellSize && show4)
                {
                    g.DrawRectangle(Pens.LightGoldenrodYellow, x + CellSize, y, CellSize, CellSize);
                }
                if (y < 31 * CellSize && show4)
                {
                    g.DrawRectangle(Pens.LightGoldenrodYellow, x, y + CellSize, CellSize, CellSize);
                }
                if (x < 39 * CellSize && y < 31 * CellSize && show4)
                {
                    g.DrawRectangle(Pens.LightGoldenrodYellow, x + CellSize, y + CellSize, CellSize, CellSize);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (CurrentTile == null) return;

            int id = 0;
            if (CurrentTile is Tile8x8)
            {
                var tempId = ((Tile8x8)CurrentTile).Id;
                id = Program.Project.TileLookups.Where(x => x.Type == TileType.Tile8x8 && x.TileId == tempId).First().Id;
            }
            else if (CurrentTile is Tile16x16)
            {
                var tempId = ((Tile16x16)CurrentTile).Id;
                id = Program.Project.TileLookups.Where(x => x.Type == TileType.Tile16x16 && x.TileId == tempId).First().Id;
            }

            tiles[currentIndex] = id;

            int x = (currentIndex % GridSize) * CellSize;
            int y = (currentIndex / GridSize) * CellSize;

            Graphics g = this.CreateGraphics();

            //using (Brush brush = new SolidBrush(Color.White))
            //{
            //    g.FillRectangle(brush, x, y, CellSize, CellSize);
            //}

            //using (Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
            //{
            //    g.DrawString(tiles[currentIndex].ToString(), DefaultFont, brush, new Point(x, y));
            //}

            DrawTile(currentIndex, x, y, g);
        }

        private void TileMapForm_Load(object sender, EventArgs e)
        {

        }
    }
}
