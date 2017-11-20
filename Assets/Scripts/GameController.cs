using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CRGames_game
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

    class GameController : MonoBehaviour
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
