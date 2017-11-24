using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRGames_game
{
    public class Map
    {
        // Array of tiles
        Tile[] tiles;
        // Maximum number of tiles the Map can contain
        int size;
        int width;
        int height;

        // Creates a Map of given size and populates it with Tiles and a random PVCTile
        public Map(int width, int height, Sprite[] sprites)
        {
            this.size = width * height;
            tiles = new Tile[size];

            this.width = width;
            this.height = height;

            GameObject mapPivot = new GameObject();
            mapPivot.name = "Map Pivot";
            mapPivot.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            
            for (int y = 0; y < height; y++){
                for (int x = 0; x < width; x++){
                    GameObject gob = new GameObject();
                    gob.name = "tile_" + x + "x" + y;
                    gob.transform.parent = mapPivot.transform;
                    SpriteRenderer rend = gob.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                    rend.sprite = sprites[x + (y * width)];
                    gob.transform.localPosition = new Vector3((0.75f * x), -(0.75f * y), 0.0f);
                    Tile tile = new Tile(x + (y * width));
                    tile.x = x;
                    tile.y = y;
                    tiles[x + (y * width)] = tile;
                }
            }

            /*for (int i = 0; i < size; i++)
            {
                tiles[i] = new Tile(i);
            }*/
            generatePVC();
        }
        
		public Tile getTileByID(int id){
			return tiles [id];
		}

        public Tile getTileAtPosition(int x, int y){
            return tiles[x + (y * this.width)];
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

        // Resets every tile in the map
        public bool reset()
        {
            for (int i = 0; i < size; i++)
            {
                tiles[i].reset();
            }
            return true;
        }

		public int getNumberOfTiles(){
			return tiles.Length;
		}

        public void setTile(int id, Tile tile){
            // TODO search for the array location based on ID,
            // seeing as IDs may change
            tiles[id] = tile;
        }
    }
}
