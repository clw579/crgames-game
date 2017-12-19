using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRGames_game
{
    public class UIManager : MonoBehaviour
    {
		// Text Elements of UI
		public Text college, gangMembers, tileLevel, pvc;
        public Text playersCollege, playersGangMembers, name;
        public GameObject noTileWarning;
        public GameObject tileMenu, showButton;

        void Update()
		{
			
		}

        public void initialiseUI(string playersCollege, int noOfGangMembers, string name)
        {
    
            showButton.SetActive(false);
            this.RefreshCurrentPlayerInfo(playersCollege, noOfGangMembers, name);
        }

        /// <summary>
        /// Refreshes the tile menu.
        /// </summary>
        /// <param name="currentTile">Current tile.</param>
        /// <param name="tileCollege">College tile belongs to.</param>
        public void RefreshTileMenu(Tile currentTile, string tileCollege)
		{
			college.text = tileCollege;
			gangMembers.text = currentTile.getGangStrength().ToString();
		}

        public void RefreshCurrentPlayerInfo(string playersCollege, int noOfGangMembers, string name)
        {
            this.playersCollege.text = playersCollege;
            this.playersGangMembers.text = noOfGangMembers.ToString();
            this.name.text = name;
        }

        public void showTileInfo()
        {
            tileMenu.SetActive(true);
            showButton.SetActive(false);
           
            
        }

        public void showTileWarning()
        {
   
            noTileWarning.SetActive(true);
            StartCoroutine(FadeTextToZeroAlpha(2.5f,  noTileWarning.GetComponent<Text>() ));
        }
        

        public IEnumerator FadeTextToZeroAlpha(float t, Text i)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }
        }



    }
}