using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
    CLASS: HumanPlayer
    FUNCTION: A human player skeleton class, to be used in future when AI players are implemented
 */

namespace CRGames_game
{
    class HumanPlayer : Player
    {
        // The amount of currency that this player has
        private int currency = 0;
        // The number of research points that this player has
        private int researchPoints = 0;
        // Whether this player has the PVC
        private bool hasPVC = false;

        /// <summary>
        /// Initialises the HumanPlayer.
        /// </summary>
        /// <param name="college">The player college</param>
        /// <param name="name">The player name</param>
        public HumanPlayer(int college, String name) : base(college , name)
        {

        }

        /// <summary>
        /// Stub. Used in future to upgrade gang members
        /// </summary>
        public void upgrade()
        {

        }

        /// <summary>
        /// Stub. Used in future to research new technology
        /// </summary>
        public void research()
        {

        }
    }
}
