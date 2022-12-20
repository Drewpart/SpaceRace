using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceRace
{
    internal class rightCars
    {
        public int size = 12;
        public int xSpeed = 8;
        public int x, y;

        public rightCars(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void MoveRight()
        {
          x -= xSpeed;
        }

    }
}

    

