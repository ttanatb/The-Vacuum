using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToLoadAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingImage;

    private AsyncOperation async;


    /// <summary>
    /// Method that loads in a level given a level
    /// </summary>
    /// <param name="level"></param>
    public void ClickAsync(int level)
    {
        // Load current level in the gameManager
        Application.LoadLevel(level);

        //loadingImage.SetActive(true);
        //StartCoroutine(LoadLevelWithBar(level));
    }


    /// <summary>
    /// Method that loads in a level given a level number and a loading bar
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    IEnumerator LoadLevelWithBar (int level)
    {
        async = Application.LoadLevelAsync(level);

        while(!async.isDone) // Check to see if the level is completely loaded
        {
            loadingBar.value = async.progress;
            yield return null;
        }
    }


    /// <summary>
    /// Method that quits the game when hitting the exit button
    /// </summary>
    public void Quit()
    {
        // IF WE ARE RUNNING IN A UNITY STANDALONE
        #if UNITY_STANDALONE
            // Quit the Application
            Application.Quit();
        #endif


        // IF WE ARE RUNNING IN the unity Editor
        #if UNITY_EDITOR

            // Stop play mode in Unity
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

    }
}
