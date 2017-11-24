using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRGames_game
{
    class Player
    {
        private int college;
        private String name;
        private ArrayList ownedTiles = new ArrayList();

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

        public string GetName()
        {
            return name;
        }

        public ArrayList GetOwnedTiles()
        {
            return this.ownedTiles;
        }
    }


   

}
