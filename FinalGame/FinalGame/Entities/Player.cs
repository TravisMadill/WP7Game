using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace FinalGame.Entities
{
    /// <summary>
    /// This class represents the player. The stone-thrower.
    /// </summary>
    class Player : Entity
    {
        /// <summary>
        /// The sprite of the crosshair for aiming.
        /// </summary>
        Texture2D crosshair;

        /// <summary>
        /// The sprite of the player preparing to throw.
        /// </summary>
        Texture2D sprite_prepThrowing;

        /// <summary>
        /// The sprite of the player throwing a stone.
        /// </summary>
        Texture2D sprite_throwStone;

        /// <summary>
        /// The time that the throwing stone sprite will be shown (2 seconds, in ticks -> 2000000)
        /// </summary>
        long throwTime;

        /// <summary>
        /// The throwing stone sound effect.
        /// </summary>
        SoundEffect throwingSnd = GameState.loader.Load<SoundEffect>("sfx\\throw");
        
        /// <summary>
        /// Creates a new instance of the player. Also grabs the sprites for the other animation states, too.
        /// </summary>
        public Player()
            : base(20, Main.DisplayHeight - 100, "player_idle")
        {
            crosshair = GameState.loader.Load<Texture2D>("sprites\\crosshair");
            sprite_prepThrowing = GameState.loader.Load<Texture2D>("sprites\\player_prepThrow");
            sprite_throwStone = GameState.loader.Load<Texture2D>("sprites\\player_throwing");
            throwTime = DateTime.Now.Ticks;
        }

        public override void doLogic()
        {
            foreach (TouchLocation tl in Entity.tc)
            {
                if (tl.State == TouchLocationState.Released)
                {
                    //Add a new stone based and make its throw power based on the distance tapped from the player.
                    State_Game.stoneTracker.StonesThrown += 1;
                    float powerY = -(tl.Position.Y - position.Y)/100;
                    float powerX = (tl.Position.X - position.X)/100;
                    Main.currentGS.addEntity(new Stone((int)position.X, (int)position.Y, (int)tl.Position.X, (int)tl.Position.Y, (int)(25*(powerX+powerY))));
                    throwingSnd.Play(); //...and play the sound effect.
                }
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            base.draw(sb);
            foreach (TouchLocation tl in Entity.tc)
            {
                if (tl.State == TouchLocationState.Moved)
                {
                    if (this.sprite.Equals(GameState.loader.Load<Texture2D>("sprites\\player_idle")))
                        this.swapSprites(ref sprite_prepThrowing);
                    sb.Draw(crosshair, tl.Position - new Vector2(20, 20), Color.White);
                }
                else if (tl.State == TouchLocationState.Released)
                {
                    if (this.sprite.Equals(GameState.loader.Load<Texture2D>("sprites\\player_prepThrow")))
                    {
                        this.swapSprites(ref sprite_prepThrowing);
                        this.swapSprites(ref sprite_throwStone);
                        throwTime = DateTime.Now.Ticks;
                    }
                }
            }
            if (DateTime.Now.Ticks - throwTime > 2000000)
            {
                if (this.sprite.Equals(GameState.loader.Load<Texture2D>("sprites\\player_throwing")))
                {
                    this.swapSprites(ref sprite_throwStone);
                }
            }
        }
    }
}
