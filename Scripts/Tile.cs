using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPRTest1
{
    public class Tile
    {
        // Unique identifier, should correspond to index in map
        int tileID;
        // Number of gang members on the tile
        int gangStrength;
        // Corresponding int value of enum college
        int college;

        // Creates a tile with the given unique identifier
        public Tile(int id)
        {
            tileID = id;
            gangStrength = 0;
            college = 0;
        }

        // Returns the id of the tile
        public int getID()
        {
            return tileID;
        }

        // Returns the gang strength of the tile
        public int getGangStrength()
        {
            return gangStrength;
        }

        // Sets the gang strength of the tile to the given value
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

        // Returns the int value corresponding to the enum college of the tile
        public int getCollege()
        {
            return college;
        }

        // Sets the college of the tile to the given number and returns true. If out of range (0-9 inclusive) then does nothing and returns false
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

        // Sets the gang strength and college of the tile to 0 but leaves the id untouched
        public bool reset()
        {
            gangStrength = 0;
            college = 0;
            return true;
        }
    }
}

