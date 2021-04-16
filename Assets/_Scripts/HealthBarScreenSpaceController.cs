using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthBarScreenSpaceController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    private Slider healthbarSlider;

    private void OnEnable()
    {
        PlayerHitEvent.playerHitEvent += TakeDamage;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbarSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHealth();
        }
    }

    public void TakeDamage(int damage)
    {
        healthbarSlider.value -= damage;
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            healthbarSlider.value = 0;
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            Debug.Log("Triggered by Enemy");

            // game over screen should load here.
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ResetHealth()
    {
        healthbarSlider.value = maxHealth;
        currentHealth = maxHealth;
    }
}
