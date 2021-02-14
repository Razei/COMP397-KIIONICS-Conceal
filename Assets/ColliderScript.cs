using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ontrigger enter COLLISION IS WORKING!");

        //SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);

        if (other.gameObject.tag == "Enemy")
        {
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            Debug.Log("Triggered by Enemy");

            // game over screen should load here.
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
      

    }
}
