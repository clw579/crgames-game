using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRGames_game
{
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
			switch (tile.getCollege())
			{
				case (int)colleges.Alcuin:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourAlcuin;
					break;
				case (int)colleges.Goodricke:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourGoodricke;
					break;
				case (int)colleges.Langwith:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourLangwith;
					break;
				case (int)colleges.Constantine:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourConstantine;
					break;
				case (int)colleges.Halifax:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourHalifax;
					break;
				case (int)colleges.Vanbrugh:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourVanbrugh;
					break;
				case (int)colleges.Derwent:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourDerwent;
					break;
				case (int)colleges.James:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourJames;
					break;
				case (int)colleges.Wentworth:
					gameObject.GetComponent<SpriteRenderer>().color = manager.ColourWentworth;
					break;
				default:
					gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
					break;
			}
		}
		
		/// <summary>
		/// When clicked, report the click to the GameManager
		/// </summary>
		void OnMouseDown(){
			Debug.Log(gameObject.name);
			manager.TileClicked(tile);
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
}
