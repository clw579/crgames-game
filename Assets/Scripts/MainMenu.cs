using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

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
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().buildIndex + 1);
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
 