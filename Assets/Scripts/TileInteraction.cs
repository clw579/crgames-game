using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SEPRTest1;
using CRGames_game;

public class TileInteraction : MonoBehaviour {

	// The Tile associated with this object
	public Tile tile;

	// The GameManager
	GameManager manager;
	
	void Start(){
		// Find the GameManager
		manager = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// When clicked, report the click to the GameManager
	/// </summary>
	void OnMouseDown(){
		Debug.Log(gameObject.name);
		manager.TileClicked(this.tile);
	}

	/// <summary>
	/// Highlight the tile when moused over
	/// </summary>
	void OnMouseEnter() {
        gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
    
	/// <summary>
	/// Unhighlight when the mouse moves out
	/// </summary>
    void OnMouseExit() {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
