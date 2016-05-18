using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tsolelet2
{
    enum direction { horizontal,vertical,non}
    enum ShipSnn { satil=4,dabur=3,tsolelt=3,dvora=2,non=0}
    enum playerType { player,comp}
        
    public partial class WarFrm : Form
    {
        int rows = 10;
        int colls = 10;
       
        PictureBox[,] p;
        PictureBox[,] c;
        string g;
        Ship[] playerShip;
        Ship[] computerShip;
        direction direct;
        ShipSnn ship;
        bool playerTurn;
        bool compTurn;
        bool hitflag;
        bool continiuebombing;
        bool rightORup;
        int index;
        bool pointToBomb;
        int bombdindex;
        int sec;
        int min;
        bool clock;
        bool playagain;
        Point hitedPoint;
        List<Point> bombdPlayer;
        List<Point> bombdComp;
        List<Point> chekRightUp;
        List<Point> chekLeftDown;
        





        public WarFrm()
        {
            InitializeComponent();
        }

        private void WarFrm_Load(object sender, EventArgs e)
        {
          
            this.BackgroundImage = Image.FromFile("ppp.jpg");
            this.ClientSize = new Size(800, 500);
            exitbtn.Left = ClientSize.Width - exitbtn.Width - 10;
            exitbtn.Top = 10;
            p = new PictureBox[rows, colls];
            c = new PictureBox[rows, colls];
            playerTurn = false;
            compTurn = false;
            label2.Visible = false;
            label4.Visible = false;
            playagain = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <colls; j++)
                {
                    //player ships mat
                    p[i, j] = new PictureBox();
                    c[i, j] = new PictureBox();

                    c[i, j].Width=c[i, j].Height = p[i, j].Width = p[i, j].Height = (this.ClientRectangle.Width / 3 ) / rows;

                    p[i, j].Top = p[i, j].Height * j + 150;
                    p[i, j].Left = p[i, j].Width * i + 60;

                    p[i, j].Visible = true;
                    p[i, j].BorderStyle = BorderStyle.Fixed3D;
                    p[i, j].BackColor = Color.Transparent;

                    c[i, j].Top = p[i, j].Height * j + 150;
                    c[i, j].Left = p[i, j].Width * i + 220+p[i,j].Width*rows;
                    c[i, j].Visible = true;
                    c[i, j].BorderStyle = BorderStyle.Fixed3D;
                    c[i, j].BackColor = Color.Transparent;
                    c[i, j].MouseClick += player_mouseclick;
                    c[i, j].Cursor = Cursors.Cross;
                    
                
                    this.Controls.Add(p[i, j]);
                    this.Controls.Add(c[i, j]);
                    

                }
            }
          
        }

 
        //starting the game
        private void startBtn_Click(object sender, EventArgs e)
        {
            sec = 0;
            min = 0;
            label4.Text = min.ToString() + ":";
            label2.Visible = true;
            label4.Visible = true;
            startBtn.Visible = false;
            clock = true;
            bombdComp = new List<Point>();
            bombdPlayer = new List<Point>();
            chekRightUp = new List<Point>();
            chekLeftDown = new List<Point>();
            hitedPoint = new Point(-1, -1);
            playerTurn = true;
            hitflag = false;
            continiuebombing = false;
            startBtn.Enabled = false;
            bombdindex = 0;
            rightORup = true;
            if (playagain)
            {
                for (int i = 0; i < colls; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        p[i, j].BackColor = Color.Transparent;
                        p[i, j].Image = null;
                        c[i, j].BackColor = Color.Transparent;
                        c[i, j].Image = null;
                    }
                }
            }
            initlizePlayerShip();
            initlizeCompShip();
            intruGame();
        }

        //placing player ship object in players matrix
        void initlizePlayerShip()
        {
            g = Xmlutility.GetCordinate();
            string[] h = g.Split('/');
            string[] b;
            playerShip = new Ship[4];
            for (int i = 0; i < h.Length; i++)
            {
                ship = getShiptype(i);

                b = h[i].Split(',');

                if (b[2].Equals("horizontal"))
                     direct = direction.horizontal;
                else
                     direct = direction.vertical;
                playerShip[i] = new Ship((int)ship, direct, int.Parse(b[0]), int.Parse(b[1]),ship,playerType.player); 
            }
            

            for (int i = 0; i <playerShip.Length; i++)
            {
                for (int j = 0; j < playerShip[i].getSize(); j++)
                {
                    p[playerShip[i].getPoint()[j].X, playerShip[i].getPoint()[j].Y].Image = Image.FromFile("sss.jpg");
                    p[playerShip[i].getPoint()[j].X, playerShip[i].getPoint()[j].Y].SizeMode = PictureBoxSizeMode.StretchImage;
                }

            }

        }
        void initlizeCompShip()
        {
            Random rnd = new Random();
            bool flag=true;
            bool flag1=true;
            int x=0;
            int y=0;
            computerShip = new Ship[4];
            for (int i = 0; i < computerShip.Length; i++)
            {
                ship = getShiptype(i);
                
                if(i==0)
                {
                  while(flag)
                  {
                      flag = false;
                      x = rnd.Next(1, 3);
                      if (x == 1)
                      {
                          direct = direction.horizontal;
                          x = rnd.Next(0, 10);
                          y = rnd.Next(0, 10);
                          flag = !chekTheBounds(new Point(x+(int)ship-1, y));
                      }
                          
                      else
                      {
                          direct = direction.vertical;
                          x = rnd.Next(0, 10);
                          y = rnd.Next(0, 10);
                          flag = !chekTheBounds(new Point(x, y + (int)ship-1));
                      }
                  }

                  computerShip[i] = new Ship((int)ship, direct, x, y, ship,playerType.comp);
                }
                else
                {
                    flag = true;
                    flag1 = true;
                    while(flag||flag1)
                    {
                        x = rnd.Next(1, 3);
                        if (x == 1)
                        {
                            direct = direction.horizontal;
                            x = rnd.Next(0, 10);
                            y = rnd.Next(0, 10);
                            flag = chekIfItsFree(x, y, i, computerShip);
                            flag1 = !chekTheBounds(new Point(x + (int)ship - 1, y));

                        }
                        else
                        {
                            direct = direction.vertical;
                            x = rnd.Next(0, 10);
                            y = rnd.Next(0, 10);
                            flag = chekIfItsFree(x, y, i, computerShip);
                            flag1 = !chekTheBounds(new Point(x, y+(int)ship-1));
                        }
                       
                    }
                    computerShip[i] = new Ship((int)ship, direct, x, y, ship,playerType.comp);

                }
                
            }
    
           
            
        }
        

        //returns the enum of aship
        ShipSnn getShiptype(int i)
        {
            switch (i)
            {
                case 0:
                     return ship = ShipSnn.satil;
                case 1:
                     return  ShipSnn.tsolelt;
                case 2:
                     return ShipSnn.dabur;
                case 3:
                     return ShipSnn.dvora;
                
            }
            return ShipSnn.non;
        }


        //the function cheking if the space in the matrix is free
        bool chekIfItsFree(int x,int y,int size,Ship [] s)
        {
        
            for (int j = 0; j < size; j++)
            {

                for (int k = 0; k < (int)ship; k++)
                {
                    for (int q = 0; q < s[j].getSize(); q++)
                    {
                        if (direct == direction.horizontal)
                        {
                            
                            //if (x + k == s[j].getPoint()[q].X && y == s[j].getPoint()[q].Y)
                            if(s[j].getPoint()[q].equalsByXY(x+k,y))
                                return true;
                        }
                        else
                        {
                            
                            //if (x == computerShip[j].getPoint()[q].X && y + k == computerShip[j].getPoint()[q].Y)
                            if (s[j].getPoint()[q].equalsByXY(x,y+k))
                                return true;
                        }
                        
                    }                 
                }        
            }
            return false;     
        }
        
        /*return true if thers a hite 
         * add the hits to hits List
         * dicreases live 
         */
        bool hit(int x, int y,Ship [] s)
        {
            
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s[i].getSize(); j++)
                {
                    if (x == s[i].getPoint()[j].X && y == s[i].getPoint()[j].Y)
                    {
                        s[i].dicreasLives();
                        return true;
                    }
                }
                
            }
           
            return false;
        }

        bool alreadyShot(Point current,List<Point> bombd)
        {
            for (int i = 0; i < bombd.Count; i++)
            {
                if (bombd[i].equalsByPoint(current))
                    return true;
            }
            return false;
        }


        //player move
        private void player_mouseclick(object sender, MouseEventArgs e)
        {
            if (playerTurn)
            {

                PictureBox cc = (PictureBox)sender;
                int y = (cc.Top-150) / cc.Height;
                int x = (cc.Left-c[0,0].Left) / cc.Width;
                Point p = new Point(x, y);
                if (!alreadyShot(p, bombdComp))
                {
                    if (hit(x, y, computerShip))
                    {
                        cc.Image = Image.FromFile("fff.jpg");
                        cc.SizeMode = PictureBoxSizeMode.StretchImage;
                       

                    }
                    else
                    {
                        cc.Image = Image.FromFile("ccc.jpg");
                        cc.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    bombdComp.Add(p);
                    playerTurn = false;
                    compTurn = true;
                    intruGame();
                }


            }

        }

        //computer move on game
        async void compMove()
        {

            await Task.Delay(2000);   
            int x, y;
            Point pp;
            Random rnd = new Random();
            
            if(compTurn&&!hitflag)
            {
                
                do
                {
                    x = rnd.Next(0, 10);
                    y = rnd.Next(0, 10);
                    pp = new Point(x, y);

                } while (alreadyShot(pp, bombdPlayer));

                if (hit(x, y, playerShip))
                {
                    hitedPoint.X = x;
                    hitedPoint.Y = y;
                    p[x,y].Image = Image.FromFile("fff.jpg");
                    hitflag = true;
                    continiuebombing = false;

                }
                else
                {
                    p[x,y].Image = Image.FromFile("ccc.jpg");
                    p[x,y].SizeMode = PictureBoxSizeMode.StretchImage;
                }
                bombdPlayer.Add(pp);
                playerTurn = true;
                compTurn = false;
                intruGame();
            }
            if (compTurn && hitflag)
            {
                if (!continiuebombing)
                {
                    ship = ShipSnn.non;
                    rightORup = true;
                    for (int i = 0; i <playerShip.Length ; i++)
                    {
                        if ((int)ship < playerShip[i].getSize() && playerShip[i].getStatus())
                        {
                            ship = playerShip[i].getName();
                            bombdindex = (int)ship - 1;
                        }
                           
                    }

                    if (rnd.Next(1, 3) == 1)
                    {
                        direct = direction.horizontal;
                    }
                    else
                    {
                        direct = direction.vertical;
                    }
                    continiuebombing = true;
                    rightORup = true;
                    pointToBomb = true;
                    index = 0;
                    chekRightUp.Clear();
                    chekLeftDown.Clear();
                }
                if (continiuebombing)
                {
                    if(direct==direction.horizontal&&compTurn)
                    {
                        if (pointToBomb)
                        {
                            for (int i = 1; i < (int)ship ; i++)
                            {
                                pp = new Point(hitedPoint.X + i, hitedPoint.Y);
                                if ((!alreadyShot(pp, bombdPlayer)) && chekTheBounds(pp))
                                    chekRightUp.Add(pp);
                                else
                                    break;

                            }

                            for (int i = 1; i < (int)ship; i++)
                            {
                                pp = new Point(hitedPoint.X - i, hitedPoint.Y);
                                if ((!alreadyShot(pp, bombdPlayer)) && chekTheBounds(pp))
                                    chekLeftDown.Add(pp);
                                else
                                    break;

                            }
                            pointToBomb = false;
                            
                        }
                        if(rightORup&&compTurn)
                        {
                            chekTheRightDown();
                           
                        }
                        if(!rightORup&&compTurn)
                        {
                            chekTheLeftUp();
                        }
                    }
                    if (direct == direction.vertical&&compTurn)
                    {
                        if (pointToBomb)
                        {
                            for (int i = 1; i < (int)ship; i++)
                            {
                                pp = new Point(hitedPoint.X, hitedPoint.Y+i);
                                if ((!alreadyShot(pp, bombdPlayer)) && chekTheBounds(pp))
                                    chekRightUp.Add(pp);
                                else
                                    break;

                            }

                            for (int i = 1; i < (int)ship ; i++)
                            {
                                pp = new Point(hitedPoint.X, hitedPoint.Y-i);
                                if ((!alreadyShot(pp, bombdPlayer)) && chekTheBounds(pp))
                                    chekLeftDown.Add(pp);
                                else
                                    break;

                            }
                            pointToBomb = false;
                           
                        }
                        if (rightORup&&compTurn)
                        {
                            chekTheRightDown();
                        }
                        if (!rightORup&&compTurn)
                        {
                            chekTheLeftUp();
                        }
                    }
                    intruGame();
                }
            }
        }
        void chekTheRightDown()
        {
            if (chekRightUp.Count > 0)
            {
                if (hit(chekRightUp[index].X, chekRightUp[index].Y, playerShip))
                {
                    p[chekRightUp[index].X, chekRightUp[index].Y].Image = Image.FromFile("fff.jpg");
                    p[chekRightUp[index].X, chekRightUp[index].Y].SizeMode = PictureBoxSizeMode.StretchImage;
                    bombdPlayer.Add(chekRightUp[index]);
                    chekRightUp.RemoveAt(index);
                    bombdindex--;
                    if (bombdindex == 0)
                    {
                        continiuebombing = false;
                        hitflag = false;
                    }
                    playerTurn = true;
                    compTurn = false;
                    intruGame();
                }
                else
                {
                    p[chekRightUp[index].X, chekRightUp[index].Y].Image = Image.FromFile("ccc.jpg");
                    p[chekRightUp[index].X, chekRightUp[index].Y].SizeMode = PictureBoxSizeMode.StretchImage;
                    bombdPlayer.Add(chekRightUp[index]);
                    chekRightUp.RemoveAt(index);
                    rightORup = false;
                    playerTurn = true;
                    compTurn = false;
                }

            }
            else
            {
                rightORup = false;
            }
                           
                         
        }

        void chekTheLeftUp()
        {
            if (chekLeftDown.Count > 0)
            {
                if (hit(chekLeftDown[index].X, chekLeftDown[index].Y, playerShip))
                {
                    p[chekLeftDown[index].X, chekLeftDown[index].Y].Image = Image.FromFile("fff.jpg");
                    p[chekLeftDown[index].X, chekLeftDown[index].Y].SizeMode = PictureBoxSizeMode.StretchImage;
                    bombdPlayer.Add(chekLeftDown[index]);
                    chekLeftDown.RemoveAt(index);
                    bombdindex--;
                    if (bombdindex == 0)
                    {
                        continiuebombing = false;
                        hitflag = false;
                    }
                    playerTurn = true;
                    compTurn = false;
                }
                else
                {
                    p[chekLeftDown[index].X, chekLeftDown[index].Y].Image = Image.FromFile("ccc.jpg");
                    p[chekLeftDown[index].X, chekLeftDown[index].Y].SizeMode = PictureBoxSizeMode.StretchImage;
                    bombdPlayer.Add(chekLeftDown[index]);
                    chekLeftDown.RemoveAt(index);
                    if (bombdindex == (int)ship - 1 )
                    {
                        if (direct == direction.horizontal)
                            direct = direction.vertical;
                        else
                            direct = direction.horizontal;
                        pointToBomb = true;
                        chekRightUp.Clear();
                        chekLeftDown.Clear();
                        rightORup = true;
                        playerTurn = true;
                        compTurn = false;

                    }
                    else
                    {
                        continiuebombing = false;
                        hitflag = false;
                        playerTurn = true;
                        compTurn = false;
                    }
                }
            }
            else
            {
                if (bombdindex == (int)ship - 1)
                {
                    if (direct == direction.horizontal)
                        direct = direction.vertical;
                    else
                        direct = direction.horizontal;
                    pointToBomb = true;
                    chekRightUp.Clear();
                    chekLeftDown.Clear();
                    rightORup = true;
                }
                else
                {
                    continiuebombing = false;
                    hitflag = false;
                }
            }
                            
        }

        bool chekTheBounds(Point p)
        {
            return p.X >= 0 && p.X < rows && p.Y >= 0 && p.Y < rows;
        }

        void intruGame()
        {
           
            bool playerLife = false; ;
            bool compLife=false;
            for (int i = 0; i < playerShip.Length; i++)
            {
                if(playerShip[i].getStatus())
                {
                    playerLife = true;
                    break;
                }
            }
            for (int i = 0; i < computerShip.Length; i++)
            {
                if (computerShip[i].getStatus())
                {
                    compLife = true;
                    break;
                }
            }
           if(playerLife&&compLife)
           {
               if (playerTurn)
                 
                   label2.Text = "player its your turn";
               else
               {
                   label2.Text = "comp turn";
                   compMove();
               }
           }
           else
           {
               if (playerLife)
                   label2.Text = "you won the game";
               else
               {
                   label2.Text = "you loose";
                   for (int i = 0; i < computerShip.Length; i++)
                   {
                       for (int j = 0; j < computerShip[i].getSize(); j++)
                       {
                           if (!computerShip[i].getStatus())
                               break;
                           if (!alreadyShot(computerShip[i].getPoint()[j], bombdComp))
                           {
                               c[computerShip[i].getPoint()[j].X, computerShip[i].getPoint()[j].Y].Image = Image.FromFile("sss.jpg");
                               c[computerShip[i].getPoint()[j].X, computerShip[i].getPoint()[j].Y].SizeMode = PictureBoxSizeMode.StretchImage;
                           }
                       }
                   }
                       
               }
               playagain = true;
               clock = false;
               startBtn.Size = new Size(240, 55);
               startBtn.FlatAppearance.BorderSize = 0;
               startBtn.Text = "play again";
               startBtn.Visible = true;
               startBtn.Enabled = true;
           }
          

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (clock)
            {
                sec++;
                if (sec == 60)
                {
                    min++;
                    sec = 0;
                }
                label4.Text = min.ToString() + ":" + sec.ToString(); 
            }
            
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
     


    }


    

}
