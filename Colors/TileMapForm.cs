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

            int col = e.X / CellSize;
            int row = e.Y / CellSize;

            if (col >= 0 && col < GridSize && row >= 0 && row < GridSize)
            {
                int index = row * GridSize + col;
                currentIndex = index;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            tiles[currentIndex] = 1;// Tile8X8Form.theIndex;
            this.Invalidate();
        }

        private void TileMapForm_Load(object sender, EventArgs e)
        {

        }
    }
}
