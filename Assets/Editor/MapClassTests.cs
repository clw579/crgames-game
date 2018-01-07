using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace CRGames_game {
	
	public class MapClassTests {

		[Test]
		public void TestGetTileById() {
			//Assign
			// create a new test map and 2 new test tiles
			Map testMap = new Map(2, 2);
			Tile testTile = new Tile(0, new GameObject());
			Tile testTile2 = new Tile(1, new GameObject());
			// add the tiles to the map
			testMap.addTile(testTile);
			testMap.addTile(testTile2);

			//Assert
			Assert.AreEqual(testTile, testMap.getTileByID(0));
			Assert.AreEqual(testTile2, testMap.getTileByID(1));
		}

		[Test]
		public void TestGetTileAtPosition() {
			//Assign
			// create a new test map and 2 new test tiles
			Map testMap = new Map(2, 2);
			Tile testTile = new Tile(0, new GameObject());
			Tile testTile2 = new Tile(1, new GameObject());
			// add the tiles to the map
			testMap.addTile(testTile);
			testMap.addTile(testTile2);

			//Assert
			Assert.AreEqual(testTile, testMap.getTileAtPosition(0, 0));
			Assert.AreEqual(testTile2, testMap.getTileAtPosition(1, 0));
		}

		[Test]
		public void TestGetTileId() {
			//Assign
			// create a new test map and 2 new test tiles
			Map testMap = new Map(2, 2);
			Tile testTile = new Tile(0, new GameObject());
			Tile testTile2 = new Tile(1, new GameObject());
			// add the tiles to the map
			testMap.addTile(testTile);
			testMap.addTile(testTile2);

			//Assert
			Assert.AreEqual(0, testMap.getTileId(testTile));
			Assert.AreEqual(1, testMap.getTileId(testTile2));
		}

		[Test]
		public void TestGetGangStrength() {
			//Assign
			// create a new test map and 2 new test tiles
			Map testMap = new Map(2, 2);
			Tile testTile = new Tile(0, new GameObject());
			Tile testTile2 = new Tile(1, new GameObject());
			// set a realistic and an extreme value for tile 1 and 2 respectively
			testTile.setGangStrength(3);
			testTile2.setGangStrength(100);
			// add the tiles to the map
			testMap.addTile(testTile);
			testMap.addTile(testTile2);

			//Assert
			Assert.AreEqual(3, testMap.getGangStrength(testTile));
			Assert.AreEqual(100, testMap.getGangStrength(testTile2));
		}

		[Test]
		public void TestMoveGangMember() {
			//Assign
			// create a new test map and 2 new test tiles
			Map testMap = new Map(2, 2);
			Tile testTile = new Tile(0, new GameObject());
			Tile testTile2 = new Tile(1, new GameObject());
			// set a realistic and an extreme value for testTile and testTile2 respectively
			testTile.setGangStrength(3);
			testTile2.setGangStrength(100);
			// add the tiles to the map
			testMap.addTile(testTile);
			testMap.addTile(testTile2);

			//Action
			// move the gang members from testTile to testTile2
			testMap.moveGangMember(testTile, testTile2);

			//Assert
			Assert.AreEqual(0, testMap.getGangStrength(testTile));
			Assert.AreEqual(103, testMap.getGangStrength(testTile2));
		}

		[Test]
		public void TestResetColours() {
			//Assign
			// get gameManager from the camera
			GameManager manager = GameObject.FindWithTag("MainCamera").GetComponent<GameManager>();
			// get the correct college colours from the game manager
            Color[] collegeColours = new Color[6];
            collegeColours[0] = Color.blue;
            collegeColours[1] = Color.red;

            // create a new test map and 2 new test tiles
            Map testMap = new Map(1, 2);
			Tile testTile = new Tile(0, new GameObject());
			Tile testTile2 = new Tile(1, new GameObject());

            GameObject testTileObject = new GameObject();
            GameObject testTileObject2 = new GameObject();

            testTileObject.AddComponent<SpriteRenderer>();
            testTileObject2.AddComponent<SpriteRenderer>();

            testTile.SetObject(testTileObject);
            testTile2.SetObject(testTileObject2);
       
            // add the tiles to the map
            testMap.addTile(testTile);
			testMap.addTile(testTile2);

			//Action
			// set the colleges for the test tiles
			testTile.setCollege(0);
			testTile2.setCollege(1);
			// change the colours on the test tiles
			testTile.setColor(Color.yellow);
			testTile2.setColor(Color.green);
			// reset the colours on the tiles in the map to the college colours defined in GameManager
			testMap.resetColours(collegeColours);

			Assert.AreEqual(collegeColours[0], testTile.getColor());
			Assert.AreEqual(collegeColours[1], testTile2.getColor());
		}

		[Test]
		public void TestGetAdjacent() {
			//Assign
			// create a new test map and fill it with tiles
			Map testMap = new Map(3, 3);
			Tile[] testTile = new Tile[9];
			for (int x = 0; x < 3; x++) {
				for (int y = 0; y < 3; y++) {
					testTile[x + (y * 3)] = new Tile(x + (y * 3), new GameObject());
					testTile[x + (y * 3)].x = x;
					testTile[x + (y * 3)].y = y;
					testMap.addTile(testTile[x + (y * 3)]);
				}
			}

			//Assert
			// assert that tiles 3, 1, 5 and 7 are adjacent to tile 4
			Assert.AreEqual(new Tile[] {testTile[3], testTile[1], testTile[5], testTile[7]}, testMap.getAdjacent(testTile[4]));
			// assert that a corner tile (tile 0) has 2 adjacents: tiles 1 and 3
			Assert.AreEqual(new Tile[] {null, null, testTile[1], testTile[3]}, testMap.getAdjacent(testTile[0]));
		}

		[Test]
		public void TestIsAdjacent() {
			//Assign
			// create a new test map and fill it with tiles
			Map testMap = new Map(3, 3);
			Tile[] testTile = new Tile[9];
			for (int x = 0; x < 3; x++) {
				for (int y = 0; y < 3; y++) {
					testTile[x + (y * 3)] = new Tile(x + (y * 3), new GameObject());
					testTile[x + (y * 3)].x = x;
					testTile[x + (y * 3)].y = y;
					testMap.addTile(testTile[x + (y * 3)]);
				}
			}

			//Assert
			// assert that tile 1 is adjacent to tile 2
			Assert.AreEqual(true, testMap.isAdjacent(testTile[1], testTile[2]));
			// assert that tile 2 is not adjacent to tile 4, despite being next to each other in the array
			Assert.AreEqual(false, testMap.isAdjacent(testTile[2], testTile[3]));
		}
	}
}
