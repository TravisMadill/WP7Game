using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalGame.Templates;
using Microsoft.Xna.Framework;

namespace FinalGame.Entities.Birds
{
    /// <summary>
    /// This class represents the standard bird enemy in this game.
    /// They just fly in a straight line, towards wherever they want to go.
    /// They also rotate based on their destination, too.
    /// 
    /// Also, this is the only bird enemy in the game mainly because of time constraints. Adding more shouldn't
    /// be too hard, though.
    /// </summary>
    class NormalBird : Bird
    {
        float rotation;

        public NormalBird(int x, int y, int destinationX, int destinationY)
            : base(x, y, "bird")
        {
            //Get rotation angle.
            rotation = (float)(-Math.Atan2(destinationX - x - 40 / 2, destinationY - y - 40 / 2));

            //Set movement direction based on destination and start positions.
            Movement = new Vector2((float)Math.Sin(rotation) * 5, (float)-Math.Cos(rotation) * 5);
            
            //Extending the hitbox because of rotations. This is the maximum that this can extend to.
            // (The square root of 40 squared plus 40 squared)
            this.width = 56;
            this.height = 56;
        }

        public override void doLogic()
        {
            if (!isOnScreen(position, width, height)) //If this wasn't here, a level could potentially never end, since the bird would still be alive.
            {
                Main.currentGS.removeEntity(this);
            }
                
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            if(rotation >= 0) //We can't have the birds flying upside down
                sb.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, 40, 40), null, Color.White, rotation, new Vector2(width / 2, height / 2), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
            else
                sb.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, 40, 40), null, Color.White, rotation, new Vector2(width / 2, height / 2), Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally, 0);
        }
    }
}
