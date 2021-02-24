using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje_1
{
    class player
    {
        /*public int _X{ get; set; }
        public int _Y{ get; set; }*/
        
        public static int _X;
        public static int _Y;
        public static int _altin;

        public int getX ()
        {
            return _X;
        }
        public int getY ()
        {
            return _Y;
        }
        public int getAltin ()
        {
            return _altin;
        }
        public void setX (int x)
        {
            _X = x;
        }        
        public void setY (int x)
        {
            _Y = x;
        }        
        public void setAltin (int x)
        {
            _altin = x;
        }

    }
}
