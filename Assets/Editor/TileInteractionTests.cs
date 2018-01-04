using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace CRGames_game{

    public class TileInteractionTests {


        [UnityTest]
        public IEnumerator SpawnGangMemberTest() {

            // create a new gameobject and add the tileinteraction object 
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<TileInteraction>();
            TileInteraction tileInteractionTest = gameObject.GetComponent<TileInteraction>();
            
            //Assign
            // create a gangmember object
            GameObject testGangMember = Resources.Load("Animator/gooseani1_0") as GameObject;
         
            // set it 
            tileInteractionTest.SetGangMemberSprite(testGangMember);
            tileInteractionTest.CreateGangMember();

            // skip one frame
            yield return null;

            //Action - retrieve the spawned gangMember
            GameObject SpawnedGangMember = GameObject.FindGameObjectWithTag("GangMember") as GameObject;

            //Assert
            Assert.NotNull(SpawnedGangMember);
    
        }


        




    }
}