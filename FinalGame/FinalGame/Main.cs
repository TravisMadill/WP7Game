using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace FinalGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        //Pregenerated things.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// The current state of the game. The state determines what is currently happening in the game.
        /// </summary>
        public static GameState currentGS;

        /// <summary>
        /// The width of the display: 800 pixels
        /// </summary>
        public static int DisplayWidth;

        /// <summary>
        /// The height of the display: 480 pixels
        /// </summary>
        public static int DisplayHeight;

        /// <summary>
        /// The current highscore in this game.
        /// </summary>
        public static int highScore = 0;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            DisplayWidth = graphics.GraphicsDevice.Viewport.Width;
            DisplayHeight = graphics.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Set the game state to the main menu.
            changeGameState(GameState.MENU_MAIN);

            //Get the high score from the save file.
            highScore = loadHighScore();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //Stop the game state.
            currentGS.stop();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                currentGS.stop();
                this.Exit();
            }

            //Text.displayMessage("Running slowly?: " + gameTime.IsRunningSlowly, DisplayWidth - Text.getWidth("Running slowly?: " + gameTime.IsRunningSlowly), DisplayHeight - 25, 0.5f, 0.5f, 0.5f, 1, 1, 0);

            //Update everything. See the game state's respective state for a more in-depth look.
            Entity.updateTouchPanel();
            currentGS.renderThisFrame(gameTime.ElapsedGameTime.Milliseconds);
            
            //Add and remove the entities that were requesting to do so.
            currentGS.addRemoveEntities();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); //For some reason, any other colour makes the screen flash like crazy.


            spriteBatch.Begin();
                if (currentGS is State_Game) //Draw the tree background if we are playing the game.
                {
                    spriteBatch.Draw(State_Game.background, new Vector2(0, 0), Color.White);
                }

                if (currentGS is State_Menu) //Draw the logo of the game if we are in a menu.
                {
                    float logoRotation = (float)Math.Sin((double)DateTime.Now.Ticks / 2000000.0) * 0.1f;
                    spriteBatch.Draw(State_Menu.logo, new Rectangle(250, 125, State_Menu.logo.Width, State_Menu.logo.Height), null, Color.White, logoRotation-0.15f, new Vector2(State_Menu.logo.Width / 2, State_Menu.logo.Height / 2), SpriteEffects.None, 1);
                }

                foreach (Entity e in currentGS.entities) //Draw every alive object in the game
                {
                    e.draw(spriteBatch);
                }
                //Text.displayMessage("Entities: " + currentGS.entities.Count, 0, 100, 1, 1, 1, 1, 1, 0);

                Text.draw(spriteBatch); //Draw all the strings requesting to be drawn
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes the state of the game. Usually from a menu to the game, but other modes can be added.
        /// Use this method if static access is not required or the ContentManager is null.
        /// </summary>
        /// <param name="state">The new state of the game. Use constants from the GameState class.</param>
        public void changeGameState(int state)
        {
            if (currentGS != null)
                currentGS.stop();

            if (GameState.loader == null)
                GameState.loader = this.Content;

            switch (state)
            {
                case GameState.MENU_MAIN:
                    currentGS = new State_Menu(State_Menu.MENU_MAIN);
                    currentGS.initialise();
                    break;
                case GameState.MENU_PAUSE:
                    currentGS = new State_Menu(State_Menu.MENU_PAUSE);
                    currentGS.initialise();
                    break;
                case GameState.GAME:
                    currentGS = new State_Game();
                    currentGS.initialise();
                    break;
            }
        }

        /// <summary>
        /// This is how Microsoft told me to properly save things on Windows Phone.
        /// Anyway, this saves the specified score to the save file.
        /// </summary>
        /// <param name="highScore">The score to save.</param>
        public static void saveHighScore(int highScore)
        {
            IsolatedStorageFile saveStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream fs = null;
            using (fs = saveStorage.CreateFile("save.sav")) //This statement is new to me, but it looks like it can create new objects and such.
            {
                if (fs != null)
                {
                    byte[] bytes = System.BitConverter.GetBytes(highScore);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// This is how Microsoft told me to properly load things on Windows Phone.
        /// Anyway, this loads the current high score from the save file.
        /// </summary>
        /// <returns>The saved high score, zero if there was a problem loading or there is no highscore.</returns>
        public static int loadHighScore()
        {
            using (IsolatedStorageFile saveStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (saveStorage.FileExists("save.sav"))
                {
                    using (IsolatedStorageFileStream fs = saveStorage.OpenFile("save.sav", System.IO.FileMode.Open))
                    {
                        if (fs != null)
                        {
                            byte[] bytes = new byte[4];
                            int count = fs.Read(bytes, 0, 4);
                            if (count > 0)
                            {
                                return System.BitConverter.ToInt32(bytes, 0);
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
