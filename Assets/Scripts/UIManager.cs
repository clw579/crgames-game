﻿using System.Collections;
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

		void Update()
		{
			
		}

        public void initialiseUI(string playersCollege, int noOfGangMembers, string name)
        {
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
    }
}