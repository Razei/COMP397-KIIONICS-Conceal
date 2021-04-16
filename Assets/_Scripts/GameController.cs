using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
     
    GameObject mainMenu;
    GameObject options;
    GameObject loadGame;
    private GameObject activeObject;

    void Awake()
    {
        OnGoalEvent.goalEvent += goalReached;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuContainer");
        options = GameObject.Find("OptionsContainer");
        loadGame = GameObject.Find("LoadGameContainer");

        options?.SetActive(false);
        loadGame?.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        
        SceneManager.LoadScene("GameScene");
    }

    public void LoadGame()
    {
        if (loadGame)
        {
            activeObject = loadGame;
            switchFromMainPanel();
        }
    }

    public void showOptions()
    {
        activeObject = options;
        switchFromMainPanel();
    }

    private void switchFromMainPanel()
    {
        mainMenu.SetActive(false);
        activeObject.SetActive(true);
    }

    public void switchToMainPanel()
    {
        mainMenu.SetActive(true);
        activeObject.SetActive(false);
    }

    public void returnToMainScene(){
         SceneManager.LoadScene("MainMenu");
    }

    public void goalReached()
    {
        SceneManager.LoadScene("Goal");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
