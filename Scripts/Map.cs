using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPRTest1
{
    public class Map
    {
        // Array of tiles
        Tile[] tiles;
        // Maximum number of tiles the Map can contain
        int size;

        // Creates a Map of given size and populates it with Tiles and a random PVCTile
        public Map(int size)
        {
            this.size = size;
            tiles = new Tile[size];
            for (int i = 0; i < size; i++)
            {
                tiles[i] = new Tile(i);
            }
            generatePVC();
        }
        
        // Returns the ID of a given Tile
        public int getTileId(Tile tile) {
            return tile.getID();
        }

        // Returns the gang strength of a given Tile
        public int getGangStrength(Tile tile)
        {
            return tile.getGangStrength();
        }

        // 'Moves' all gang members from a location Tile to a destination Tile. Returns false if no gang members at location
        public bool moveGangMember(Tile location, Tile destination)
        {
            if (location.getGangStrength() == 0)
            {
                return false;
            }
            else
            {
                destination.setGangStrength(destination.getGangStrength() + location.getGangStrength());
                location.setGangStrength(0);
                return true;
            }
        }

        // Generates a PVC tile in a random location in the map
        public void generatePVC() {
            Random rng = new Random();
            int rand = rng.Next(size);
            tiles[rand] = new PVCTile(rand);
        }

        public bool save(String fileName)
        {
            return false;
        }

        public bool load(String fileName)
        {
            return false;
        }

        // Resets every tile in the map
        public bool reset()
        {
            for (int i = 0; i < size; i++)
            {
                tiles[i].reset();
            }
            return true;
        }
    }
}
