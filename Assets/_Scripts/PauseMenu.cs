using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventoryPanel;
    public Button inventoryButton;
    public bool Paused;

    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
        inventoryButton.onClick.AddListener(ShowInventory);
        inventoryPanel.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
                inventoryPanel.SetActive(false);
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Paused = false;
            } else
            {
                SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
                pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;
                Paused = true;
            }
            
        }
    }

    public void Resume()
    {
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Paused = false;
    }

    public void Save()
    {
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);

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
        SceneManager.LoadScene("MainMenu");
    }
}
