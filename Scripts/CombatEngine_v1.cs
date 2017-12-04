using System;
using System.Collections.Generic;

namespace CombatEngine_v1
{
    class CombatEngine
    {

        public static void Main(string[] args)
        {
            CombatEngine myClass = new CombatEngine();
            myClass.AttackSimulator();
        }


        //tst
        double x = 20f;  // Number of gang members of attacking player, feel free to play around with to check outcomes of different battles
        double y = 20f;  // Number of gang members of the defending player
        double levelOfTile = 0f;  // Level of tile being attacked, eg. tiles only give defence bonus, not attack bonus, between level 0-3 otherwise too powerful
        bool pvc = false;  // Currently not used properly, will be fixed soon, ignore for now
        double pvcBonus = 1f; // not used
        double xdamage;
        double ydamage;
        double hiddenDamageModifier = 0.4f;  // Just to linearly scale the overall damage dealt per turn
        int turn = 1;

        Random rand = new Random();

        public double randomnessFactor()  // Generates random numbers that follow a normal distribution, got from Wikipedia
        {

            double a = 1.0 - rand.NextDouble();
            double b = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(a)) * Math.Sin(2.0 * Math.PI * b);
            double randValue = 1 + 0.2 * randStdNormal; //First number gives the mean of the distribution, the second number gives the variance, 
            //play around with these 2 to adjust the magnitude of randomness
            return randValue;
        }

        public void hasPvc()
        {
            if (pvc)
            {
                pvcBonus = 1.2f;
            }
        }

        public void Attack()
        {

            double rfx = randomnessFactor();
            double rfy = randomnessFactor();
            xdamage = Math.Ceiling(x * rfx * (1 / (1 + (0.15 * levelOfTile))) * pvcBonus * hiddenDamageModifier);
            ydamage = Math.Ceiling(y * rfy * pvcBonus * hiddenDamageModifier);

            x -= ydamage;
            y -= xdamage;

            if (x < 0)
            {
                x = 0;
            }

            if (y < 0)
            {
                y = 0;
            }

            Console.WriteLine("Turn number " + turn);
            Console.WriteLine("RandomnessFactor_x " + rfx);
            Console.WriteLine("RandomnessFactor_y " + rfy);
            Console.WriteLine("x = " + x);
            Console.WriteLine("y = " + y);
            Console.WriteLine();
        }

        public void AttackSimulator()
        {
            while (x != 0 && y != 0)
            {
                hasPvc();
                Attack();
                turn += 1;
            }
            turn = 1;
        }
    }
}

