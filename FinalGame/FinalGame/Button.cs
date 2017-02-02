using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace FinalGame
{
    /// <summary>
    /// The concept from this class is relatively the same from the Windows XNA game, but
    /// with touch controls instead.
    /// </summary>
    class Button : Entity
    {
        /// <summary>
        /// Whether or not this button is being pressed.
        /// </summary>
        protected bool pressed = false;

        /// <summary>
        /// The other button sprite (that looks like it's being pressed)
        /// </summary>
        Texture2D otherBtnState;

        /// <summary>
        /// The text to display on the button.
        /// </summary>
        string btnText;

        /// <summary>
        /// The colour of the text.
        /// </summary>
        Color textColour = Color.Black;

        /// <summary>
        /// The sound that the button makes when pressed.
        /// </summary>
        SoundEffect clickSnd = GameState.loader.Load<SoundEffect>("sfx\\click");
        
        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="x">The x coordinate of this button</param>
        /// <param name="y">The y coordinate of this button</param>
        /// <param name="btnText">The message to show on the button.</param>
        public Button(int x, int y, string btnText)
            : base(x, y, "button_n")
        {
            otherBtnState = GameState.loader.Load<Texture2D>("sprites\\button_p");
            this.btnText = btnText;
        }

        public override void doLogic()
        {
            foreach (TouchLocation tl in tc)
            {
                if (tl.State == TouchLocationState.Pressed)
                {
                    if (this.HitBox.Intersects(new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 1, 1)))
                    //if (this.x < tl.Position.X && this.x + width > tl.Position.X && this.y < tl.Position.Y && this.x + height > tl.Position.Y)
                    {
                        if (this.sprite.Equals(GameState.loader.Load<Texture2D>("sprites\\button_n")))
                        {
                            this.swapSprites(ref otherBtnState);
                            textColour = Color.White;
                        }
                    }
                }
                if (tl.State == TouchLocationState.Released)
                {
                    if (this.sprite.Equals(GameState.loader.Load<Texture2D>("sprites\\button_p")))
                    {
                        this.swapSprites(ref otherBtnState);
                        textColour = Color.Black;
                    }
                    if (this.HitBox.Intersects(new Rectangle((int)tl.Position.X, (int)tl.Position.Y, 1, 1)))
                    //if (this.x < tl.Position.X && this.x + width > tl.Position.X && this.y < tl.Position.Y && this.x + height > tl.Position.Y)
                    {
                        this.pressed = true;
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether or not this button is being pressed.
        /// </summary>
        /// <returns>If this button was pressed or not.</returns>
        public bool wasPressed()
        {
            if (this.pressed)
            {
                this.pressed = false;
                clickSnd.Play();
                return true;
            }
            return false;
        }

        public override void draw(SpriteBatch sb)
        {
            Vector2 textOffset = new Vector2(width / 2, height / 2); //To make sure that the text is centred on the button.
            sb.Draw(sprite, position, Color.White);
            sb.DrawString(Text.font, btnText, Position + textOffset, textColour, 0, Text.font.MeasureString(btnText) / 2, 1, SpriteEffects.None, 0);
        }
    }
}
