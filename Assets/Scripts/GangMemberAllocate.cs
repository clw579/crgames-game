using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    CLASS: GangMemberAllocate
    FUNCTION: Manages gang member allocation per player
 */

namespace CRGames_game
{
    public class GangMemberAllocate : MonoBehaviour
    {
        // The current map
        Map map;
        // The player to manage
        Player aPlayer;

        /// <summary>
        /// Initialises GangMemberAllocate.
        /// </summary>
        /// <param name="map">The current map.</param>
        /// <param name="aPlayer">The player to manager.</param>
        public GangMemberAllocate(Map map, Player aPlayer)
        {
            this.map = map;
            this.aPlayer = aPlayer;
        }

        /// <summary>
        /// Calculates the number of gang members owned by a player.
        /// </summary>
        /// <returns>The number of gang members owned by a player.</returns>
        public int CalculateMembers()
        {
            int noOfGangMembers = aPlayer.GetOwnedTiles().Count;
            
            return noOfGangMembers;
        }
    }
}
