using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace SEPRTest1
{
    enum colleges
    {
        Unknown,
        Alcuin,
        Goodricke,
        Langwith,
        Constantine,
        Halifax,
        Vanbrugh,
        Derwent,
        James,
        Wentworth,
    }

    class GameManager : MonoBehaviour
    {
        
        static void Main(string[] args)
        {
			GenerateMap ();
        }

		static void GenerateMap(){
			Map map = new Map(128);
			Console.Out.Write("test");
		}

		static void NextTurn(){
			
		}
    }
}
