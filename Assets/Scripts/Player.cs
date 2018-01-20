using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
    CLASS: Player
    FUNCTION: Keeps track of player properties such as college, name and owned tiles
 */

namespace CRGames_game
{
    public class Player
    {
        // The player's college
        private int college;
        // The player's name
        private String name;
        // The tiles that the player owns
        private List<Tile> ownedTiles;
        // The number of gang members that this player owns
        private int noOfGangMembers;		

        /// <summary>
        /// Initialises the player with both a college and a name.
        /// </summary>
        /// <param name="college">The player's college.</param>
        /// <param name="name">The player's name.</param>
        public Player(int college, String name)
        {
            this.college = college;
            this.name = name;
            this.ownedTiles = new List<Tile>() ;
            noOfGangMembers = 0;
        }

        /// <summary>
        /// Alerts the player that it's their turn, used by AIPlayer to carry out turn logic.
        /// </summary>
        public void AlertItsMyTurn(){

		}

        /// <summary>
        /// Gets the player's college.
        /// </summary>
        /// <returns>The player's college.</returns>
		public int GetCollege (){
			return college;
		}

        /// <summary>
        /// Gets the player's name.
        /// </summary>
        /// <returns>The player's name.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Get the tiles owned by the player.
        /// </summary>
        /// <returns>The tiles owned by the player.</returns>
        public List<Tile> GetOwnedTiles()
        {
            return ownedTiles;
        }

        /// <summary>
        /// Adds a tile to the list of the player's owned tiles.
        /// </summary>
        /// <param name="tile">The tile to add.</param>
        public void AddOwnedTiles(Tile tile)
        {
            ownedTiles.Add(tile);
        }

        /// <summary>
        /// Gets the number of gang members owned by the player.
        /// </summary>
        /// <returns>The number of gang members owned by the player.</returns>
        public int GetNumberOfGangMembers()
        {
            return noOfGangMembers;
        }
        
        /// <summary>
        /// Sets the number of gang members owned by the player to the number of tiles they own and returns the value.
        /// </summary>
        /// <returns>The number of gang members that the palyer owns.</returns>
        public int allocateGangMembers()
        {
            this.noOfGangMembers += ownedTiles.Count;
            return noOfGangMembers;
        }

        /// <summmary>
        /// Set the gang strength of the player.
        /// </summary>
        /// <param name="noOfGangMembers">The strength to set.</param>
        public void setGangStrength(int noOfGangMembers)
        {
            this.noOfGangMembers = noOfGangMembers;
        }
    }
}
