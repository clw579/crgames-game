using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
    CLASS: AIPlayer
    FUNCTION: Currently unused, this is where the enemy AI would be implemented in the future.
 */

namespace CRGames_game
{
    class AIPlayer : Player
    {
        /// <summary>
        /// Implements the AI for AI opponents
        /// </summary>
        /// <param name="college">The college that this player belongs to.</param>
        /// <param name="name">The name of this player.</param>
        public AIPlayer(int college, String name) : base(college, name)
        {

        }
    }
}
