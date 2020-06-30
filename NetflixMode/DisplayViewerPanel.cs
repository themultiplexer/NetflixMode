using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetflixMode
{
    [System.ComponentModel.DesignerCategory("Code")]
    class DisplayViewerPanel : Panel
    {
        public Dictionary<string, DisplayModel> Displays = new Dictionary<string, DisplayModel>();

        public DisplayViewerPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            /*
            using (SolidBrush brush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(brush, ClientRectangle);
            e.Graphics.DrawRectangle(Pens.Yellow, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            */
        }

        public void DrawDisplay()
        {
            Point center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            Size size = new Size();

            foreach (KeyValuePair<string, DisplayModel> dm in Displays)
            {
                size.Width += dm.Value.Width;
                size.Height += dm.Value.Height;
            }

            center.X -= (size.Width / Displays.Keys.Count) / 20;
            center.Y -= (size.Height / Displays.Keys.Count) / 20;


            foreach (KeyValuePair<string, DisplayModel> dm in Displays)
            {
                if (GetPanelForName(dm.Key) is MyPanel ep)
                {
                    ep.Width = dm.Value.Width / 10;
                    ep.Height = dm.Value.Height / 10;

                    if (dm.Value.Disabled)
                    {
                        ep.BackColor = Color.Gray;
                    }
                    else
                    {
                        ep.BackColor = Color.LightGray;
                    }
                    ep.Name = dm.Value.Name;
                    ep.Location = new Point(center.X + (dm.Value.X / 10), center.Y + (dm.Value.Y / 10));
                }
                else
                {
                    MyPanel p = new MyPanel(dm.Value.Name);
                    p.Width = dm.Value.Width / 10;
                    p.Height = dm.Value.Height / 10;
                    p.Key = dm.Key;
                    p.BackColor = Color.LightGray;
                    p.Location = new Point(center.X + (dm.Value.X / 10), center.Y + (dm.Value.Y / 10));
                    p.Click += new EventHandler(panel_MouseClick);
                    this.Controls.Add(p);
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            }
        }

        private MyPanel GetPanelForName(string name)
        {
            foreach (Control c in Controls)
            {
                if (c is MyPanel m && m.Key == name)
                {
                    return m;
                }
            }
            return null;
        }

        private void panel_MouseClick(object sender, EventArgs e)
        {
            MyPanel mp = (MyPanel)sender;
            mp.Selected = !mp.Selected;
        }
    }
}
