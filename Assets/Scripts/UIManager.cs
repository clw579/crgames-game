using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRGames_game
{
    public class UIManager : MonoBehaviour
    {


        public Text noOfGangMembers;
        private string noOfGangMembersString;
        private string currentPlayer;


        void Awake()
        {
           // initialise the string
            noOfGangMembersString = "";
         
    }
        // called from game manager to update the text of the Gang member text UI
        public void updateGangMembers(string text , string name)
        {
            noOfGangMembersString = text;
            currentPlayer = name;
        }
         
        // constant loop to keep updating the text on the screen every frame
        public void Update()
        {
            noOfGangMembers.text = currentPlayer + " Gang Members: " + noOfGangMembersString;
        }





    }
}