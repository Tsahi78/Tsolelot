using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsolelet2
{
    class Ship
    {
        private int size;
        private  Point [] point;
        private ShipSnn name;
        private int lives;
        private bool status;
        private playerType player;


        public Ship(int Size,direction direct,int x,int y,ShipSnn Name,playerType Player)
        {
            size = Size;
            lives = Size;
            name = Name;
            status = true;
            player = Player;
            point = new Point[size];
            point[0] = new Point(x, y);
            
            for (int i = 1; i <point.Length; i++)
            {
                if(direct==direction.horizontal)
                {
                   
                        point[i] = new Point(x + i, y);
                }
                else
                {
            
                        point[i] = new Point(x, y + i);
                        
                }
             
            }
        }

        public int getSize()
        {
            return size;
        }
        public Point [] getPoint()
        {
            return point;
        }
        public void dicreasLives()
        {
            lives--;
            status =!( lives == 0);
        }
        public bool getStatus()
        {
            return status;
        }
        public playerType getPlayerType()
        {
            return player;
        }
        public ShipSnn getName()
        {
            return name;
        }

    }
}
