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
        const int CellSize = 20;
        const int GridSize = 40;

        public byte[] tiles = new byte[1280];
        private int currentIndex = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TileBase CurrentTile { get; set; }

        public TileMapForm()
        {
            InitializeComponent();
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
                    g.DrawString(tiles[i].ToString(), DefaultFont, brush, new Point(x, y));
                }

                g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
            }
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

            if (CurrentTile is Tile8x8)
            {
            }
            else if (CurrentTile is Tile16x16)
            {
            }

            tiles[currentIndex] = 1;

            int x = (currentIndex % GridSize) * CellSize;
            int y = (currentIndex / GridSize) * CellSize;

            Graphics g = this.CreateGraphics();

            using (Brush brush = new SolidBrush(Color.White))
            {
                g.FillRectangle(brush, x, y, CellSize, CellSize);
            }
            
            using (Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
            {
                g.DrawString(tiles[currentIndex].ToString(), DefaultFont, brush, new Point(x, y));
            }
        }

        private void TileMapForm_Load(object sender, EventArgs e)
        {

        }
    }
}
