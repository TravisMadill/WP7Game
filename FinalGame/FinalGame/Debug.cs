using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalGame
{
    /// <summary>
    /// This is pretty much the same story as the Windows XNA game, but for phones!
    /// It's more or less a shortcut for outputting things to console.
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// Writes the object to console using its string representation.
        /// </summary>
        /// <param name="message">The message to feed through the console.</param>
        public static void output(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
