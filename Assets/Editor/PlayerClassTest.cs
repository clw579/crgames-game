using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace CRGames_game
{
    public class PlayerClassTest
    {


        [Test]
        public void TestPlayersDetails()
        {
             //Arrange
            Player player1 = new Player(0, "A");
       

            //Assert
            Assert.AreEqual(0, player1.GetCollege());
            Assert.AreEqual("A", player1.GetName());
        } 
        
        [Test]
        public void TestAddTiles()
        {
            //Arrange
            Player player1 = new Player(0, "A");

            List<Tile> TilesToAdd = new List<Tile>();
            TilesToAdd.Add (new Tile(1, new GameObject()));
            TilesToAdd.Add(new Tile(2, new GameObject()));

            for(int x = 0; x < TilesToAdd.Count; x++)
            {
                player1.AddOwnedTiles(TilesToAdd[x]);
            }
            //Action
            int noOfTiles = player1.GetOwnedTiles().Count;

            //Assert
            Assert.AreEqual(2, noOfTiles);

        }

        [Test]

        public void TestAddGangMembers()
        {

            //Arrange
            Player player1  = new Player(0, "A");
            player1.allocateGangMembers();
            player1.AddOwnedTiles(new Tile(1, new GameObject()));
            player1.AddOwnedTiles(new Tile(2, new GameObject()));

            //Assert
            Assert.AreEqual(2, player1.allocateGangMembers());

        }




    }
}