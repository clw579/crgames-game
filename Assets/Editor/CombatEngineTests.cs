using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
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
    }
}
