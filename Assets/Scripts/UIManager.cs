using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRGames_game
{
    public class UIManager : MonoBehaviour
    {
		
		public Text college, gangMembers, tileLevel, pvc;

		void Update()
		{
			
		}

		public void RefreshTileMenu(Tile currentTile, string tileCollege)
		{
			college.text = tileCollege;
			gangMembers.text = currentTile.getGangStrength().ToString();
		}
    }
}