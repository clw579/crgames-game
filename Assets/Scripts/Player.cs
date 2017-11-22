using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRGames_game
{
    class Player
    {
        int college;
        String name;

		public Player(int college){

		}

        public Player(int college, String name)
        {
            this.college = college;
            this.name = name;
        }

		public void AlertItsMyTurn(){

		}

		public int GetCollege (){
			return college;
		}

		public string GetName(){
			return name;
		}
    }


   

}
