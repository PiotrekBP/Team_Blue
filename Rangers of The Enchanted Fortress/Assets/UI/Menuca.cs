using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuca : MonoBehaviour
{

    //public static bool gameispause = false;
    public bool startLevelWithMenu = true; //if its true ist start a scene with menu
    //private bool isInMenu = false;
    public GameObject mainMenuUI;
    public GameObject pauseMenuUI;


    // Update is called once per frame
    private void Start()
    {
        if (startLevelWithMenu == true)
        {
            //Cursor.visible = true;
            mainMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 0f; //subject to change
            //isInMenu = true;
            //startLevelWithMenu = false;
        }
        else
        {
            //Cursor.visible = true;
            mainMenuUI.SetActive(false);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f; //subject to change
            //isInMenu = false;
            //startLevelWithMenu = true;
        }

    }
    void Update()
    {
        /////// If you want a pause menu \\\\\\\
        /*if (Input.GetKeyDown(KeyCode.Escape) && isInMenu == false)
        {
            if (gameispause) //pause ON
            {
                Resume();
            }
            else //pause OFF
            {
                Pause();
                ;
            }
        }*/

    }


    /*public void Resume() //script off pauseOFF
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameispause = false;
        Cursor.visible = false;

    }
    void Pause() //script off pauseON
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameispause = true;
        Cursor.visible = true;

    }*/

    public void restartLevel() //restart Level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startLevelWithMenu = false;
    }
    // From pause menu to main menu
    /*public void mainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startLevelWithMenu = true;
        Time.timeScale = 0f;
        mainMenuUI.SetActive(true);
        Cursor.visible = true;

    }*/
    public void startGame() //start game from main menu
    {

        Time.timeScale = 1f;
        //animation goes here
        mainMenuUI.SetActive(false);
        //isInMenu = false;

    }
    public void exitGame() //exitgame
    {
        Application.Quit();
    }

}
