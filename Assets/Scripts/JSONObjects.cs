using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONObjects {

}

/// <summary>
/// Game state JSON Representation.
/// </summary>
[System.Serializable]
public class GameState_JSON {
	public Map_JSON map = new Map_JSON ();
	public Player_JSON[] players;
	public int numberOfPlayers;
}

/// <summary>
/// Player JSON Representation.
/// </summary>
[System.Serializable]
public class Player_JSON {
	public int college;
	public string name;
	public int positionInArray;
}

/// <summary>
/// Map JSON Representation.
/// </summary>
[System.Serializable]
public class Map_JSON {
	public Tile_JSON[] tiles;
	public int size;
	public int numberOfTiles;
	public int width;
	public int height;
}

/// <summary>
/// Tile JSON Representation.
/// </summary>
[System.Serializable]
public class Tile_JSON {
	public int tileID;
	public int gangStrength;
	public int college;
	public int positionInArray;
}