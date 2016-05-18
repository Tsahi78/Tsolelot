using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsolelet2
{
    class Point
    {
        private int x;
        private int y;

        public Point(int xx, int yy)
        {
            x = xx;
            y = yy;
        }


        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool equalsByPoint(Point p)
        {
            return p.X == x && p.Y == y;
        }

        public bool equalsByXY(int xx,int yy)
        {
            return xx == x && yy == y;
        }

        public String toString()
        {
            return "[" + x + "," + y + "]";
        }




    }
}
