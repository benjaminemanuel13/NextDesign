namespace Colors
{
    public partial class PalletteForm : Form
    {
        const int CellSize = 20;
        const int GridSize = 16;

        private ToolTip tooltip = new ToolTip();
        private byte currentIndex = 0;

        public byte CurrentIndex = 0;

        public PalletteForm()
        {
            InitializeComponent();

            this.ClientSize = new Size(CellSize * GridSize, CellSize * GridSize);
            this.Text = "RGB332 Palette Viewer";
            this.DoubleBuffered = true;

            tooltip.InitialDelay = 0;
            tooltip.ReshowDelay = 0;
            tooltip.AutoPopDelay = 5000;
            tooltip.ShowAlways = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int i = 0; i < 256; i++)
            {
                int x = (i % GridSize) * CellSize;
                int y = (i / GridSize) * CellSize;

                var (r, gVal, b) = ConvertRGB332ToRGB888((byte)i);
                using (Brush brush = new SolidBrush(Color.FromArgb(r, gVal, b)))
                {
                    g.FillRectangle(brush, x, y, CellSize, CellSize);
                    g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
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
                tooltip.SetToolTip(this, tip);
                currentIndex = (byte)index;
            }
            else
            {
                tooltip.SetToolTip(this, string.Empty);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            CurrentIndex = currentIndex;
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
