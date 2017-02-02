using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalGame
{
	/// <summary>
	/// The state that the game is currently in. There should only be one instance of GameState.
    /// Managing states using abstract classes was an idea for easier state management, since it was kind
    /// of cumbersome in the Windows XNA game.
	/// </summary>
	public abstract class GameState
	{
		//Constants for determining the current game state
		public const int MENU_MAIN = 0;
        public const int MENU_PAUSE = 1;
		public const int GAME = 2;

		/// <summary>
		/// A list of entities that is used by the main class and game states
        /// for rendering and performing logic.
		/// </summary>
		public List<Entity> entities = new List<Entity>();

        /// <summary>
        /// The list of entities that are requesting to be added. They are added on the next update loop.
        /// </summary>
        public List<Entity> toAddList = new List<Entity>();

        /// <summary>
        /// The list of entities that are requesting to be removed. They are removed on the next update loop.
        /// </summary>
        public List<Entity> toRemoveList = new List<Entity>();

		/// <summary>
		/// The loader that allows resources to be loaded from anywhere.
		/// </summary>
		public static Microsoft.Xna.Framework.Content.ContentManager loader;

        /// <summary>
        /// Creates a new instance of GameState.
        /// Typically not called directly, rather from specific game states.
        /// </summary>
        public GameState() { }

		/// <summary>
		/// A one-time event to allow resources to be loaded and other such things
		/// </summary>
		public abstract void initialise();

		/// <summary>
		/// A one-time event that allows the state to unload content for the specific game state
		/// </summary>
		public abstract void stop();

		/// <summary>
		/// Allows the entity to update variables based on the time that
		/// has passed, and other such things that would vary from frame to frame
		/// </summary>
		/// <param name="delta">Time since the last render</param>
		public abstract void renderThisFrame(long delta);

		/// <summary>
		/// Adds an entity to the list of entities to be rendered and updated.
		/// </summary>
		/// <param name="e">The entity to add</param>
		public void addEntity(Entity e)
		{
			toAddList.Add(e);
		}

		/// <summary>
		/// Removes the entity from the list of entities that are being rendered and updated,
		/// if it exists.
		/// </summary>
		/// <param name="e">The entity to be removed</param>
		public void removeEntity(Entity e)
		{
			toRemoveList.Add(e);
		}

        /// <summary>
        /// Adds and removes the entities that are requesting to be added or removed.
        /// Occurs after doLogic to allow all entities to do what they need to do before actually adding/removing them.
        /// </summary>
        public void addRemoveEntities()
        {
            entities.AddRange(toAddList);
            foreach (Entity e in toRemoveList)
            {
                entities.Remove(e);
            }
            toAddList.Clear();
            toRemoveList.Clear();
        }

        /// <summary>
        /// Changes the state of the game. Usually from a menu to the game, but other modes can be added.
        /// </summary>
        /// <param name="state">The new state of the game. Use constants from this class.</param>
        public static void changeGameState(int state)
        {
            if (Main.currentGS != null)
                Main.currentGS.stop();

            switch (state)
            {
                case MENU_MAIN:
                    Main.currentGS = new State_Menu();
                    Main.currentGS.initialise();
                    break;
                case MENU_PAUSE:
                    Main.currentGS = new State_Menu(State_Menu.MENU_PAUSE);
                    Main.currentGS.initialise();
                    break;
                case GAME:
                    Main.currentGS = new State_Game();
                    Main.currentGS.initialise();
                    break;
            }
        }

        /// <summary>
        /// Gets a list of the possible collisions that this entity may be touching.
        /// Methods that call this decide how to react to the collisions.
        /// </summary>
        /// <param name="entityToCheck">The entity to check for collisions.</param>
        /// <returns>An entity list object containing all possible collisions.</returns>
        public List<Entity> getPossibleCollision(Entity entityToCheck)
        {
            List<Entity> returning = new List<Entity>();
            foreach (Entity e in entities)
            {
                if (e != entityToCheck && entityToCheck.HitBox.Intersects(e.HitBox))
                {
                    returning.Add(e);
                }
            }
            return returning;
        }
    }
}
