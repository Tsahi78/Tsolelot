using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tsolelet2
{
    class Xmlutility
    {
       

        private Xmlutility() { }

        public static string  GetCordinate(){
               string g="";
               bool flag = true;
               XmlReader reader=XmlReader.Create(@"XMLcordinate.xml");
                while (reader.Read())
                {
                   

                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag
                        
                        switch (reader.Name.ToString())
                        {
                            case "xy":
                                if (!flag)
                                    g += "/";
                                g+=  reader.ReadString();
                                g = g.Replace("\n", "");
                                g = g.Replace("\r", "");
                                g = g.Replace("\t", "");
                                g = g.Replace(" ", "");
                                g += ",";
                                break;
 
                            case "position":
                                g+= reader.ReadString();
                                g = g.Replace("\n", "");
                                g = g.Replace("\r", "");
                                g = g.Replace("\t", "");
                                g=g.Replace(" ","");
                                flag = false;
                                break;
                        }
                        
                    }
                    
                    
                }

                return g;
        }
       
         
    }
}
