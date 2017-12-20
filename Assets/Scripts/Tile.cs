using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
    CLASS: Tile
    FUNCTION: Holds the properties of a tile
 */

namespace CRGames_game
{
    public class Tile
    {
        // This tile's location in x/y coordinates (x = 0, y = 0 :: top-left)
        public int x;
        public int y;

        // Unique identifier, should correspond to index in map
        private int tileID;
        // Number of gang members on the tile
        private int gangStrength;
        // Corresponding int value of enum college
        private int college;
        // "Real world" tile object
        GameObject gameObject;

        /// <summary>
        /// Creates a tile with the given unique identifier.
        /// </summary>
        /// <param name="id">The tile's ID.</param>
        /// <param name="gob">The tile's associated GameObject.</param>
        public Tile(int id, GameObject gob)
        {
            tileID = id;
			gameObject = gob;
            gangStrength = 0;
            //college = 1;
        }

        /// <summary>
        /// Sets this tile's ID.
        /// </summary>
        /// <param name="id">The ID to set a the tile's ID.</param>
        public void setTileID(int id){
            this.tileID = id;
        }

        /// <summary>
        /// Returns the id of the Tile.
        /// </summary>
        /// <returns>The ID of this Tile.</returns>
        public int getID()
        {
            return tileID;
        }

        /// <summary>
        /// Returns the gang strength of the tile.
        /// <summary>
        /// <returns>The gang strength of the tile.</returns>
        public int getGangStrength()
        {
            return gangStrength;
        }

        /// <summary>
        /// Sets the gang strength of the tile to the given value.
        /// </summary>
        /// <param name="newStrength">The strength to set as the tile's gang strength.</param>
        /// <returns>True if setting the gang strength was successful.</returns>
        public bool setGangStrength(int newStrength)
        {
            if (newStrength < 0)
            {
                return false;
            }
            else
            {
                gangStrength = newStrength;
                return true;
            }
            
        }

        /// <summary>
        /// Returns the int value corresponding to the enum college of the tile.
        /// </summary>
        /// <returns>The college that this tile belongs to.</returns>
        public int getCollege()
        {
            return college;
        }

        /// <summary>
        /// Sets the college of the tile to the given number and returns true. If out of range (0-9 inclusive) then does nothing and returns false.
        /// </summary>
        /// <returns>True if a valid college was given and set correctly.</returns>
        public bool setCollege(int newCollege)
        {
            if (newCollege < 0 || newCollege > 9)
            {
                return false;
            }
            else
            {   
                college = newCollege;
                return true;
            }
            
        }

        /// <summary>
        /// Sets the GameObject associated with this tile.
        /// </summary>
        /// <param name="gob">The GameObject to set as the tile's object.</param>
        public void SetObject(GameObject gob){
            this.gameObject = gob;
        }
        
        /// <summary>
        /// Returns the GameObject associated with this tile.
        /// </summary>
        /// <returns>The GameObject associated with this tile.</returns>
        public GameObject GetObject(){
            return this.gameObject;
        }

        /// <summary>
        /// Sets the colour of this tile.
        /// </summary>
        /// <param name="color">The colour to set the tile to.</param>
		public void setColor(Color color) {
        
			gameObject.GetComponent<SpriteRenderer>().color = color;;
		}

        /// <summary>
        /// Resets the tile's colour to its college colour.
        /// </summary>
        /// <param name="collegeColours">The array of college colours to use to set this tile's colour.</param>
		public void resetColor(Color[] collegeColours) {
			setColor(collegeColours[(int)college]);
		}

        /// <summary>
        /// Sets the gang strength and college of the tile to 0 but leaves the id untouched.
        /// </summary>
        /// <returns>True on successful resetting.</returns>
        public bool reset()
        {
            gangStrength = 0;
            college = 0;
            return true;
        }
    }
}

