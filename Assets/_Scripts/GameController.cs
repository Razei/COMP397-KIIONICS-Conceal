using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject mainMenu;
    GameObject options;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuContainer");
        options = GameObject.Find("OptionsContainer");
        options.SetActive(false);
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

    }

    public void Options()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }

    public void OptionsBack()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
