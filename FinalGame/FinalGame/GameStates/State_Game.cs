using System;
using FinalGame.Entities;
using FinalGame.Entities.Birds;
using FinalGame.UI;
using Microsoft.Xna.Framework.Graphics;

namespace FinalGame
{
    class State_Game : GameState
    {

        /// <summary>
        /// The current level of the game the user is currently in right now.
        /// </summary>
        public static int level = 1;

        /// <summary>
        /// The current player information
        /// </summary>
        public static Player player = new Player();

        /// <summary>
        /// Keeps track of the stones thrown this session.
        /// </summary>
        public static StoneTracker stoneTracker = new StoneTracker();

        /// <summary>
        /// Keeps track of the current score of the game.
        /// </summary>
        public static ScoreTracker scoreTracker = new ScoreTracker();

        /// <summary>
        /// The current score from this game. Used to determine whether or not to save a new high score or not.
        /// </summary>
        public static int curScore = 0;

        /// <summary>
        /// Used for notifying the user when a level is finished.
        /// </summary>
        public static LevelFinishNotify lvlFinNotify = new LevelFinishNotify();

        /// <summary>
        /// Whether or not a message is currently being displayed. (Namely lvlFinNotify)
        /// </summary>
        public bool displayingMessage = false;

        /// <summary>
        /// The background for the level.
        /// </summary>
        public static Texture2D background;

        /// <summary>
        /// The menu bar background.
        /// </summary>
        public MenuBar menubar = new MenuBar();

        /// <summary>
        /// The random number generator for bird spawning and starting positions.
        /// </summary>
        public Random r = new Random();

        /// <summary>
        /// The number of birds generated so far.
        /// </summary>
        public int birdsGenerated = 0;

        /// <summary>
        /// Creates a new instance of the Game state.
        /// </summary>
        public State_Game() { }

        public override void initialise()
        {
            background = loader.Load<Texture2D>("sprites\\bg");
            addEntity(player);
            addEntity(stoneTracker);
            addEntity(scoreTracker);
            addEntity(menubar);
            birdsGenerated = 0;
        }

        public override void stop()
        {

        }

        public override void renderThisFrame(long delta)
        {
            //Display what level we're on now.
            Text.displayMessage("Level " + level, Main.DisplayWidth / 2, Main.DisplayHeight - 50, 0, 0, 0, 1, 1, Text.WAVE_xonly);

            //Do logic for all the entities if not displaying a message.
            if (!displayingMessage)
            {
                foreach (Entity e in entities)
                {
                    e.doLogic();
                }
                foreach (Entity e in entities)
                {
                    e.move(delta);
                }
            }
            else
            {
                lvlFinNotify.doLogic();
            }

            //The level will generate 10 additional birds for each level.
            // For example, Level 1 will have 10, Level 3 will have 30, Level 50 will have 500, etc.
            if (birdsGenerated < level * 10 && !displayingMessage)
            {
                if (r.NextDouble() > 0.95)
                {
                    addEntity(new NormalBird(r.Next(Main.DisplayWidth), r.Next(Main.DisplayHeight - 60), r.Next(Main.DisplayWidth), r.Next(Main.DisplayHeight - 60)));
                    //addEntity(new NormalBird(500, 5, 500, 5)); //This makes all the birds start at the same place, and fly the same path. Nice for testing.
                    birdsGenerated++;
                }
            }
            else if (!thereAreStillBirdsAlive() && !displayingMessage)
            {
                level++;
                birdsGenerated = 0;
                addEntity(lvlFinNotify);
                displayingMessage = true;
            }
        }

        /// <summary>
        /// Determines if there are still bird entitied in the rendering List.
        /// </summary>
        /// <returns>Whether or not there are still birds alive.</returns>
        private bool thereAreStillBirdsAlive()
        {
            foreach (Entity e in entities)
            {
                if (e is Templates.Bird)
                    return true;
            }
            return false;
        }
    }
}
