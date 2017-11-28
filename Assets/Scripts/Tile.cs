using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        /// Creates a tile with the given unique identifier
        /// </summary>
        public Tile(int id)
        {
            tileID = id;
            gangStrength = 0;
            college = 0;
        }

        /// <summary>
        /// Sets this tile's ID
        /// </summary>
        public void setTileID(int id){
            this.tileID = id;
        }

        /// <summary>
        /// Returns the id of the Tile
        /// </summary>
        /// <returns>The ID of this Tile</returns>
        public int getID()
        {
            return tileID;
        }

        /// <summary>
        /// Returns the gang strength of the tile
        /// <summary>
        /// <returns>The gang strength of the tile</returns>
        public int getGangStrength()
        {
            return gangStrength;
        }

        /// <summary>
        /// Sets the gang strength of the tile to the given value
        /// </summary>
        /// <returns>True if setting the gang strength was successful</returns>
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
        /// Returns the int value corresponding to the enum college of the tile
        /// </summary>
        /// <returns>The college that this tile belongs to</returns>
        public int getCollege()
        {
            return college;
        }

        /// <summary>
        /// Sets the college of the tile to the given number and returns true. If out of range (0-9 inclusive) then does nothing and returns false
        /// </summary>
        /// <returns>True if a valid college was given and set correctly</returns>
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
        /// Sets the GameObject associated with this Tile
        /// </summary>
        public void SetObject(GameObject gob){
            this.gameObject = gob;
        }
        
        /// <summary>
        /// Returns the GameObject associated with this Tile
        /// </summary>
        /// <returns>The GameObject associated with this Tile</returns>
        public GameObject GetObject(){
            return this.gameObject;
        }

        /// <summary>
        /// Sets the gang strength and college of the tile to 0 but leaves the id untouched
        /// </summary>
        /// <returns>True on successful resetting</returns>
        public bool reset()
        {
            gangStrength = 0;
            college = 0;
            return true;
        }
    }
}

