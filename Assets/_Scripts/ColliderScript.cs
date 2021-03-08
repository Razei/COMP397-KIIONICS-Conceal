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

        //IF THERE IS A COLLISION 
/*        if (other.gameObject.tag == "Enemy")
        {
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);
            Debug.Log("Triggered by Enemy");

            // game over screen should load here.
            SceneManager.LoadScene("GameOver");
            Cursor.lockState = CursorLockMode.None;
        }*/

        //Game note: if health isn't zero, reduce health 

        //Game note: if health IS ZERO, game over screen

    }

    void OnCollisionEnter(Collision collision)
    {
      

    }
}
