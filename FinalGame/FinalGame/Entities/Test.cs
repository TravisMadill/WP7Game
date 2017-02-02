using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalGame.Entities
{
    /// <summary>
    /// This wasn't really anything. This was just to test some things.
    /// All that's left is its moving and bouncing off the screen borders.
    /// </summary>
    class Test : Entity
    {
        public Test(int x, int y) : base(x, y, "test")
        {
            
        }

        public override void doLogic()
        {
            if (Position.X + Movement.X + width > Main.DisplayWidth || Position.X + Movement.X < 0)
            {
                movement.X = -Movement.X;
            }
            if (Position.Y + Movement.Y + height > Main.DisplayHeight || Position.Y + Movement.Y < 0)
            {
                movement.Y = -Movement.Y;
            }
            Text.displayMessage((((int)position.X) + ", " + ((int)position.Y)), (int)position.X - Text.getWidth(((int)position.X) + ", " + ((int)position.Y))/2, (int)position.Y-20, 255, 255, 255, 255, (float)0.8, Text.NONE);
        }
    }
}
