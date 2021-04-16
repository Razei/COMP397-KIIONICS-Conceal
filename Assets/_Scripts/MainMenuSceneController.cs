using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
     
    GameObject mainMenu;
    GameObject options;
    GameObject loadGame;
    public GameController gameController;
    public SceneDataSO defaultData;
    private GameObject activeObject;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.Find("MainMenuContainer");
        options = GameObject.Find("OptionsContainer");
        loadGame = GameObject.Find("LoadGameContainer");

        if (!gameController)
        {
            gameController = FindObjectOfType<GameController>();
        }

        options?.SetActive(false);
        loadGame?.SetActive(false);
    }

    public void NewGame()
    {
        gameController.ResetGame();
        SceneManager.LoadScene("GameScene");
        gameController.TriggerStart();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");

        // called after the next scene is loaded
        SceneManager.sceneLoaded += afterLoaded;
        /*if (loadGame)
        {
            activeObject = loadGame;
            switchFromMainPanel();
        }*/
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

    private void afterLoaded(Scene scene, LoadSceneMode mode)
    {
        // trigger the start function to get the player reference
        gameController.TriggerStart();

        // trigger the load event
        LoadSaveEvent.LoadData();

        // unsubscribe
        SceneManager.sceneLoaded -= afterLoaded;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
