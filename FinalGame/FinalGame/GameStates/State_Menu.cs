using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame
{
	public class State_Menu : GameState
	{
		//Current menu state
		int curMenu = 0;

		//Constants for menu state
		const int Menu_Main = 0;
		const int Menu_Pause = 1;

		//Menu buttons
		Button startBtn;
		Button resumeBtn;
		Button resetHighScoreBtn;

		//Logo sprite
		public static Texture2D logo = loader.Load<Texture2D>("sprites\\logo");

		//High score string
		string hscoreStr = "Current High Score: "; //The string for showing the current high score.

		public State_Menu()
		{
			curMenu = Menu_Main; //As a default.
		}

		public State_Menu(int menuMode)
		{
			curMenu = menuMode;
		}

		public override void initialise()
		{
			switch (curMenu)
			{
				//Add all the entities used in each respective menu state
				case Menu_Main:
					startBtn = new Button(Main.DisplayWidth/2 - 100, Main.DisplayHeight/2 - 22, "Start!");
					resetHighScoreBtn = new Button(Main.DisplayWidth / 2 - 100, Main.DisplayHeight / 2 + 100, "Reset High Scores");
					addEntity(startBtn);
					addEntity(resetHighScoreBtn);
					break;
				case Menu_Pause:
					addEntity((resumeBtn = new Button(Main.DisplayWidth / 2 - 100, Main.DisplayHeight / 2 - 22, "Resume")));

					break;
			}
		}

		public override void stop()
		{
			switch(curMenu)
			{
				//Remove menu entities from the menu state.
				case Menu_Main:
					removeEntity(startBtn);
					removeEntity(resetHighScoreBtn);
					break;
				case Menu_Pause:
					removeEntity(resumeBtn);
					break;
			}
		}

		public override void renderThisFrame(long delta)
		{
			switch (curMenu)
			{
				case Menu_Main:
					if (startBtn.wasPressed())
					{
						GameState.changeGameState(GameState.GAME);
					}
					if (resetHighScoreBtn.wasPressed())
					{
						Main.saveHighScore(0);
						Main.highScore = Main.loadHighScore();
					}

					Text.displayMessage(hscoreStr + Main.highScore, Main.DisplayWidth / 2 - Text.getWidth(hscoreStr + Main.highScore) / 2, Main.DisplayHeight / 2 + 50, 1, 1, 1, 1, 1, Text.WAVE_yonly);

					foreach (Entity e in entities)
					{
						e.doLogic();
					}
					foreach (Entity e in entities)
					{
						e.move(delta);
					}
					break;
				case Menu_Pause:
					if (resumeBtn.wasPressed())
					{
						GameState.changeGameState(GameState.GAME);
					}
					Text.displayMessage(hscoreStr + Main.highScore, Main.DisplayWidth / 2 - Text.getWidth(hscoreStr + Main.highScore) / 2, Main.DisplayHeight / 2 + 50, 1, 1, 1, 1, 1, Text.WAVE_yonly);
					Text.displayMessage("Current Score: " + (State_Game.curScore + State_Game.scoreTracker.CurrentScore - State_Game.stoneTracker.StonesThrown), Main.DisplayWidth / 2 - Text.getWidth("Current Score: " + (State_Game.curScore + State_Game.scoreTracker.CurrentScore - State_Game.stoneTracker.StonesThrown)) / 2, Main.DisplayHeight / 2 + 80, 1, 1, 1, 1, 1, Text.WAVE_yonly);
					Text.displayMessage("PAUSED", Main.DisplayWidth / 2 - Text.getWidth("PAUSED"), Main.DisplayHeight / 2 - 80, 1, 1, 1, 1, 2, Text.NONE);
					foreach (Entity e in entities)
					{
						e.doLogic();
					}
					foreach (Entity e in entities)
					{
						e.move(delta);
					}
					break;
				default: //Just in case.
					Text.displayMessage("What's going on???", Main.DisplayWidth / 2, Main.DisplayHeight / 2, 1, 1, 1, 1, 1, Text.SHAKE);
					break;
			}
		}
	}
}
