using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
	CLASS: TileInteraction
	FUNCTION: Handles players interacting with a tile (mouse over, clicking)
 */

namespace CRGames_game
{
	public class TileInteraction : MonoBehaviour {

		// The Tile associated with this object
		public Tile tile;

		// The GameManager
		GameManager manager;
		// The gang member sprite
        GameObject gangMemberSprite;
		// Gang member displayed on tile
		GameObject myGangMember;
		// Whether the gang member has been shown
		bool memberShown = false;
		// The tile's text object
		GameObject textObject;

		void Start(){
			// Find the GameManager
			manager = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();

    
			// Reset the colour of this tile
			tile.resetColor(manager.getCollegeColours());

			// Find the label of this tile
			foreach (Transform child in transform)
			{
				if (child.gameObject.tag == "Label"){
					child.gameObject.SetActive(false);
					
					textObject = child.gameObject;
				}
			}
		}

		// Update is called once per frame
		void Update () {
			// If the tile has at least one gang member, set the counter on the tile to the number of gang members
			if (tile.getGangStrength() > 0){
				// Show the counter label
				textObject.SetActive(true);
				// Set the counter label to the gang strength
				textObject.GetComponent<TextMesh>().text = tile.getGangStrength().ToString();

				// If a gang member has not been shown on the tile, show one
				if (!memberShown){
					CreateGangMember();

					memberShown = true;
				}
			}

			// If there are no gang members on the tile, hide the label and the gang member sprite
			if (tile.getGangStrength() == 0){
				textObject.SetActive(false);

				memberShown = false;
				Destroy(myGangMember);
			}
		}

		/// <summary>
		/// Creates a gang member on the tile.
		/// </summary>
		void CreateGangMember(){

            // Instantiate a copy of the gang member sprite
            GameObject member = Instantiate<GameObject>(gangMemberSprite) as GameObject;

			// Set gang member's parent to the tile
			member.transform.parent = transform;

            // Move the poisiton on the sprites
            member.transform.localPosition = new Vector3(0.0f, -0.1f, -5.0f);
    
            // Update the gangmembers
            myGangMember = member;
		}
	    
		/// <summary>
		/// Unhighlight when the mouse moves out.
		/// </summary>
	    void OnMouseExit() {
	        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
	    }

		/// <summary>
		/// Highlight the tile when moused over and detect click events.
		/// </summary>
		void OnMouseOver()
		{
			// Increase the tile size
			gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			// Activate the tile highlighter
			gameObject.transform.GetChild(0).gameObject.SetActive(true);

			// Detect left and right click over tile
			if (!EventSystem.current.IsPointerOverGameObject())
            {
				if(Input.GetMouseButtonDown(1)){
					manager.requestAttack(tile);
				}else if (Input.GetMouseButtonDown(0)){
					manager.TileClicked(tile);
				}
            
            }
		}

		/// <summary>
		/// Sets the sprite to use to represent gang members.
		/// </summary>
		/// <param name="gangMemberSprite">The sprite to use for gang members.</param>
		public void SetGangMemberSprite(GameObject gangMemberSprite){
	
            this.gangMemberSprite = gangMemberSprite;
		}
	}
}
