﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceRace
{
    internal class Carss
    {
        // colour, rectangle 
        public int size = 8;
        public int xSpeed = 8;
        public int x, y;

        public Carss(int _x, int _y, int _speed)
        {
            x = _x;
            y = _y;
            xSpeed = _speed;
        }

        public void Move()
        {
            x += xSpeed;

            

        }
    }
}
