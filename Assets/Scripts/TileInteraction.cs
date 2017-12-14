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
		Sprite gangMemberSprite;
		// Array of gang members displayed on tile
		List<GameObject> myGangMembers = new List<GameObject>();

		void Start(){
			// Find the GameManager
			manager = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();

			myRenderer = gameObject.GetComponent<SpriteRenderer>();

			tile.resetColor(manager.getCollegeColours());
		}

		// Update is called once per frame
		void Update () {
			tile.resetColor(manager.getCollegeColours());

			if (tile.getGangStrength() > myGangMembers.Count){
				CreateGangMember();
			}

			if (tile.getGangStrength() < myGangMembers.Count){
				for (int i = 0; i < myGangMembers.Count - tile.getGangStrength(); i++){
					Destroy(myGangMembers[0]);
					myGangMembers.RemoveAt(0);
				}
			}
		}

		void CreateGangMember(){
			GameObject member = new GameObject();
			SpriteRenderer rend = member.AddComponent<SpriteRenderer>() as SpriteRenderer;
			rend.sprite = gangMemberSprite;

			member.transform.position = new Vector3(UnityEngine.Random.Range(transform.position.x - 0.25f, transform.position.x + 0.25f), UnityEngine.Random.Range(transform.position.y - 0.25f, transform.position.y + 0.25f), -5.0f);

			member.transform.parent = transform;

			myGangMembers.Add(member);
		}

        /// <summary>
        /// When clicked, report the click to the GameManager
        /// </summary>
        void OnMouseDown()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                manager.TileClicked(tile);
            }
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

			if (Input.GetMouseButtonDown(1)) 
				{
					manager.requestAttack(tile);
				}
		}

		public void SetGangMemberSprite(Sprite member){
			this.gangMemberSprite = member;
		}
	}
}
