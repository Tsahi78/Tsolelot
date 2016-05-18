using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsolelet2
{
    class Missell
    {/*
                      if(continiuebombing)
                {
                    changeDirection = !rightORup;
                    if (direct == direction.horizontal)
                    {
                        if (rightORup)
                        {
                            if (hitedPoint.X + ((int)ship - bombdindex) > rows - 1 || alreadyShot(pp = new Point(hitedPoint.X + ((int)ship - bombdindex), hitedPoint.Y), bombdPlayer))
                            {
                                rightORup = false;
                                intruGame();
                            }
                            else
                            {
                                
                                if (hit(pp.X,pp.Y, playerShip))
                                {
                                    
                                    p[pp.X, pp.Y].Image = Image.FromFile("fff.jpg");
                                    bombdindex--;
                                    hits++;
                                    bombdPlayer.Add(pp);
                                    if (bombdindex == 0)
                                    {
                                        continiuebombing = false;
                                        hitflag = false;
                                    }

                                }
                                else
                                {

                                    bombdPlayer.Add(pp);
                                    rightORup = false;
                                    p[pp.X, pp.Y].Image = Image.FromFile("ccc.jpg");
                                    p[pp.X, pp.Y].SizeMode = PictureBoxSizeMode.StretchImage;
                                   
                                }
                            }
                        }
                        if (!rightORup&&changeDirection)
                        {
                            if (hitedPoint.X - ((int)ship - (bombdindex+hits)) < 0 || alreadyShot(pp = new Point(hitedPoint.X - ((int)ship -( bombdindex+hits)), hitedPoint.Y), bombdPlayer))
                            {
                                if (bombdindex == (int)ship - 1 && horStart)
                                {
                                    direct = direction.vertical;
                                    rightORup = true;
                                    intruGame();
                                }
                                else
                                {
                                    continiuebombing = false;
                                    hitflag = false;
                                    intruGame();
                                    
                                }
                            }
                            else
                            {
                                if (hit(hitedPoint.X - ((int)ship - (bombdindex+hits)), hitedPoint.Y, playerShip))
                                {
                                    
                                    p[hitedPoint.X - ((int)ship - (bombdindex+hits)), hitedPoint.Y].Image = Image.FromFile("fff.jpg");
                                    bombdindex--;
                                   
                                    bombdPlayer.Add(pp);
                                    if (bombdindex == 0)
                                    {
                                        continiuebombing = false;
                                        hitflag = false;
                                    }

                                }
                                else
                                {

                                    p[hitedPoint.X - ((int)ship - (bombdindex+hits)), hitedPoint.Y].Image = Image.FromFile("ccc.jpg");
                                    p[hitedPoint.X - ((int)ship - (bombdindex+hits)), hitedPoint.Y].SizeMode = PictureBoxSizeMode.StretchImage;
                                    bombdPlayer.Add(pp);
                                    if (bombdindex == (int)ship - 1&&horStart)
                                    {
                                        direct = direction.vertical;
                                        playerTurn = true;
                                        compTurn = false;
                                        rightORup = true;
                                        intruGame();
                                    }
                                    else
                                    {
                                        continiuebombing = false;
                                        hitflag = false;
                                      
                                    }
                                }
                            }
                        }
                    }

                    if(direct==direction.vertical)
                    {
                        if (rightORup)
                        {
                            if (hitedPoint.Y + ((int)ship - bombdindex) > rows - 1 || alreadyShot(pp = new Point(hitedPoint.X, hitedPoint.Y + ((int)ship - bombdindex)), bombdPlayer))
                            {
                                rightORup = false;
                                intruGame();
                            }
                            else
                            {
                                if (hit(hitedPoint.X, hitedPoint.Y + ((int)ship - bombdindex), playerShip))
                                {
                                    
                                    p[hitedPoint.X, hitedPoint.Y + ((int)ship - bombdindex)].Image = Image.FromFile("fff.jpg");
                                    bombdindex--;
                                    bombdPlayer.Add(pp);
                                    hits++;
                                    if (bombdindex == 0)
                                    {
                                        continiuebombing = false;
                                        hitflag = false;
                                      
                                    }

                                }
                                else
                                {
                                    rightORup = false;
                                    bombdPlayer.Add(pp);
                                    p[hitedPoint.X, hitedPoint.Y + ((int)ship - bombdindex)].Image = Image.FromFile("ccc.jpg");
                                    p[hitedPoint.X, hitedPoint.Y + ((int)ship - bombdindex)].SizeMode = PictureBoxSizeMode.StretchImage;
                                    
                                   
                                }
                            }
                        }
                        if(!rightORup&&changeDirection)
                        {
                            if (hitedPoint.Y - ((int)ship - (bombdindex+hits)) < 0 || alreadyShot(pp = new Point(hitedPoint.X, hitedPoint.Y - ((int)ship -( bombdindex+hits))), bombdPlayer))
                            {
                                if (bombdindex == (int)ship - 1 && verStart)
                                {
                                    direct = direction.horizontal;
                                    rightORup = true;
                                    intruGame();
                                }
                                else
                                {
                                    continiuebombing = false;
                                    hitflag = false;
                                    intruGame();
                                }
                            }
                            else
                            {
                                if (hit(hitedPoint.X, hitedPoint.Y - ((int)ship - (bombdindex+hits)), playerShip))
                                {
                                    
                                    p[hitedPoint.X, hitedPoint.Y - ((int)ship - (bombdindex+hits))].Image = Image.FromFile("fff.jpg");
                                    bombdindex--;
                                    bombdPlayer.Add(pp);
                                    if (bombdindex == 0)
                                    {
                                        continiuebombing = false;
                                        hitflag = false;
                                    }

                                }
                                else
                                {
                                    bombdPlayer.Add(pp);
                                    p[hitedPoint.X , hitedPoint.Y- ((int)ship - (bombdindex+hits))].Image = Image.FromFile("ccc.jpg");
                                    p[hitedPoint.X, hitedPoint.Y - ((int)ship - (bombdindex+hits))].SizeMode = PictureBoxSizeMode.StretchImage;
                                    if (bombdindex == (int)ship - 1 &&verStart)
                                    {
                                        direct = direction.horizontal;
                                        playerTurn = true;
                                        compTurn = false;
                                        rightORup = true;
                                        intruGame();
                                    }
                                    else
                                    {
                                        continiuebombing = false;
                                        hitflag = false;
                                       
                                    }
                                }
                            }
                        }
                    }
                }
      */

    }
}
