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
		private int _currentTurn;
		// The index of the current Player
		private int _currentPlayer;

		// Array of Players in the game
		private Player[] _players;
		// The Map
		private Map _map;

		// The tile that was last clicked on, needed for movement and such things
		private Tile lastClickedTile = null;
        
		void Start()
        {
			// Load the Map Sprites
			mapSprites = Resources.LoadAll<Sprite>("uni_map");

			// Display the Map
			GenerateMap ();
        }

		/// <summary>
		/// Generates the Map object
		/// </summary>
		void GenerateMap(){
			Map map = new Map(48, 27, mapSprites, tilePrefab);

			_map = map;
		}

		/// <summary>
		/// Moves the game to the next turn.
		/// </summary>
		void NextTurn(){
			this._currentTurn++;

			this._currentPlayer++;

			if (this._currentPlayer > this._players.Length) {
				this._currentPlayer = 0;
			}

			_players [this._currentPlayer].AlertItsMyTurn ();
		}

		/// <summary>
		/// Loads the game.
		/// </summary>
		/// <returns>Success of loading game.</returns>
		bool LoadGame(){
			// Read the save game file (currently only allows for one saved game)
			string filePath = Path.Combine(Application.dataPath, this.savePath);
			StreamReader reader = new StreamReader(filePath);
        	string load_json = reader.ReadToEnd();
        	reader.Close();

			// Translate the loaded data into a GameState JSON object
			GameState_JSON game_state = JsonUtility.FromJson<GameState_JSON>(load_json);

			// Extract the saved Map from the game_state
			Map load_map = new Map(game_state.map.width, game_state.map.height, mapSprites, tilePrefab);
			
			// Initialise each Tile in the saved Map
			for (int i = 0; i < game_state.map.tiles.Length; i++){
				Tile load_tile = new Tile(game_state.map.tiles[i].tileID);
				load_tile.setGangStrength(game_state.map.tiles[i].gangStrength);
				load_tile.setCollege(game_state.map.tiles[i].college);
				load_tile.x = game_state.map.tiles[i].x;
				load_tile.y = game_state.map.tiles[i].y;
				load_map.setTile(game_state.map.tiles[i].positionInArray, load_tile);
			}

			// Create an array of Players to store the loaded Player values
			Player[] load_players = new Player[game_state.numberOfPlayers];

			// Initialise each saved Player
			for (int i = 0; i < game_state.numberOfPlayers; i++){
				Player load_player = new Player(game_state.players[i].college, game_state.players[i].name);
				load_players[game_state.players[i].positionInArray] = load_player;
			}

			// Finalise loading
			this._map = load_map;
			this._players = load_players;
			this._currentTurn = game_state.currentTurn;
			this._currentPlayer = game_state.currentPlayer;

			// If we made it this far, loading was successful
			return true;
		}

		/// <summary>
		/// Saves the game
		/// </summary>
		/// <returns>Success of saving the game.</returns>
		bool SaveGame(){
			// Create JSON representations of the data needing to be stored
			GameState_JSON game_state_json = new GameState_JSON ();
			Player_JSON[] players_json = new Player_JSON[this._players.Length];
			Map_JSON map_json = new Map_JSON ();
			Tile_JSON[] tile_json = new Tile_JSON[this._map.getNumberOfTiles()];

			// Save every Player's data as a JSON object
			for (int i = 0; i < this._players.Length; i++) {
				players_json[i] = new Player_JSON();
				players_json [i].college = this._players [i].GetCollege ();
				players_json [i].name = this._players [i].GetName ();
				players_json[i].positionInArray = i;
			}

			// Store each Tile's data as a JSON object
			for (int i = 0; i < this._map.getNumberOfTiles(); i++) {
				tile_json[i] = new Tile_JSON();
				tile_json[i].tileID = i;
				tile_json[i].gangStrength = this._map.getGangStrength (this._map.getTileByID(i));
				tile_json[i].college = this._map.getTileByID (i).getCollege ();
				tile_json[i].positionInArray = i;
				tile_json[i].x = this._map.getTileByID(i).x;
				tile_json[i].y = this._map.getTileByID(i).y;
			}

			// Store Map data as a JSON object
			map_json.numberOfTiles = tile_json.Length;
			map_json.tiles = tile_json;
			map_json.width = mapWidth;
			map_json.height = mapHeight;

			// Store game state related values as JSON
			game_state_json.numberOfPlayers = players_json.Length;
			game_state_json.map = map_json;
			game_state_json.players = players_json;
			game_state_json.currentTurn = this._currentTurn;
			game_state_json.currentPlayer = this._currentPlayer;

			// Stringify JSON
			string save_json = JsonUtility.ToJson(game_state_json);
			
			// Build the path to the save file
			string filePath = Path.Combine(Application.dataPath, this.savePath);

			// Write the JSON string to the save file
			File.WriteAllText(filePath, save_json); 


			// If we made it this far, saving was successful
			return true;
		}

		/// <summary>
		/// Ends the turn.
		/// </summary>
		/// <returns>The turn.</returns>
		void EndTurn(){
			
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
					int strength = tile.getGangStrength();
					tile.setGangStrength(0);
					lastClickedTile.setGangStrength(lastClickedTile.getGangStrength() + strength);
					lastClickedTile = null;
				}else{
					lastClickedTile = tile;
				}
			}
		}
    }

	public static class GameState {

	}
}
