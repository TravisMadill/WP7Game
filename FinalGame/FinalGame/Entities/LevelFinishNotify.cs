using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame.Entities
{
    /// <summary>
    /// This class is an entity which allows the player to know that a level has been finished
    /// and can now move onto the next level.
    /// </summary>
    class LevelFinishNotify : Entity
    {
        Texture2D messageBox = GameState.loader.Load<Texture2D>("sprites\\messageBox");
        Button confirmBtn = new Button(440, 290, "OK");

        public LevelFinishNotify()
            : base(0, 0, "nothing")
        {

        }

        public override void doLogic()
        {
            confirmBtn.doLogic(); //When this entity is alive, there is nothing else that can do logic.
            if (confirmBtn.wasPressed())
            {
                Main.currentGS.removeEntity(this);
                ((State_Game)(Main.currentGS)).displayingMessage = false;
                State_Game.curScore += State_Game.scoreTracker.CurrentScore - State_Game.stoneTracker.StonesThrown;
                if (State_Game.curScore > Main.highScore)
                {
                    Main.highScore = State_Game.curScore;
                    Main.saveHighScore(Main.highScore);
                }
            }
        }

        public override void draw(SpriteBatch sb)
        {
            //The level complete string.
            string lvlCompleteMsg = "That's it! Level " + State_Game.level + "'s next!\nCurrent Score: " + (State_Game.curScore + State_Game.scoreTracker.CurrentScore - State_Game.stoneTracker.StonesThrown);

            //Just some helpful advice.
            if ((State_Game.curScore + State_Game.scoreTracker.CurrentScore - State_Game.stoneTracker.StonesThrown) < -10)
            {
                lvlCompleteMsg += "\nHaving trouble? You know what they say... Kill two birds with one stone!";
            }

            //Used for measuring the window size.
            int msgWidth = Text.getWidth(lvlCompleteMsg);
            int msgHeight = (int)Text.font.MeasureString(lvlCompleteMsg).Y;

            //The rectangle the message box will stretch to.
            Rectangle msgBoxSize = new Rectangle(Main.DisplayWidth / 2 - msgWidth / 2 - 5, Main.DisplayHeight / 2 - msgHeight / 2 - 5, msgWidth + 10, msgHeight + 10);
            
            //...And draw.
            sb.Draw(messageBox, msgBoxSize, Color.White);
            confirmBtn.draw(sb);
            Text.displayMessage(lvlCompleteMsg, Main.DisplayWidth / 2 - msgWidth / 2, Main.DisplayHeight / 2 - msgHeight / 2, 1, 1, 1, 1, 1, Text.NONE);
        }
    }
}
