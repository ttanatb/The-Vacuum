using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToLoadAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingImage;

    private GameObject gameManager;

    private AsyncOperation async;


    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void ResumeClick()
    {
        gameManager.GetComponent<GameManagerScript>().PauseHandling();
    }



    /// <summary>
    /// Method that loads in a level given a level
    /// </summary>
    /// <param name="level"></param>
    public void PlayGame(int level)
    {
        // Load current level in the gameManager
        Application.LoadLevel(level);

        GameManagerScript.Instance.CurrentGameState = GameState.Play;
        // Resume the game
        Time.timeScale = 1;

        // Lock and hide the curser
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Method that exits a level given the scene number
    /// </summary>
    /// <param name="level"></param>
    public void ExitGame(int level)
    {
        // Load current level in the gameManager
        Application.LoadLevel(level);

        GameManagerScript.Instance.CurrentGameState = GameState.Menu;
    }

    /// <summary>
    /// Method that loads in a level given a level
    /// </summary>
    /// <param name="level"></param>
    public void LoadScene(int level)
    {
        // Load current level in the gameManager
        Application.LoadLevel(level);
    }

    /// <summary>
    /// Method that hides/setsUnActive a GameObject/Image
    /// </summary>
    /// <param name="level"></param>
    public void HideGameObject(GameObject unhiddenGameObject)
    {
        // Load current level in the gameManager
        unhiddenGameObject.SetActive(false);
    }


    /// <summary>
    /// Method that unhides/setsActive a GameObject/Image
    /// </summary>
    /// <param name="level"></param>
    public void UnHideGameObject(GameObject hiddenGameObject)
    {
        // Load current level in the gameManager
        hiddenGameObject.SetActive(true);
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
