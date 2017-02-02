using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalGame.Templates
{
    /// <summary>
    /// This class represents a bird. As in, anything that can be hit by a stone.
    /// Beyond being an slight extension of Entity, there's nothing really special about it.
    /// </summary>
    public abstract class Bird : Entity
    {
        /// <summary>
        /// Creates a new bird object.
        /// </summary>
        /// <param name="x">The x coordinate of the bird.</param>
        /// <param name="y">The y coordinate of the bird.</param>
        /// <param name="spriteName">The sprite name of the bird.</param>
        public Bird(int x, int y, string spriteName)
            : base(x, y, spriteName) { }
    }
}
