using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRGames_game
{
    public class Map
    {
        // Store the tile prefab to istantiate
        public GameObject tilePrefab;

        // Array of tiles
        Tile[] tiles;
        // Maximum number of tiles the Map can contain
        int size;
        int width;
        int height;
        
        // Creates a Map of given size and populates it with Tiles and a random PVCTile
        public Map(int width, int height, Sprite[] sprites, GameObject tilePrefab)
        {   
            // Initialise the map
            this.tilePrefab = tilePrefab;
            this.size = width * height;
            
            this.width = width;
            this.height = height;

            tiles = new Tile[size];

            // Create an object to hold all of the map tiles
            GameObject mapPivot = new GameObject();
            mapPivot.name = "Map Pivot";
            mapPivot.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            
            // Create the map
            for (int y = 0; y < height; y++){
                for (int x = 0; x < width; x++){
                    // Instantiate the new tile
                    GameObject gob = GameObject.Instantiate<GameObject>(tilePrefab) as GameObject;
                    gob.name = "tile_" + x + "x" + y;
                    gob.transform.parent = mapPivot.transform;

                    // Get the renderer and set the tile sprite
                    SpriteRenderer rend = gob.GetComponent<SpriteRenderer>();
                    rend.sprite = sprites[x + (y * width)];

                    // Set the tile position
                    gob.transform.localPosition = new Vector3((0.75f * x), -(0.75f * y), 0.0f);
                    
                    // Add the interaction script to the tile
                    TileInteraction interact = gob.AddComponent<TileInteraction>() as TileInteraction;

                    // Create a new Tile object to hold the properties of this tile
                    Tile tile = new Tile(x + (y * width));
                    tile.x = x;
                    tile.y = y;
                    tiles[x + (y * width)] = tile;

                    interact.tile = tile;
                }
            }

            GameObject.FindGameObjectWithTag("MainCamera").transform.parent.gameObject.GetComponent<MapCamera>().SetMaxCoord(0.75f * width, 0.75f * height);

            // Create a PVC tile
            generatePVC();
        }
        
        /// <summary>
        /// Returns a Tile with a given ID
        /// </summary>
        /// <returns>The Tile with the given ID</returns>
		public Tile getTileByID(int id){
			return tiles [id];
		}

        /// <summary>
        /// Returns a Tile at a given position
        /// </summary>
        /// <returns>The Tile at the given position</returns>
        public Tile getTileAtPosition(int x, int y){
            return tiles[x + (y * width)];
        }

        /// <summary>
        /// Returns the ID of a given Tile
        /// </summary>
        /// <returns>The ID of the given Tile</returns>
        public int getTileId(Tile tile) {
            return tile.getID();
        }

        /// <summary>
        /// Returns the gang strength of a given Tile
        /// </summary>
        /// <returns>The gang strength of a given Tile</returns>
        public int getGangStrength(Tile tile)
        {
            return tile.getGangStrength();
        }

        /// <summary>
        /// Moves all gang members from a location Tile to a destination Tile. Returns false if no gang members at location
        /// </summary>
        /// <returns>True if movement is successful, False otherwise</returns>
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

        /// <summary>
        /// Generates a PVC tile in a random location in the map
        /// </summary>
        public void generatePVC() {
            System.Random rng = new System.Random();
            int rand = rng.Next(size);
            PVCTile tile = new PVCTile(tiles[rand].getID());
            tile.x = tiles[rand].x;
            tile.y = tiles[rand].y;
            tiles[rand] = tile;
        }
        
        public bool save(String fileName)
        {
            return false;
        }

        public bool load(String fileName)
        {
            return false;
        }

        /// <summary>
        /// Resets all tiles
        /// </summary>
        /// <returns>True if reset is successful, False otherwise</returns>
        public bool reset()
        {
            for (int i = 0; i < size; i++)
            {
                tiles[i].reset();
            }
            return true;
        }

        /// <summary>
        /// Gets the number of Tiles in the Map
        /// </summary>
        /// <returns>Number of Tiles in the Map</returns>
		public int getNumberOfTiles(){
			return tiles.Length;
		}

        /// <summary>
        /// Set a Tile at a given ID to a given Tile
        /// </summary>
        public void setTile(int id, Tile tile){
            // TODO search for the array location based on ID,
            // seeing as IDs may change
            tiles[id] = tile;
        }
    }
}
