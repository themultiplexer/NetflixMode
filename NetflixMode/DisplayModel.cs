using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixMode
{
    class DisplayModel
    {
        public string Name;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public bool Disabled;

        public DisplayModel(string name)
        {
            Name = name;
        }
    }
}
