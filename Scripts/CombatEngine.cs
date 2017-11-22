using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPRTest1
{
    class CombatEngine
    {
        Player player1;
        Player player2;

        public CombatEngine(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
        }

        public Player resolveCombat()
        {
            return player1;
        }
    }
}
