using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CRGames_game
{

    /*
        CLASS: MainMenu
        FUNCTION: Handles functions for the main menu
     */

    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Moves to the game scene.
        /// </summary>
        public void PlayGame()
        {
            // Load the next scene in the scene manager
            SceneManager.LoadScene("main");
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}