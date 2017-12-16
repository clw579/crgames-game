using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace CRGames_game
{
    public class Player
    {
        private int college;
        private String name;
        private List<Tile> ownedTiles;
        private int noOfGangMembers;
       

		public Player(int college){

		}

        public Player(int college, String name)
        {
            this.college = college;
            this.name = name;
            this.ownedTiles = new List<Tile>() ;
            noOfGangMembers = 0;
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

        public List<Tile> GetOwnedTiles()
        {
            return ownedTiles;
        }

        public void AddOwnedTiles(Tile tile)
        {
            ownedTiles.Add(tile);
        }

        public int GetNumberOfGangMembers()
        {
            return noOfGangMembers;
        }
        
        public int allocateGangMembers()
        {
            this.noOfGangMembers += ownedTiles.Count;
            return noOfGangMembers;
             

        }


    }


   

}
