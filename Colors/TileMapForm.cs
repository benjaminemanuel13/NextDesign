using Colors.Assistant.Plugin.Models;
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
using static System.Net.Mime.MediaTypeNames;

namespace Colors
{
    public partial class TileMapForm : Form
    {
        //private string basePath = @"C:\Users\benja\source\Next\Game\";
        string BasePath = @"C:\Users\benja\source\Next\Colors\CoreGame\Game\";

        const int CellSize = 16;
        const int GridSize = 40;
        const int PathCellSize = 32;

        public int[] tiles = new int[1280];
        private int currentIndex = 0;

        private int currentCol = 0;
        private int currentRow = 0;
        private bool settingPathStep = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TileBase CurrentTile { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tile8x8Form Tile8X8Form { get; set; }

        public TileMapForm()
        {
            InitializeComponent();

            var thesetiles = ProjectDBContext.Project.TilesMaps.First();

            var count = 0;
            foreach (var id in thesetiles.LookupIds)
            {
                tiles[count++] = id;
            }

            mode.SelectedIndex = 0;

            GetPaths();
        }

        private void GetPaths()
        {
            var thesepaths = ProjectDBContext.Project.Paths.Include(x => x.Steps).ToList();

            paths.Items.Clear();
            foreach (var path in thesepaths)
            {
                paths.Items.Add(path);

                steps.Items.Clear();
                foreach (var step in path.Steps)
                {
                    steps.Items.Add(step);
                }
            }

            if(paths.Items.Count > 0)
            {
                paths.SelectedIndex = 0;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (mode.SelectedIndex == 0)
            {

                for (int i = 0; i < 1280; i++)
                {
                    int x = (i % GridSize) * CellSize;
                    int y = (i / GridSize) * CellSize;

                    using (Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                    {
                        if (tiles[i] != 0)
                        {
                            DrawTile(i, x, y, g);
                        }
                    }

                    g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
                }
            }
            else
            {
                for (int i = 0; i < 1280; i++)
                {
                    int x = (i % GridSize) * CellSize;
                    int y = (i / GridSize) * CellSize;

                    using (Brush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                    {
                        if (tiles[i] != 0)
                        {
                            DrawTile(i, x, y, g);
                        }
                    }
                }

                int top = 0;
                int left = 0;
                for (int i = 0; i < 32 / 2; i++)
                {
                    for (int j = 0; j < 40 / 2; j++)
                    {
                        int x = left * CellSize;
                        int y = top * CellSize;

                        using (Brush brush = new SolidBrush(Color.FromArgb(0xFF, 0xFF, 0xFF)))
                        {
                            var font = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
                            g.DrawString(j.ToString() + "," + i.ToString(), font, Brushes.White, x, y);
                        }

                        g.DrawRectangle(Pens.Black, x, y, CellSize * 2, CellSize * 2);

                        left += 2;
                    }
                    left = 0;
                    top += 2;
                }
            }
        }

        private void DrawTile(int i, int x, int y, Graphics g)
        {
            var id = tiles[i];
            var lookup = ProjectDBContext.Project.TileLookups.Find(id);

            if (lookup.Type == TileType.Tile8x8)
            {
                id = lookup.TileId;

                var tile = ProjectDBContext.Project.Tiles8.Find(id);

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
                id = lookup.TileId;

                var tile = ProjectDBContext.Project.Tiles16.Find(id);

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

        private void PathMouseMove(MouseEventArgs e)
        {
            int col = e.X / CellSize;
            int row = e.Y / CellSize;

            if (col >= 0 && col < GridSize && row >= 0 && row < 32)
            {
                col = col / 2;
                row = row / 2;

                currentCol = col;
                currentRow = row;
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mode.SelectedIndex == 1)
            {
                PathMouseMove(e);
                return;
            }

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

                if (x < 39 * CellSize && show4)
                {
                    g.DrawRectangle(Pens.Black, x + CellSize, y, CellSize, CellSize);
                }
                if (y < 31 * CellSize && show4)
                {
                    g.DrawRectangle(Pens.Black, x, y + CellSize, CellSize, CellSize);
                }
                if (x < 39 * CellSize && y < 31 * CellSize && show4)
                {
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

        private void PathsMouseClick(MouseEventArgs e)
        {
            if (settingPathStep == true)
            {
                var temppath = (Colors.Models.Path)paths.SelectedItem;

                Step newStep = new Step() { 
                    Name = "Step " + (temppath.Steps.Count + 1).ToString(),
                    PathId = temppath.Id,
                    X = currentCol,
                    Y = currentRow
                };

                var path = ProjectDBContext.Project.Paths.Find(temppath.Id);
                path.Steps.Add(newStep);

                ProjectDBContext.Project.SaveChanges();

                steps.Items.Add(newStep);

                settingPathStep = false;
                messages.Text = "(no message)";
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (mode.SelectedIndex == 1)
            {
                PathsMouseClick(e);
                return;
            }

            if (CurrentTile == null) return;

            int id = 0;
            if (CurrentTile is Tile8x8)
            {
                var tempId = ((Tile8x8)CurrentTile).Id;
                id = ProjectDBContext.Project.TileLookups.Where(x => x.Type == TileType.Tile8x8 && x.TileId == tempId).First().Id;
            }
            else if (CurrentTile is Tile16x16)
            {
                var tempId = ((Tile16x16)CurrentTile).Id;
                id = ProjectDBContext.Project.TileLookups.Where(x => x.Type == TileType.Tile16x16 && x.TileId == tempId).First().Id;
            }

            tiles[currentIndex] = id;

            int x = (currentIndex % GridSize) * CellSize;
            int y = (currentIndex / GridSize) * CellSize;

            Graphics g = this.CreateGraphics();

            DrawTile(currentIndex, x, y, g);
        }

        private void TileMapForm_Load(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            var tilemap = ProjectDBContext.Project.TilesMaps.First();
            tilemap.LookupIds = tiles;

            ProjectDBContext.Project.SaveChanges();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] output = new byte[1280];
            int count = 0;
            byte tilePos = 0;

            Dictionary<int, byte> tileMap = new Dictionary<int, byte>();

            var writer = new StreamWriter(BasePath + "tiles.asm", false, Encoding.UTF8);

            for (int i = 0; i < tiles.Length; i++)
            {
                var tileId = tiles[i];

                if (tileId == 0) continue;

                var lookup = ProjectDBContext.Project.TileLookups.Where(x => x.Id == tileId).First();

                byte newPos = 0;

                if (lookup.Type == TileType.Tile8x8)
                {
                    if (!tileMap.ContainsKey(tileId))
                    {
                        tileMap.Add(tileId, tilePos);
                        newPos = tilePos;
                        output[i] = tilePos++;

                        var tile = ProjectDBContext.Project.Tiles8.Find(lookup.TileId);
                        var data = Save8x8Tile(tile.Pixels);
                        writer.Write(data);
                    }
                    else
                    {
                        newPos = tileMap[tileId];
                        output[i] = newPos;
                    }
                }
                else if (lookup.Type == TileType.Tile16x16)
                {
                    if (!tileMap.ContainsKey(tileId))
                    {
                        tileMap.Add(tileId, tilePos);
                        output[i] = tilePos;
                        newPos = tilePos;
                        tilePos += 4;

                        var tile = ProjectDBContext.Project.Tiles16.Find(lookup.TileId);
                        var data = Save16x16Tile(tile.Pixels);
                        writer.Write(data);
                    }
                    else
                    {
                        newPos = tileMap[tileId];
                        output[i] = newPos;
                    }

                    tiles[i + 1] = 0;
                    output[i + 1] = ++newPos;

                    if (i < 1240)
                    {
                        tiles[i + 40] = 0;
                        output[i + 40] = ++newPos;

                        tiles[i + 41] = 0;
                        output[i + 41] = ++newPos;
                    }
                }
            }

            writer.Close();

            byte[] finaloutput = new byte[1280 * 2];
            int varcount = 0;

            for (int i = 0; i < 1280; i++)
            {
                finaloutput[i] = output[varcount++];
            }

            File.WriteAllBytes(BasePath + "tileMap.map", finaloutput);
        }

        private string Save8x8Tile(byte[] colors)
        {
            var sb = new StringBuilder();
            string line = "";

            for (var y = 2; y <= 64; y += 2)
            {
                if (y % 8 == 0 && y > 0)
                {
                    sb.Append("\tdb ");

                    var hex1 = colors[y - 2].ToString("X2");
                    var hex2 = colors[y - 1].ToString("X2");

                    line += "0x";
                    line += hex1.Substring(1, 1);
                    line += hex2.Substring(1, 1);
                    sb.Append(line + "\r\n");
                    line = "";
                }
                else
                {
                    var hex1 = colors[y - 2].ToString("X2");
                    var hex2 = colors[y - 1].ToString("X2");
                    line += "0x" + hex1.Substring(1, 1) + hex2.Substring(1, 1) + ", ";
                }
            }

            return sb.ToString().TrimEnd(',');
        }

        private string Save16x16Tile(byte[] colors)
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
                    var line = "\tdb ";
                    for (var y = 0; y < 8; y += 2)
                    {
                        var hex1 = colors[count].ToString("X2");
                        var hex2 = colors[count + 1].ToString("X2");
                        string final = "0x" + hex1.Substring(1, 1);
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

            return sb.ToString().TrimEnd(',');
        }

        string SaveMap(byte[] colors)
        {
            var sb = new StringBuilder();
            string line = "";

            for (var y = 2; y <= 1280; y += 2)
            {
                if (y % 8 == 0 && y > 0)
                {
                    sb.Append("db ");

                    var hex1 = colors[y - 2].ToString("X2");
                    var hex2 = colors[y - 1].ToString("X2");

                    line += "0x";
                    line += hex1.Substring(1, 1);
                    line += hex2.Substring(1, 1);
                    sb.Append(line + "\r\n");
                    line = "";
                }
                else
                {
                    var hex1 = colors[y - 2].ToString("X2");
                    var hex2 = colors[y - 1].ToString("X2");
                    line += "0x" + hex1.Substring(1, 1) + hex2.Substring(1, 1) + ", ";
                }
            }

            return sb.ToString().TrimEnd(',');
        }

        private void mode_Click(object sender, EventArgs e)
        {

        }

        private void mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode.SelectedIndex == 0)
            {
                pathPanel.Visible = false;
            }
            else
            {
                pathPanel.Visible = true;
            }

            this.Invalidate();
        }

        private void addStep_Click(object sender, EventArgs e)
        {
            messages.Text = "Please Select A Step Position In The Map";

            settingPathStep = true;
        }

        public void DrawHighlight()
        {
            //int x = (currentIndex % GridSize) * CellSize;
            //int y = (currentIndex / GridSize) * CellSize;
            //Graphics g = this.CreateGraphics();
            //g.DrawRectangle(Pens.LightGoldenrodYellow, x, y, CellSize, CellSize);
        }

        public void StopHighlight()
        {
            //int x = (currentIndex % GridSize) * CellSize;
            //int y = (currentIndex / GridSize) * CellSize;
            //Graphics g = this.CreateGraphics();
            //g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
        }

        public void MoveSelection(SelectionMove move)
        {

        }
    }
}
