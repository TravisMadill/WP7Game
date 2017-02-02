using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame
{
    public class StateController
    {
        public static GameState currentState = null;
        public const int MENU = 0;
        public const int GAME = 1;
        public long lastTime = DateTime.Now.Ticks;

        public StateController(int startState)
        {
            changeState(startState);
        }

        public static void changeState(int newState)
        {
            if (currentState != null)
                currentState.stop();

            switch (newState)
            {
                case MENU:
                    currentState = new State_Menu();
                    currentState.initialise();
                    break;
                case GAME:
                    currentState = new State_Game();
                    currentState.initialise();
                    break;
            }
        }

        public abstract void draw(SpriteBatch sb, Microsoft.Xna.Framework.GameTime delta); // Let each game mode have its own method of drawing
    }
}
