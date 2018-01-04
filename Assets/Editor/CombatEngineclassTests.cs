using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
namespace CRGames_game
{

    public class CombatEngineTests
    {
        // Test for the SetPVCBonus
        [Test]
        public void SetPVCTest()
        {
            CombatEngine combatEngine = new CombatEngine();
            combatEngine.SetPVCBonus(5);
            Assert.AreEqual(5, combatEngine.GetPVCBonus());
        }

        // Test for the GetPVCBonus
        [Test]
        public void GetPVCTest()
        {
            CombatEngine combatEngine = new CombatEngine();
            combatEngine.SetPVCBonus(5);
            Assert.AreEqual(5, combatEngine.GetPVCBonus());
        }

        // Test for the SetHiddenDamageModifier
        [Test]
        public void SetModifierTest()
        {
            CombatEngine combatEngine = new CombatEngine();
            combatEngine.SetHiddenDamageModifier(3);
            Assert.AreEqual(3, combatEngine.GetHiddenDamageModifier());
        }

        // Test for the GetHiddenDamageModifier
        [Test]
        public void GetModifierTest()
        {
            CombatEngine combatEngine = new CombatEngine();
            combatEngine.SetHiddenDamageModifier(3);
            Assert.AreEqual(3, combatEngine.GetHiddenDamageModifier());
        }


        [Test]
        public void TestAttack()
        {
            
            CombatEngine combatEngineTest = new CombatEngine();
            var random = NSubstitute.Substitute.For<System.Random>();
            //uses constant random factor of 0.5 for testing
            random.NextDouble().Returns(0.5);
            combatEngineTest.setRandom(random);


            //Assign a attack with equal gangMember Strengths and both results should be 2
            int[] results = combatEngineTest.Attack(5, 5);
           
           
            Assert.AreEqual(results[0], 2);
            Assert.AreEqual(results[1], 2);

            //Assign a attack where results should be 0
            results = combatEngineTest.Attack(1, 1);

            Assert.AreEqual(results[0], 0);
            Assert.AreEqual(results[1], 0);




        }






    }
}
