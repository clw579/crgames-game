using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRGames_game
{
    class PVCTile : Tile
    {
		public PVCTile(int id, GameObject gob) : base(id, gob)
        {

        }
        public bool startMiniGame()
        {
            return false;
        }
    }
}
