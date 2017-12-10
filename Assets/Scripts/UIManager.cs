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

		void Update()
		{
			
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
    }
}