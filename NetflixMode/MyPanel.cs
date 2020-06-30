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
    public class MyPanel : Panel
    {
        public string Name;
        private bool selected;
        public string Key;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                /*
                if (value)
                {
                    this.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    this.BorderStyle = BorderStyle.None;
                }
                */
                selected = value;
                this.Refresh();
            }
        }

        public MyPanel(string name)
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.Name = name;

            /*
            Label l = new Label();
            l.Text = name;
            l.AutoSize = false;
            l.Font = new Font(l.Font.FontFamily, 15);
            l.ForeColor = Color.White;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Dock = DockStyle.Fill;
            l.Enabled = false;
            //l.Padding = new Padding(10);
            l.BackColor = Color.Transparent;
            this.Controls.Add(l);
            */


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.Clear(Color.Transparent);

            Rectangle back = ClientRectangle;
            back.X += 2;
            back.Y += 2;
            back.Width -= 4;
            back.Height -= 4;

            using (SolidBrush brush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(brush, back);

            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            e.Graphics.DrawString(this.Name, this.Font, Brushes.Black, ClientRectangle, stringFormat);

            if (this.Selected)
            {
                Pen p = new Pen(Color.Green, 4);
                e.Graphics.DrawRectangle(p, 2, 2, ClientSize.Width - 5, ClientSize.Height - 5);
            }
            else
            {
                Pen p = new Pen(Color.Black, 4);
                e.Graphics.DrawRectangle(p, 2, 2, ClientSize.Width - 5, ClientSize.Height - 5);
            }
        }
    }
}
