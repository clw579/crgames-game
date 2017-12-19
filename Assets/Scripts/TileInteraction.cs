using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CRGames_game
{
	public class TileInteraction : MonoBehaviour {

		// The Tile associated with this object
		public Tile tile;

		// The GameManager
		GameManager manager;
		// The renderer attached to this tile
		SpriteRenderer myRenderer;
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

			myRenderer = gameObject.GetComponent<SpriteRenderer>();

			tile.resetColor(manager.getCollegeColours());

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
			if (tile.getGangStrength() > 0){
				textObject.SetActive(true);
				textObject.GetComponent<TextMesh>().text = tile.getGangStrength().ToString();

				if (!memberShown){
					CreateGangMember();

					memberShown = true;
				}
			}

			if (tile.getGangStrength() == 0){
				textObject.SetActive(false);

				memberShown = false;
				Destroy(myGangMember);
			}
		}

		void CreateGangMember(){

            // Instantiate a copy of the gang member sprite
            GameObject member = Instantiate<GameObject>(gangMemberSprite) as GameObject;

            // assgin the spriteRender and animtor components
            SpriteRenderer rend = member.GetComponent<SpriteRenderer>() as SpriteRenderer;
            Animator anim = member.GetComponent<Animator>() as Animator;

			// Set gang member's parent to the tile
			member.transform.parent = transform;

            // move the poisiton on the sprites
            member.transform.localPosition = new Vector3(0.0f, -0.1f, -5.0f);
            
            // play the gangMembers animation
            anim.Play("gooseAnimation");

            //update the gangmembers
            myGangMember = member;
		}

        /// <summary>
        /// When clicked, report the click to the GameManager
        /// </summary>
        void OnMouseDown()
        {
            
        }
	    
		/// <summary>
		/// Unhighlight when the mouse moves out
		/// </summary>
	    void OnMouseExit() {
	        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
	    }

		void OnMouseOver()
		{
			gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			gameObject.transform.GetChild(0).gameObject.SetActive(true);

			if (!EventSystem.current.IsPointerOverGameObject())
            {
				if(Input.GetMouseButtonDown(1)){
					manager.requestAttack(tile);
				}else if (Input.GetMouseButtonDown(0)){
					manager.TileClicked(tile);
				}
            
            }
		}

		public void SetGangMemberSprite(GameObject gangMemberSprite){
	
            this.gangMemberSprite = gangMemberSprite;
		}
	}
}
