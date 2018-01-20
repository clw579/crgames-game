using System;
using System.Collections.Generic;
using UnityEngine;

/*
    CLASS: CombatEngine
    FUNCTION: Resolves conflicts between two players, based on the number of gang members that each player has, with some randomness added.
 */

namespace CRGames_game
{
    public class CombatEngine
    {
		// Level of tile being attacked, between 0 - 3 for balance
        double levelOfTile = 0f;
		// Currently not used properly
        bool pvc = false;
        double pvcBonus = 1f; 
		// Linearly scale the overall damage dealt per turn
        double hiddenDamageModifier = 0.4f;
        // The RNG to use to add some randomness to combat
		System.Random rand = new System.Random();

		/// <summary>
		/// Generates random numbers that follow a normal distribution.
		/// </summary>
		/// <returns>A random factor.</returns>
        private double randomnessFactor()  // 
        {
			// Generate two random numbers
            double a = 1.0 - rand.NextDouble();
            double b = 1.0 - rand.NextDouble();
			// Mean of the distribution
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(a)) * Math.Sin(2.0 * Math.PI * b);
			// Variance of the distribution
            double randValue = 1 + 0.2 * randStdNormal;
            return randValue;
        }
		
        /// <summary>
        /// Checks whether any tiles contain the PVC and sets the PVC bonus.
        /// </summary>
        public void hasPvc()
        {
            if (pvc)
            {
                pvcBonus = 1.2f;
            }
        }

		/// <summary>
		/// Resolves a turn of conflict between an attacker and a defender.
		/// </summary>
		/// <param name="attack">Attacker number of gang members.</param>
		/// <param name="defend">Defender number of gang members.</param>
		public int[] Attack(int attack, int defend)
        {
            // Generate a random attack and defence value and clamp these values
            double randomAttack = randomnessFactor();
            double randomDefend = randomnessFactor();
			double attackDamage = Math.Ceiling(attack * randomAttack * (1 / (1 + (0.15 * levelOfTile))) * pvcBonus * hiddenDamageModifier);
            double defendDamage = Math.Ceiling(defend * randomDefend * pvcBonus * hiddenDamageModifier);

			// Calculates the attacker and defender remaining gang members as a double
			double resultAttack = (double)attack - defendDamage;
			double resultDefend = (double)defend - attackDamage;

			// Sets attacker and defender gang members either to 0 if the remaining is below 0 or rounds down to the nearest integer
			attack = (resultAttack < 0) ? 0 : (int)resultAttack;
			defend = (resultDefend < 0) ? 0 : (int)resultDefend;

			return new int[] { attack, defend };
        }

        /// <summary>
        /// Gets the PVC bonus.
        /// </summary>
        /// <returns>The PVC bonus.</returns>
        public double GetPVCBonus(){
            return pvcBonus;
        }

        /// <summary>
        /// Gets the hidden damage modifier.
        /// </summary>
        /// <returns>The hidden damage modifier.</returns>
        public double GetHiddenDamageModifier(){
            return hiddenDamageModifier;
        }

        /// <summary>
        /// Sets the PVC bonus value.
        /// </summary>
        /// <param name="bonus">The value to set as the bonus.</param>
        public void SetPVCBonus(double bonus){
            pvcBonus = bonus;
        }
        
        /// <summary>
        /// Sets the hidden damage modifier value.
        /// </summary>
        /// <param name="modifier">The value to set as the hidden damage modifier.</param>
        public void SetHiddenDamageModifier(double modifier){
            hiddenDamageModifier = modifier;
        }

        /// <summary>
        /// Sets the random number generator to use.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        public void setRandom(System.Random random)
        {
            this.rand = random ;
        }
    }
}