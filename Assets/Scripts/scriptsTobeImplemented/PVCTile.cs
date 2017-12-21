using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
    CLASS: PVCTile
    FUNCTION: Handles the PVC minigame
 */

namespace CRGames_game
{
    class PVCTile : Tile
    {
        /// <summary>
        /// Initialises the PVC tile with an ID and an associated GameObject.
        /// </summary>
        /// <param name="id">The tile ID.</param>
        /// <param name="gob">The tile's GameObject.</param>
		public PVCTile(int id, GameObject gob) : base(id, gob)
        {

        }

        /// <summary>
        /// Starts the PVC minigame.
        /// </summary>
        /// <returns>True if the player wins the game.</returns>
        public bool startMiniGame()
        {
            return false;
        }
    }
}
