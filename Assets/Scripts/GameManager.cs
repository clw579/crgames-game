using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

namespace SEPRTest1
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
		private String savePath = "gamestates.json";

		private int _currentTurn;
		private int _currentPlayer;

		private Player[] _players;
		private Map _map;
        
		void Start()
        {
			GenerateMap ();

			this._players = new Player[1];
			this._players[0] = new Player((int)colleges.Goodricke, "Tom");
			
			SaveGame();
        }

		void GenerateMap(){
			Map map = new Map(128);

			// TODO map-ish stuff

			this._map = map;
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
			return false;
		}

		/// <summary>
		/// Saves the game.
		/// </summary>
		/// <returns>Success of saving the game.</returns>
		bool SaveGame(){
			GameState_JSON game_state_json = new GameState_JSON ();
			Player_JSON[] players_json = new Player_JSON[this._players.Length];
			Map_JSON map_json = new Map_JSON ();
			Tile_JSON[] tile_json = new Tile_JSON[this._map.getNumberOfTiles()];

			for (int i = 0; i < this._players.Length; i++) {
				players_json[i] = new Player_JSON();
				players_json [i].college = this._players [i].GetCollege ();
				players_json [i].name = this._players [i].GetName ();
				players_json[i].positionInArray = i;
			}

			for (int i = 0; i < this._map.getNumberOfTiles(); i++) {
				tile_json[i] = new Tile_JSON();
				tile_json[i].tileID = i;
				tile_json[i].gangStrength = this._map.getGangStrength (this._map.getTileByID(i));
				tile_json[i].college = this._map.getTileByID (i).getCollege ();
				tile_json[i].positionInArray = i;
			}

			map_json.numberOfTiles = tile_json.Length;
			map_json.tiles = tile_json;

			game_state_json.numberOfPlayers = players_json.Length;
			game_state_json.map = map_json;
			game_state_json.players = players_json;

			string save_json = JsonUtility.ToJson(game_state_json);

			string filePath = Path.Combine(Application.dataPath, this.savePath);
			
			File.WriteAllText(filePath, save_json); 

			return false;
		}

		/// <summary>
		/// Ends the turn.
		/// </summary>
		/// <returns>The turn.</returns>
		void EndTurn(){

		}
    }

	public static class GameState {

	}
}
