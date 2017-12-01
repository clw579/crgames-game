using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace CRGames_game
{
    enum colleges
    {
        Unknown,
        Alcuin,
        Goodricke,
        Langwith,
        Constantine,
        Halifax,
        Vanbrugh,
        Derwent,
        James,
        Wentworth,
    }

    class GameManager : MonoBehaviour
    {
		// Lookup table for enum colleges
		private string[] collegeLookupTable = new string[]
		{
			"Unknown",
			"Alcuin",
			"Goodricke",
			"Langwith",
			"Constantine",
			"Halifax",
			"Vanbrugh",
			"Derwent",
			"James",
			"Wentworth",
		};

		// Prefab of the Tile GameObject
		public GameObject tilePrefab;
		// Array of Sprites that make up the Map
		public Sprite[] mapSprites;
		// Store the Map's width and height
		public int mapWidth;
		public int mapHeight;
		// Gang colours
		public Color ColourAlcuin;
		public Color ColourGoodricke;
		public Color ColourLangwith;
		public Color ColourConstantine;
		public Color ColourHalifax;
		public Color ColourVanbrugh;
		public Color ColourDerwent;
		public Color ColourJames;
		public Color ColourWentworth;

		// Path of the current save file
		private String savePath = "gamestates.json";

		// The current turn number
		private int currentTurn;
		// The index of the current Player
		private int currentPlayer;

		// Array of Players in the game
		private Player[] players = new Player[10];
        private List<Player> players1 = new List<Player>();     // a dynamic list version of players, its easier to use
		// The Map
		private Map map;

		// The tile that was last clicked on, needed for movement and such things
		private Tile lastClickedTile = null;

        //public GameObject GUIManager;
        //private UIManager UIManagerScript;
		public UIManager uiManager;
        
		void Start()
        {
			// Load the Map Sprites
			mapSprites = Resources.LoadAll<Sprite>("uni_map");

			// Display the Map
			GenerateMap ();

            // get access to scripts from the UI manager
            //UIManagerScript = GUIManager.GetComponent<UIManager>();

            currentPlayer = 0; // sets inital player to player 1
            currentTurn = 1;   //sets the inital turn to 1





            players1.Add(new Player(1, "Sally"));  // tests to be removed
            players1[0].AddOwnedTiles(new Tile(1));
            players1[0].AddOwnedTiles(new Tile(2));


            players1.Add(new Player(2, "Bob"));
            players1[1].AddOwnedTiles(new Tile(3));
            players1[1].AddOwnedTiles(new Tile(4));

            //sets the first player and number of gang members when the game starts

            //UIManagerScript.updateGangMembers(players1[currentPlayer].GetNumberOfGangMembers().ToString(), players1[currentPlayer].GetName());


        }

        void Update()
        {
			if (lastClickedTile != null) {
				uiManager.RefreshTileMenu (lastClickedTile, lookupCollege (lastClickedTile.getCollege ()));
			}
        }

		/// <summary>
		/// Returns the string value of a college corresponding to its enum value
		/// </summary>
		/// <param name="college">College.</param>
		public string lookupCollege(int college) {
			return collegeLookupTable[college];
		}

		public Tile getLastClickedTile()
		{
			return lastClickedTile;
		}

		/// <summary>
		/// Generates the Map object
		/// </summary>
		void GenerateMap(){
			map = new Map(12, 6, mapSprites, tilePrefab);
		}

		/// <summary>
		/// Moves the game to the next turn.
		/// </summary>
		public void NextTurn(){
			currentTurn++;

			currentPlayer++;

			if (currentPlayer > players.Length) {
				currentPlayer = 0;
			}

			players [currentPlayer].AlertItsMyTurn ();
		}

		/// <summary>
		/// Loads the game.
		/// </summary>
		/// <returns>Success of loading game.</returns>
		bool LoadGame(){
			// Read the save game file (currently only allows for one saved game)
			string filePath = Path.Combine(Application.dataPath, savePath);
			StreamReader reader = new StreamReader(filePath);
        	string loadJson = reader.ReadToEnd();
        	reader.Close();

			// Translate the loaded data into a GameState JSON object
			GameStateJSON gameState = JsonUtility.FromJson<GameStateJSON>(loadJson);

			// Extract the saved Map from the gameState
			Map loadMap = new Map(gameState.map.width, gameState.map.height, mapSprites, tilePrefab);
			
			// Initialise each Tile in the saved Map
			for (int i = 0; i < gameState.map.tiles.Length; i++){
				Tile loadTile = new Tile(gameState.map.tiles[i].tileID);
				loadTile.setGangStrength(gameState.map.tiles[i].gangStrength);
				loadTile.setCollege(gameState.map.tiles[i].college);
				loadTile.x = gameState.map.tiles[i].x;
				loadTile.y = gameState.map.tiles[i].y;
				loadMap.setTile(gameState.map.tiles[i].positionInArray, loadTile);
			}

			// Create an array of Players to store the loaded Player values
			Player[] loadPlayers = new Player[gameState.numberOfPlayers];

			// Initialise each saved Player
			for (int i = 0; i < gameState.numberOfPlayers; i++){
				Player loadPlayer = new Player(gameState.players[i].college, gameState.players[i].name);
				loadPlayers[gameState.players[i].positionInArray] = loadPlayer;
			}

			// Finalise loading
			map = loadMap;
			players = loadPlayers;
			currentTurn = gameState.currentTurn;
			currentPlayer = gameState.currentPlayer;

			// If we made it this far, loading was successful
			return true;
		}

		/// <summary>
		/// Saves the game
		/// </summary>
		/// <returns>Success of saving the game.</returns>
		bool SaveGame(){
			// Create JSON representations of the data needing to be stored
			GameStateJSON gameStateJson = new GameStateJSON ();
			PlayerJSON[] playersJson = new PlayerJSON[players.Length];
			MapJSON mapJson = new MapJSON ();
			TileJSON[] tileJson = new TileJSON[map.getNumberOfTiles()];

			// Save every Player's data as a JSON object
			for (int i = 0; i < players.Length; i++) {
				playersJson[i] = new PlayerJSON();
				playersJson [i].college = players [i].GetCollege ();
				playersJson [i].name = players [i].GetName ();
				playersJson[i].positionInArray = i;
			}

			// Store each Tile's data as a JSON object
			for (int i = 0; i < map.getNumberOfTiles(); i++) {
				tileJson[i] = new TileJSON();
				tileJson[i].tileID = i;
				tileJson[i].gangStrength = map.getGangStrength (map.getTileByID(i));
				tileJson[i].college = map.getTileByID (i).getCollege ();
				tileJson[i].positionInArray = i;
				tileJson[i].x = map.getTileByID(i).x;
				tileJson[i].y = map.getTileByID(i).y;
			}

			// Store Map data as a JSON object
			mapJson.numberOfTiles = tileJson.Length;
			mapJson.tiles = tileJson;
			mapJson.width = mapWidth;
			mapJson.height = mapHeight;

			// Store game state related values as JSON
			gameStateJson.numberOfPlayers = playersJson.Length;
			gameStateJson.map = mapJson;
			gameStateJson.players = playersJson;
			gameStateJson.currentTurn = currentTurn;
			gameStateJson.currentPlayer = currentPlayer;

			// Stringify JSON
			string saveJson = JsonUtility.ToJson(gameStateJson);
			
			// Build the path to the save file
			string filePath = Path.Combine(Application.dataPath, savePath);

			// Write the JSON string to the save file
			File.WriteAllText(filePath, saveJson); 


			// If we made it this far, saving was successful
			return true;
		}

		/// <summary>
		/// Ends the turn.
		/// </summary>
		/// <returns>The turn.</returns>
		public void EndTurn(){

            players1[currentPlayer].allocateGangMembers(); // alocates the gang members to an attribute in Player

            if (currentPlayer < players1.Count -1)  // rotates around the current players
            {
                currentPlayer += 1;
            }
            else
            {
                currentPlayer = 0;
            }
            // loads the new player gang members by calling the update gang memebers function in the UI manager
            //UIManagerScript.updateGangMembers(players1[currentPlayer].GetNumberOfGangMembers().ToString(), players1[currentPlayer].GetName());

            


        }

		/// <summary>
		/// Works out what to do when a tile has been clicked on (e.g. move, attack);
		/// </summary>
		public void TileClicked(Tile tile){
			Debug.Log("A tile was clicked somewhere");

			if (lastClickedTile == null){
				// TODO Attack
				lastClickedTile = tile;
			}else{
				// Move all of the units on the last clicked Tile to the current Tile
				if (lastClickedTile.getCollege() == tile.getCollege() || tile.getCollege() == (int)colleges.Unknown){
//					int strength = tile.getGangStrength();
//					tile.setGangStrength(0);
//					lastClickedTile.setGangStrength(lastClickedTile.getGangStrength() + strength);
//					lastClickedTile = null;
					// Why not
					map.moveGangMember(lastClickedTile, tile);
				}else{
					lastClickedTile = tile;
				}
			}
		}
    }

	public static class GameState {

	}
}
