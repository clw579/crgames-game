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
		// Sprite to use to represent gang members
		public Sprite gangMemberSprite;
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

		// Gang colour array
		Color[] collegeColours;

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

		// The UI Canvas
		public UIManager uiManager;

		// The Combat Engine
		private CombatEngine combatEngine = new CombatEngine();
        
		void Start()
        {
			// Load the Map Sprites
			mapSprites = Resources.LoadAll<Sprite>("uni_map");

			// Create an array containing college colours
			collegeColours = new Color[10] {
				new Color(255, 255, 255, 1),
				ColourAlcuin,
				ColourGoodricke,
				ColourLangwith,
				ColourConstantine,
				ColourHalifax,
				ColourVanbrugh,
				ColourDerwent,
				ColourJames,
				ColourWentworth,
			};

			// Display the Map
			GenerateMap ();

            currentPlayer = 0; // sets inital player to player 1
            currentTurn = 1;   //sets the inital turn to 1

            players1.Add(new Player(1, "Sally"));  // tests to be removed
            players1[0].AddOwnedTiles(map.getTileAtPosition(0,0));
            players1[0].AddOwnedTiles(map.getTileAtPosition(1, 0));


            players1.Add(new Player(2, "Bob"));
            players1[1].AddOwnedTiles(map.getTileAtPosition(2, 0));
            players1[1].AddOwnedTiles(map.getTileAtPosition(2, 1));

            //sets the first player and number of gang members when the game starts

            // set intial UI elements for the first player
            uiManager.initialiseUI(collegeLookupTable[players1[currentPlayer].GetCollege()], players1[currentPlayer].GetNumberOfGangMembers(), players1[currentPlayer].GetName());




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

		public Color[] getCollegeColours() {
			return collegeColours;
		}

		public Tile getLastClickedTile()
		{
			return lastClickedTile;
		}

		/// <summary>
		/// Generates the Map object
		/// </summary>
		void GenerateMap(){
			map = new Map(24, 13, mapSprites, tilePrefab, gangMemberSprite);
		}

		/// <summary>
		/// Moves the game to the next turn.
		/// </summary>
		public void NextTurn(){

            currentTurn++;
            currentPlayer++;

            if (currentPlayer > players1.Count - 1) {
				currentPlayer = 0;
			}

            players1[currentPlayer].allocateGangMembers(); // alocates the gang members to an attribute in Player
            players1[currentPlayer].AlertItsMyTurn ();

            uiManager.RefreshCurrentPlayerInfo(collegeLookupTable[players1[currentPlayer].GetCollege()], players1[currentPlayer].GetNumberOfGangMembers(), players1[currentPlayer].GetName());
            

       
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
			Map loadMap = new Map(gameState.map.width, gameState.map.height, mapSprites, tilePrefab, gangMemberSprite);

			combatEngine.SetPVCBonus(gameState.combatEngine.pvcBonus);
			combatEngine.SetHiddenDamageModifier(gameState.combatEngine.hiddenDamageModifier);
			
			// Initialise each Tile in the saved Map
			for (int i = 0; i < gameState.map.tiles.Length; i++){
				Tile loadTile = new Tile(gameState.map.tiles[i].tileID, map.tileObjects[gameState.map.tiles[i].x + (gameState.map.tiles[i].y * map.getWidth())]);
				loadTile.setGangStrength(gameState.map.tiles[i].gangStrength);
				loadTile.setCollege(gameState.map.tiles[i].college);
				loadTile.x = gameState.map.tiles[i].x;
				loadTile.y = gameState.map.tiles[i].y;
				loadMap.setTile(gameState.map.tiles[i].positionInArray, loadTile);
			}

			// Create an array of Players to store the loaded Player values
			Player[] loadPlayers = new Player[gameState.numberOfPlayers];
			players1 = new List<Player>();
			
			// Initialise each saved Player
			for (int i = 0; i < gameState.numberOfPlayers; i++){
				Player loadPlayer = new Player(gameState.players[i].college, gameState.players[i].name);
				loadPlayers[gameState.players[i].positionInArray] = loadPlayer;
				players1.Add(loadPlayer);
			}

			for (int i = 0; i < gameState.collegeColours.Length; i++){
				collegeColours[i].r = gameState.collegeColours[i].r;
				collegeColours[i].g = gameState.collegeColours[i].g;
				collegeColours[i].b = gameState.collegeColours[i].b;
				collegeColours[i].a = gameState.collegeColours[i].a;
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
			ColourJSON[] colourJson = new ColourJSON[collegeColours.Length];
			CombatEngineJSON combatEngineJson = new CombatEngineJSON();

			combatEngineJson.pvcBonus = combatEngine.GetPVCBonus();
			combatEngineJson.hiddenDamageModifier = combatEngine.GetHiddenDamageModifier();

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

			for (int i = 0; i < collegeColours.Length; i++){
				colourJson[i].r = collegeColours[i].r;
				colourJson[i].g = collegeColours[i].g;
				colourJson[i].b = collegeColours[i].b;
				colourJson[i].a = collegeColours[i].a;
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
			gameStateJson.combatEngine = combatEngineJson;
			gameStateJson.collegeColours = colourJson;
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
	
		/// <summary>
		/// Works out what to do when a tile has been clicked on (e.g. move, attack).
		/// </summary>
		public void TileClicked(Tile tile)
		{
			// TODO:
			//Stop colleges from attacking themselves
			//
			// Stops highlighting targets from the previously clicked on tile
			if (lastClickedTile != null) {
				Tile[] adjacents = map.getAdjacent (lastClickedTile);
				for (int i = 0; i < 4; i++) {
					if (adjacents[i] != null) {
						adjacents[i].resetColor(collegeColours);
					}
				}
			}
			// Highlights in red the available targets from the clicked on tile
			if (tile.getGangStrength() > 0) {
				Tile[] adjacents = map.getAdjacent(tile);
				for (int i = 0; i < 4; i++) {
					if (adjacents[i] != null) {
						adjacents[i].setColor(Color.red);
					}
				}
			}

			lastClickedTile = tile;
		}

		/// <summary>
		/// Attempts to attack a tile from the last clicked tile.
		/// </summary>
		/// <param name="tile">Tile to attack.</param>
		public void requestAttack(Tile tile)
		{
			if (lastClickedTile != null) {
				if (map.isAdjacent(lastClickedTile, tile)) {
					int[] newStrengths = new int[2];
					newStrengths = combatEngine.Attack(lastClickedTile.getGangStrength(), tile.getGangStrength());
					lastClickedTile.setGangStrength(newStrengths[0]);
					tile.setGangStrength(newStrengths[1]);
				}
			}
		}
    }

	public static class GameState {

	}
}
