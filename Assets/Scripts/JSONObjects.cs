using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	CLASS: GameStateJSON
	FUNCTION: Represents GameManager in a JSON-friendly format
 */

/*
	CLASS: PlayerJSON
	FUNCTION: Represents Player in a JSON-friendly format
 */

/*
	CLASS: MapJSON
	FUNCTION: Represents Map in a JSON-friendly format
 */

/*
	CLASS: TileJSON
	FUNCTION: Represents Tile in a JSON-friendly format
 */

/*
	CLASS: ColourJSON
	FUNCTION: Represents college colours in a JSON-friendly format
 */

/*
	CLASS: CombatEngineJSON
	FUNCTION: Represents CombatEngine in a JSON-friendly format
 */

/// <summary>
/// Game state JSON Representation.
/// </summary>
[System.Serializable]
public class GameStateJSON {
	public MapJSON map = new MapJSON ();
	public PlayerJSON[] players;
	public ColourJSON[] collegeColours;
	public CombatEngineJSON combatEngine;
	public int numberOfPlayers;
	public int currentTurn;
	public int currentPlayer;
}

/// <summary>
/// Player JSON Representation.
/// </summary>
[System.Serializable]
public class PlayerJSON {
	public int college;
	public string name;
	public int positionInArray;
}

/// <summary>
/// Map JSON Representation.
/// </summary>
[System.Serializable]
public class MapJSON {
	public TileJSON[] tiles;
	public int size;
	public int numberOfTiles;
	public int width;
	public int height;
}

/// <summary>
/// Tile JSON Representation.
/// </summary>
[System.Serializable]
public class TileJSON {
	public int x;
	public int y;
	public int tileID;
	public int gangStrength;
	public int college;
	public int positionInArray;
}

/// <summary>
/// Colour JSON Representation.
/// </summary>
[System.Serializable]
public class ColourJSON {
	public float r;
	public float g;
	public float b;
	public float a;
}

/// <summary>
/// Combat Engine Representation.
/// </summary>
[System.Serializable]
public class CombatEngineJSON {
	public double pvcBonus;
	public double hiddenDamageModifier;
}