using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using FinalGame.Templates;

namespace FinalGame.Entities
{
    /// <summary>
    /// This class represents the stones that are thrown at the
    /// birds in this game.
    /// </summary>
    class Stone : Entity
    {
        /// <summary>
        /// The throwing power of this stone.
        /// </summary>
        int throwPower;

        /// <summary>
        /// The sound effect of the stone hitting a bird.
        /// </summary>
        SoundEffect collisionSnd = GameState.loader.Load<SoundEffect>("sfx\\smack");

        /// <summary>
        /// Creates a new instance of a stone.
        /// </summary>
        /// <param name="x">The starting x coordinate of the stone.</param>
        /// <param name="y">The starting y coordinate of the stone.</param>
        /// <param name="touchX">The x coordinate on the screen that was touched.</param>
        /// <param name="touchY">The y coordinate on the screen that was touched.</param>
        /// <param name="throwPower">The power of the throw.</param>
        public Stone(int x, int y, int touchX, int touchY, int throwPower)
            : base(x, y, "stone")
        {
            this.throwPower = throwPower;
            double rotation = (float)(-Math.Atan2(touchX - x - 40 / 2, touchY - y - 40/ 2) + Math.PI);
            Movement = new Vector2((float)Math.Sin(rotation) * throwPower, (float)-Math.Cos(rotation) * throwPower);
        }

        public override void doLogic()
        {
            //Get the possible entities that it can collide with...
            List<Entity> collisions = Main.currentGS.getPossibleCollision(this);
            if (collisions.Count > 0)
            {
                foreach (Entity e in collisions)
                {
                    if (e is Bird) //...check if those entities are birds...
                    {
                        //...then add to the score, and remove the birds and this stone.
                        Main.currentGS.removeEntity(e);
                        Main.currentGS.removeEntity(this);
                        State_Game.scoreTracker.CurrentScore += 1;
                        collisionSnd.Play(); //...And play the sound.
                    }
                }
            }
            if (!isOnScreen(position, width, height)) //And remove it if this is offscreen.
                Main.currentGS.removeEntity(this);
            Movement += GRAVITY;
        }
    }
}
