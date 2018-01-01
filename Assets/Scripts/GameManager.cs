using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

/*
    CLASS: GameManager
    FUNCTION: The game manager sets up the game, loads and saves game data, and handles transitions between turns.
				Clicking on a tile in game also reports the click to the game manager, which handles the click action.
				The game manager also sends attack events on to the combat engine.
 */

 /*
	ENUM: colleges
	FUNCTION: Keeps a list of all playable colleges.
  */

/*
	ENUM: MoveTypes
	FUNCTION: Keeps a list of all valid types of moves, used by the game manager to evaluate whether a move is valid and what action to carry out.
 */

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

	enum MoveTypes
	{
		Invalid,
		TakeOver,
		Attack,
		Move
	}

    public class GameManager : MonoBehaviour
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
        public GameObject gangMemberSprite;
     
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

		// The current turn number
		private int currentTurn;
		// The index of the current Player
		private int currentPlayer;

		// Array of Players in the game
        private List<Player> players1 = new List<Player>();     
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

            // setup placeholders players to test the functioning of the game
            setupTest();
            
            //sets the inital number of gang members for player1, from here after they are allocated by the nextTurn function
            players1[currentPlayer].setGangStrength(players1[currentPlayer].GetOwnedTiles().Count);
           
            // set intial UI elements for the first player
            uiManager.initialiseUI(collegeLookupTable[players1[currentPlayer].GetCollege()], players1[currentPlayer].GetNumberOfGangMembers(), players1[currentPlayer].GetName());
        }

        void Update()
        {
			// If a player has clicked on a tile, display information about that tile on the UI
			if (lastClickedTile != null) {
				uiManager.RefreshTileMenu (lastClickedTile, lookupCollege (lastClickedTile.getCollege ()));
			}
        }

		/// <summary>
		/// Returns the string value of a college corresponding to its enum value.
		/// </summary>
		/// <param name="college">College.</param>
		public string lookupCollege(int college) {
			return collegeLookupTable[college];
		}

		/// <summary>
		/// Gets the array of college colour.
		/// </summary>
		/// <returns>Array of college colours.</returns>
		public Color[] getCollegeColours() {
			return collegeColours;
		}

		/// <summary>
		/// Gets the last tile that was clicked on.
		/// </summary>
		/// <returns>The last tile that was clicked on.</returns>
		public Tile getLastClickedTile()
		{
			return lastClickedTile;
		}

		/// <summary>
		/// Generates the Map object.
		/// </summary>
		void GenerateMap(){
			map = new Map(24, 13, mapSprites, tilePrefab, gangMemberSprite);
		}

		/// <summary>
		/// Moves the game to the next turn.
		/// </summary>
		public void NextTurn(){
			// Reset the colours of the map
			map.resetColours(collegeColours);

			// Increment the current turn and current player
            currentTurn++;
            currentPlayer++;

			// Wrap back to the first player if currentPlayer is too large
            if (currentPlayer > players1.Count - 1) {
				currentPlayer = 0;
			}

			// Allocate more gang members to the next player
            players1[currentPlayer].allocateGangMembers();
			// Alert the next player that it's their turn, AI players will then calculate their turn
            players1[currentPlayer].AlertItsMyTurn ();

			// Reset the lastClickedTile variable
			lastClickedTile = null;

			// Update the UI
            uiManager.RefreshCurrentPlayerInfo(collegeLookupTable[players1[currentPlayer].GetCollege()], players1[currentPlayer].GetNumberOfGangMembers(), players1[currentPlayer].GetName());       
        }


	
		/// <summary>
		/// Works out what to do when a tile has been clicked on (e.g. move, attack).
		/// </summary>
		/// <param name="tile">The tile that was clicked on.</param>
		public void TileClicked(Tile tile)
		{
			// Show information relating to the tile that was clicked on
            uiManager.showTileInfo();

			// If a tile has been clicked on previously, move or attack, otherwise pick the tile that was clicked on
			if (lastClickedTile != null) {
				// Evaluate the type of move that we will be making
				int move = EvaluateMove(lastClickedTile, tile);

				switch (move)
				{
					// If the tile does not contain any gang members, take over
					case (int)MoveTypes.TakeOver:
						// Move a gang member
						MoveGangMember(lastClickedTile, tile);

						// Set the college of the tile
						tile.setCollege(lastClickedTile.getCollege());

						// Reset the tile that was last clicked on
						lastClickedTile = null;

						// Tell the tile to update its colour
						tile.resetColor(collegeColours);
						break;

					// If the tile is owned by the player that clicked it, move a gang member to that tile
					case (int)MoveTypes.Move:
						// Move a gang member
						MoveGangMember(lastClickedTile, tile);

						// Reset the tile that was last clicked on
						lastClickedTile = null;

						// Update the tile's colour
						tile.resetColor(collegeColours);
						break;

					// If the move was invalid, reset the tile that was last clicked on
					default:
						lastClickedTile = null;
						break;
				}
			}else if (tile.getGangStrength() > 0 && (tile.getCollege() == (int)colleges.Unknown || tile.getCollege() == players1[currentPlayer].GetCollege())) {
				// Get the tiles adjacent to this one
				Tile[] adjacents = map.getAdjacent(tile);

				// Highlight all of the adjacent tiles red
				for (int i = 0; i < 4; i++) {
					if (adjacents [i] != null) {
						if (adjacents[i].getCollege() != tile.getCollege ()) {
							adjacents [i].setColor (Color.red);
						}
					}
				}

				// Set the lastClickedTile to the tile that was clicked on
				lastClickedTile = tile;
			}
		}

		/// <summary>
		/// Moves a gang member from one tile to another.
		/// </summary>
		/// <param name="from">The tile to move from.</param>
		/// <param name="to">The tile to move to.</param>
		void MoveGangMember(Tile from, Tile to){
			to.setGangStrength(to.getGangStrength() + 1);
			from.setGangStrength(from.getGangStrength() - 1);
		}


        public void ReinforceTile(String noOfGangMembers)
        {
            if (getLastClickedTile() == null)
            {   
                // Checks a tile has been clicked on else, show the user a warning
                uiManager.showTileWarning();
            }else{

                // Variables holding the previous gangmember strengths
                int previousTileStrength = getLastClickedTile().getGangStrength();
                int previousPlayersGangMembers = players1[currentPlayer].GetNumberOfGangMembers();

                // Try and parse the input and place result in j, if the input is not a valid integer then nothing will happen
                int j;
                if (Int32.TryParse(noOfGangMembers, out j))
                {
                    // Checks the player has the right amount of gangmembers
                    if (previousPlayersGangMembers >= j)
                    {
                        getLastClickedTile().setGangStrength(j + previousTileStrength);
                        players1[currentPlayer].setGangStrength(previousPlayersGangMembers - j);
                    }
                }

            }

        }


		/// <summary>
		/// Attempts to attack a tile from the last clicked tile.
		/// </summary>
		/// <param name="tile">Tile to attack.</param>
		public void requestAttack(Tile tile)
		{
			// If two tiles have been clicked on in total, and they belong to two different colleges
			if (lastClickedTile != null && lastClickedTile.getCollege() != tile.getCollege()) {
				// If the two tiles that have been clicked on are adjacent to each other
				if (map.isAdjacent(lastClickedTile, tile)) {
					// Create an array to store the new gang strenghts of each tile
					int[] newStrengths = new int[2];
					// Calculate the new strengths by evaluating the attack in the combat engine
					newStrengths = combatEngine.Attack(lastClickedTile.getGangStrength(), tile.getGangStrength());
					
					// Set the new strengths
					lastClickedTile.setGangStrength(newStrengths[0]);
					tile.setGangStrength(newStrengths[1]);

					// Get the tiles adjacent to the previously selected tile
					Tile[] adjacents = map.getAdjacent (lastClickedTile);
			
					// Stop highlighting targets from the previously clicked on tile
					for (int i = 0; i < 4; i++) {
						if (adjacents[i] != null) {
							adjacents[i].resetColor(collegeColours);
						}
					}

					// Reset the tile that was last clicked on
					lastClickedTile = null;
				}
			}
		}

		/// <summary>
		/// Evaluate whether a move is valid, and what type of move it is.
		/// <summary>
		/// <param name="from">The tile the action is being performed from.</param>
		/// <param name="to">The tile the action is being perfromed to.</param>
		/// <returns>A move type from the MoveTypes enum</returns>
		int EvaluateMove(Tile from, Tile to){
			// Get the adjacent tiles
			Tile[] adjacents = map.getAdjacent (from);
			
			// Stop highlighting targets from the previously clicked on tile
			for (int i = 0; i < 4; i++) {
				if (adjacents[i] != null) {
					adjacents[i].resetColor(collegeColours);
				}
			}

			// Check if the two tiles are adjactent to each other if not then the move is not valid
			if (adjacents.Contains(to)){
				// If there are no gang members on a tile, this is a takeover
				if (to.getGangStrength() == 0){
					return (int)MoveTypes.TakeOver;
				}else if(from.getCollege() == to.getCollege()){ // If the two tiles are owned by the same college, we are moving a gang member
					return (int)MoveTypes.Move;
				}else{ // Otherwise, the move is invalid
					return (int)MoveTypes.Invalid;
				}
			}else{
				return (int)MoveTypes.Invalid;
			}
		}

		// Sets up some test players
        public void setupTest()
        {
			// Set the inital player to player 1
            currentPlayer = 0;
			// Set the inital turn to 1
            currentTurn = 1;

			// Create a player called Sally and give them some tiles
            players1.Add(new Player(1, "Sally"));
            players1[0].AddOwnedTiles(map.getTileAtPosition(0, 0));
            players1[0].AddOwnedTiles(map.getTileAtPosition(1, 0));
            map.getTileAtPosition(0, 0).setCollege(1);
            map.getTileAtPosition(1, 0).setCollege(1);


            // Set player 1 gang members
            map.getTileAtPosition(0, 0).setGangStrength(2);
            map.getTileAtPosition(1, 0).setGangStrength(2);

			// Create a player called Bob and give them some tiles
            players1.Add(new Player(2, "Bob"));
            players1[1].AddOwnedTiles(map.getTileAtPosition(2, 0));
            players1[1].AddOwnedTiles(map.getTileAtPosition(2, 1));
            map.getTileAtPosition(2, 0).setCollege(2);
            map.getTileAtPosition(2, 1).setCollege(2);

            // Set player2 gangmembers
            map.getTileAtPosition(2, 0).setGangStrength(2);
            map.getTileAtPosition(2, 1).setGangStrength(2);
        }

      
    }
}
