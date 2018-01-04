using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace CRGames_game {

    public class TileClassTests {

        [Test]
        public void TestGetTileId() {

            Tile testTile = new Tile(0, new GameObject());
            testTile.setTileID(0);

            Assert.AreEqual(testTile.getID(), 0);
    
    }
        [Test]
        public void TestGetGangStrength()
        {

            Tile testTile = new Tile(0, new GameObject());
            testTile.setGangStrength(5);

            Assert.AreEqual(testTile.getGangStrength(), 5);

        }




        [Test]
        public void TestsetCollege()
        {

            Tile testTile = new Tile(0, new GameObject());
            testTile.setCollege(0);

            Assert.AreEqual(testTile.getCollege(), 0);

        }

        [Test]
        public void TestReset()
        {

            Tile testTile = new Tile(0, new GameObject());
            testTile.setCollege(1);
            testTile.setGangStrength(5);
            testTile.reset();

            Assert.AreEqual(testTile.getCollege(), 0);
            Assert.AreEqual(testTile.getGangStrength(), 0);

        }




        [Test]
        public void TestSetColour()
        {

            GameObject testGameObject = new GameObject();
            testGameObject.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = testGameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.blue;

            Tile testTile = new Tile(0, testGameObject);
            testTile.setColor(Color.red);

            SpriteRenderer newSpriteRenderer = testTile.GetObject().GetComponent<SpriteRenderer>();

            Assert.AreEqual(newSpriteRenderer.color, Color.red);

        }


        [Test]
        public void TestResetColour()
        {

            GameObject testGameObject = new GameObject();
            testGameObject.AddComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer = testGameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.blue;

            Tile testTile = new Tile(0, testGameObject);
            testTile.setCollege(0);
            Color[] collegeColours = new Color[1];
            collegeColours[0] = Color.blue;
            testTile.resetColor(collegeColours);

            SpriteRenderer newSpriteRenderer = testTile.GetObject().GetComponent<SpriteRenderer>();

            Assert.AreEqual(newSpriteRenderer.color, Color.blue);



        }




    }
}