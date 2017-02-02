using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework.Input.Touch;

namespace FinalGame
{
    /// <summary>
    /// This class represents a single representation of something
    /// that requires something to do with reactions and updating information in the game.
    /// The next thesaurus definition for object was "Entity", so I used that name.
    /// </summary>
	public abstract class Entity
	{
        /// <summary>
        /// The current position of this entity.
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// The current sprite being used for this entity.
        /// </summary>
        protected Texture2D sprite;


        /// <summary>
        /// The movement to apply to this being just before drawing this entity
        /// in pixels/sec.
        /// </summary>
        protected Vector2 movement = new Vector2(0, 0);

        /// <summary>
        /// The width of the sprite of this entity
        /// </summary>
        public int width;
        /// <summary>
        /// The height of the sprite of this entity
        /// </summary>
        public int height;

        /// <summary>
        /// The gravity constant for beings that actually move.
        /// Equal to (0, 2.5).
        /// </summary>
        public static readonly Vector2 GRAVITY = new Vector2(0, 2.5f);

        /// <summary>
        /// The current state of the touch screen.
        /// </summary>
        public static TouchCollection tc;

        /// <summary>
        /// Creates a new entity. Adding this to the being List in the main
        /// class will allow it to be rendered and apply logic every frame.
        /// </summary>
        /// <param name="x">The x coordinate of this entity</param>
        /// <param name="y">The y coordinate of this entity</param>
        /// <param name="sprite">The sprite to use for this entity</param>
        protected Entity(int x, int y, Texture2D sprite)
        {
            this.position = new Vector2(x, y);
            this.sprite = sprite;
            this.width = sprite.Width;
            this.height = sprite.Height;
        }

        /// <summary>
        /// Creates a new entity. Adding this to the being List in the main
        /// class will allow it to be rendered and apply logic every frame.
        /// </summary>
        /// <param name="x">The x coordinate of this entity</param>
        /// <param name="y">The y coordinate of this entity</param>
        /// <param name="spriteName">The location of the sprite in the content folder</param>
        protected Entity(int x, int y, string spriteName)
        {
            this.position = new Vector2(x, y);
            this.sprite = GameState.loader.Load<Texture2D>("sprites\\" + spriteName);
            this.width = this.sprite.Width;
            this.height = this.sprite.Height;
        }

        /// <summary>
        /// This is called for this entity every frame in order to perform various tasks based
        /// on events from the last frame.
        /// The abstract means that this method must be used in inheriting classes.
        /// </summary>
        public abstract void doLogic();

        /// <summary>
        /// Moves the entity based on the movement vector and the time between frames.
        /// </summary>
        /// <param name="delta">The time since the last frame in milliseconds.</param>
        public virtual void move(float delta)
        {
            position += (movement * delta) / 100;
        }

        /// <summary>
        /// Draws the entity if it is on screen. (Can be overridden)
        /// The virtual means that what happens here is a default, but
        /// can be overridden by other entities. (Such as drawing text instead of a sprite)
        /// </summary>
        /// <param name="sb">The SpriteBatch used for drawing the being.</param>
        public virtual void draw(SpriteBatch sb)
        {
            if (isOnScreen(position, width, height))
                sb.Draw(sprite, position, Color.White);
        }

        /// <summary>
        /// Checks if a rectangle is within the window.
        /// </summary>
        /// <param name="pos">The position of the top left part of the rectangle</param>
        /// <param name="width">The width of the rectangle</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>True if this rectangle is within the screen borders, false otherwise.</returns>
        public bool isOnScreen(Vector2 pos, int width, int height)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, width, height).Intersects(
                new Rectangle(0, 0, Main.DisplayWidth, Main.DisplayHeight));
        }

        /// <summary>
        /// Gets or sets this entity's current position vector.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Gets or sets the current movement vector.
        /// </summary>
        public Vector2 Movement
        {
            get { return movement; }
            set { movement = value; }
            
        }
        
        /// <summary>
        /// Gets the rectangle that this being is located in. Typically used for collisions
        /// and such, hence the name "Hit box".
        /// </summary>
        public Rectangle HitBox
        {
            get { return new Rectangle((int)(position.X), (int)(position.Y), width, height); }
        }

        /// <summary>
        /// Swaps the current sprite of this being with a new one,
        /// and replaces the one given with the one replaced.
        /// </summary>
        /// <param name="spriteToSwap">The sprite to swap the current one with.</param>
        public void swapSprites(ref Texture2D spriteToSwap)
        {
            Texture2D temp = this.sprite;
            this.sprite = spriteToSwap;
            spriteToSwap = temp;
        }

        /// <summary>
        /// Updates the global TouchPanel state used by all entities that use touch controls.
        /// </summary>
        public static void updateTouchPanel()
        {
            tc = TouchPanel.GetState();
        }
    }
}
