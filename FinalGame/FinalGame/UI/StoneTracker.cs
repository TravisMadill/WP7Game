using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalGame.UI
{
    /// <summary>
    /// This class tracks all the stones that have been thrown throughout this game.
    /// </summary>
    class StoneTracker : Entity
    {
        /// <summary>
        /// The number of stones that have been thrown.
        /// </summary>
        public int stonesUsed = 0;

        /// <summary>
        /// Creates a new instance of the stone tracker.
        /// </summary>
        public StoneTracker()
            : base(0, 50, "ui_stoneUsed")
        {

        }

        /// <summary>
        /// Gets or sets the number of stones thrown.
        /// </summary>
        public int StonesThrown
        {
            get { return stonesUsed; }
            set { stonesUsed = value; }
        }

        public override void doLogic()
        {
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            Text.displayMessage(stonesUsed.ToString(), width, 50, 0, 0, 0, 1, 2, Text.NONE);
            base.draw(sb);
        }
    }
}
