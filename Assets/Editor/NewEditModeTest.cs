using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


namespace CRGames_game
{
    public class NewEditModeTest
    {

        public  GameObject mapSprites, tilePrefab;

        [Test]
        public void NewEditModeTestSimplePasses()
        {
            // Use the Assert class to test conditions.
             map = new Map(24, 13, mapSprites, tilePrefab);

        }

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityTest]
        public IEnumerator NewEditModeTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}