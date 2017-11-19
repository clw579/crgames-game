using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONObjects {

}

/// <summary>
/// Game state JSON Representation.
/// </summary>
public class GameState_JSON {
	public Map_JSON map = new Map_JSON ();
	public Player_JSON[] players;
	public int numberOfPlayers;
}

/// <summary>
/// Player JSON Representation.
/// </summary>
public class Player_JSON {
	public int college;
	public string name;
}

/// <summary>
/// Map JSON Representation.
/// </summary>
public class Map_JSON {
	public Tile_JSON[] tiles;
	public int size;
}

/// <summary>
/// Tile JSON Representation.
/// </summary>
public class Tile_JSON {
	public int tileID;
	public int gangStrength;
	public int college;
}