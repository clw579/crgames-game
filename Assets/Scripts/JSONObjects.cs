using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONObjects {

}

/// <summary>
/// Game state JSON Representation.
/// </summary>
[System.Serializable]
public class GameStateJSON {
	public MapJSON map = new MapJSON ();
	public PlayerJSON[] players;
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