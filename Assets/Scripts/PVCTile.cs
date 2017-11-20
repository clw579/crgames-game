using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRGames_game
{
    class PVCTile : Tile
    {
        public PVCTile(int id) : base(id)
        {

        }
        public bool startMiniGame()
        {
            return false;
        }
    }
}
