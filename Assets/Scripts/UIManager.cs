using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    CLASS: UIManager
    FUNCTION: Manages displaying and updating the UI
 */

namespace CRGames_game
{
    public class UIManager : MonoBehaviour
    {
		// Text Elements of UI
		public Text college, gangMembers, tileLevel, pvc;
        public Text playersCollege, playersGangMembers, playersName;
        public GameObject noTileWarning;
        public GameObject tileMenu, showButton;

        void Update()
		{
			
		}

        /// <summary>
        /// Initialises the UI.
        /// </summary>
        /// <param name="playersCollege">The name of the college to display.</param>
        /// <param name="noOfGangMembers">The number of gang members to display.</param>
        /// <param name="name">The name of the player to display.</param>
        public void initialiseUI(string playersCollege, int noOfGangMembers, string name)
        {
            // Deactivate the show button
            showButton.SetActive(false);
            // Update the player's info that is displayed
            this.RefreshCurrentPlayerInfo(playersCollege, noOfGangMembers, name);
        }

        /// <summary>
        /// Refreshes the tile menu.
        /// </summary>
        /// <param name="currentTile">Current tile.</param>
        /// <param name="tileCollege">College tile belongs to.</param>
        public void RefreshTileMenu(Tile currentTile, string tileCollege)
		{
            if (currentTile != null)
            {
                college.text = tileCollege;
                gangMembers.text = currentTile.getGangStrength().ToString();
            }
            else
            {
                college.text = "";
                gangMembers.text = "";
            }
		}

        /// <summary>
        /// Refreshes the player info being displayed.
        /// </summary>
        /// <param name="playersCollege">The college of the player.</param>
        /// <param name="noOfGangMembers">The number of gang members that the player owns.</param>
        /// <param name="playersName">The name of the player.</param>
        public void RefreshCurrentPlayerInfo(string playersCollege, int noOfGangMembers, string playersName)
        {
            this.playersCollege.text = playersCollege;
            this.playersGangMembers.text = noOfGangMembers.ToString();
            this.playersName.text = playersName;
        }

        /// <summary>
        /// Shows the info of the last tile that was clicked on.
        /// </summary>
        public void showTileInfo()
        {
            tileMenu.SetActive(true);
            showButton.SetActive(false);
        }

        /// <summary>
        /// Show the warning tile.
        /// </summary>
        public void showTileWarning()
        {
   
            noTileWarning.SetActive(true);
            StartCoroutine(FadeTextToZeroAlpha(2.5f,  noTileWarning.GetComponent<Text>() ));
        }
        
        /// <summary>
        /// Fade text out over time.
        /// </summary>
        /// <param name="t">The time to fade over.</param>
        /// <param name="i">The text to fade.</param>
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