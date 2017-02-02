using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalGame.Entities
{
    /// <summary>
    /// This is an area that buttons can be shown and other useful UI things can be
    /// shown.
    /// </summary>
    class MenuBar : Entity
    {
        //The button to allow for pausing the game.
        public Button pauseBtn;

        public MenuBar()
            : base(0, Main.DisplayHeight - 60, "menubar")
        {
            pauseBtn = new Button((int)position.X, (int)position.Y, "Pause");
        }

        public override void doLogic()
        {
            pauseBtn.doLogic(); //This entity is not in the main entity list, so this allows for the button to check for presses.
            if (pauseBtn.wasPressed())
            {
                GameState.changeGameState(GameState.MENU_PAUSE);
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            base.draw(sb);
            pauseBtn.draw(sb);
        }
    }
}
