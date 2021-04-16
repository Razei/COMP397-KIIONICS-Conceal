using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventoryPanel;
    public Button saveButton;
    public GameObject menuButton;
    public Button inventoryButton;
    public bool Paused;
   // public GameObject player;

    [Header("Player Settings")]
    public PlayerBehaviour player;
    public CameraController playerCamera;
    public Pauseable pausable;

    [Header("Scene Data")]
    public SceneDataSO sceneData;
    //adding label
    public GameObject gameStateElement;

    // Start is called before the first frame update
    void Start()
    {
        Paused = false;

        //pausable, player and playerCamera 
        pausable = FindObjectOfType<Pauseable>();
        player = FindObjectOfType<PlayerBehaviour>();
        playerCamera = FindObjectOfType<CameraController>();

        //saveButton.onClick.AddListener(Save);
        inventoryButton.onClick.AddListener(ShowInventory);
        inventoryPanel.SetActive(false);
        pauseMenu.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }*/
    }

    public void Pause()
    {
        if (Paused)
        {
            Resume();
        }
        else
        {
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            pauseMenu.SetActive(true);
            menuButton.SetActive(false);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Paused = true;
            AudioListener.pause = true;
        }
    }

    public void Resume()
    {
        AudioListener.pause = false;
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        pauseMenu.SetActive(false);
        menuButton.SetActive(true);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Paused = false;
    }

    public void Save()
    {
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        
        //player = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(player.transform.position);
        //PlayerPrefs.SetFloat("Player X", player.transform.position.x);
        //PlayerPrefs.SetFloat("Player Y", player.transform.position.y);
        //PlayerPrefs.SetFloat("Player Z", player.transform.position.z);

    }

    public void ShowInventory(){

        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        if(inventoryPanel.activeSelf == true){
             inventoryPanel.SetActive(false);  
        }
        else{
            inventoryPanel.SetActive(true);
        }
    }

    public void Exit()
    {
        Resume();
        Cursor.lockState = CursorLockMode.None;
        GameController.InvokeSceneChangedEvent();
        SceneManager.LoadScene("MainMenu");
        /*Application.LoadLevel("MainMenu");
        Application.UnloadLevel("GameScene");*/
    }

    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = sceneData.playerPosition;
        player.transform.rotation = sceneData.playerRotation;
        player.controller.enabled = true;

        //player.health= sceneData.playerHealth;
        //player.healthBar.SetHealth(sceneData.playerHealth);

    }

    public void OnSaveButtonPressed()
    {
        //gives unity.engine transform : TYPE MISMATCH

        sceneData.playerPosition = player.transform.position;
        sceneData.playerRotation = player.transform.rotation;
        //sceneData.playerHealth = player.health;
    }


}


