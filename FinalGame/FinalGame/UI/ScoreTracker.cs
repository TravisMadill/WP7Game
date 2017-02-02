using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalGame.UI
{
    /// <summary>
    /// This represents the tracker that keeps track of how many birds you've struck with stones.
    /// Originally for score tracking, but now it's for tracking birds struck, so it's not the "true" score.
    /// </summary>
    class ScoreTracker : Entity
    {
        /// <summary>
        /// The number of birds struck down.
        /// </summary>
        public int curScore = 0;

        /// <summary>
        /// Creates a new instance of the score tracker.
        /// </summary>
        public ScoreTracker()
            : base(0, 0, "ui_scoretag")
        {

        }

        /// <summary>
        /// Gets or sets the current score.
        /// </summary>
        public int CurrentScore
        {
            get { return curScore; }
            set { curScore = value; }
        }

        public override void doLogic()
        { 
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            Text.displayMessage(curScore.ToString(), width, 5, 0, 0, 0, 255, 2, Text.NONE);
            base.draw(sb);
        }
    }
}
