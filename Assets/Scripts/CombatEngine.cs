using System;
using System.Collections.Generic;
using UnityEngine;

namespace CRGames_game
{
    class CombatEngine
    {
		// Level of tile being attacked, between 0 - 3 for balance
        double levelOfTile = 0f;
		// Currently not used properly
        bool pvc = false;
        double pvcBonus = 1f; 
		// Linearly scale the overall damage dealt per turn
        double hiddenDamageModifier = 0.4f;
		System.Random rand = new System.Random();

		/// <summary>
		/// Generates random numbers that follow a normal distribution.
		/// </summary>
		/// <returns>A random factor.</returns>
        public double randomnessFactor()  // 
        {
			
            double a = 1.0 - rand.NextDouble();
            double b = 1.0 - rand.NextDouble();
			// Mean of the distribution
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(a)) * Math.Sin(2.0 * Math.PI * b);
			// Variance of the distribution
            double randValue = 1 + 0.2 * randStdNormal;
            return randValue;
        }
			
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

//            Console.WriteLine("Turn number " + turn);
//            Console.WriteLine("RandomnessFactor_x " + rfx);
//            Console.WriteLine("RandomnessFactor_y " + rfy);
//            Console.WriteLine("x = " + x);
//            Console.WriteLine("y = " + y);
//            Console.WriteLine();
        }
    }
}