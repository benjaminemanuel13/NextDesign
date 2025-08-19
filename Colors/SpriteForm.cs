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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Colors
{
    public partial class SpriteForm : Form
    {
        private readonly ProjectDBContext _context;

        private Sprite CurrentSprite { get; set; }

        const int CellSize = 20;
        const int GridSize = 16;

        byte[] colors = new byte[256];

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PalletteForm PalletteForm { get; set; } = null!;

        private byte currentIndex = 0;

        public SpriteForm()
        {
            InitializeComponent();

            _context = Program.Project;

            Setup();
        }

        private void Setup()
        {
            for (int i = 0; i < 256; i++)
            {
                colors[i] = 0xE3;
            }
        }

        public void SetSprite(Sprite sprite)
        {
            if (sprite == null) return;

            for (int i = 0; i < sprite.Pixels.Length; i++)
            {
                colors[i] = sprite.Pixels[i];
            }

            CurrentSprite = sprite;

            this.Invalidate();

            save.Enabled = true;
            not.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int i = 0; i < 256; i++)
            {
                int x = (i % GridSize) * CellSize;
                int y = (i / GridSize) * CellSize;

                var (r, gVal, b) = ConvertRGB332ToRGB888((byte)colors[i]);
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
                //tooltip.SetToolTip(this, tip);
                currentIndex = (byte)index;
            }
            else
            {
                //tooltip.SetToolTip(this, string.Empty);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            colors[currentIndex] = PalletteForm.CurrentIndex;

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

            for (var y = 1; y <= 256; y++)
            {
                if (y % 16 == 0)
                {
                    sb.Append("db ");
                    var hex = colors[y - 1].ToString("X2");
                    line += hex;
                    sb.Append(line + "\r\n");
                    line = "";
                }
                else
                {
                    var hex = colors[y - 1].ToString("X2");
                    line += hex + ", ";
                }
            }

            textBox1.Text = sb.ToString().TrimEnd(',');
        }

        private void save_Click(object sender, EventArgs e)
        {
            var tile = _context.Sprites.Where(x => x.Id == CurrentSprite.Id).First();
            tile.Pixels = new byte[CurrentSprite.Pixels.Length];

            for (int i = 0; i < CurrentSprite.Pixels.Length; i++)
            {
                tile.Pixels[i] = colors[i];
            }

            bool hasChanges = _context.ChangeTracker.HasChanges();
            int affected = _context.SaveChanges(); // should be > 0
        }
    }
}
