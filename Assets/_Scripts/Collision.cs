using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION IS WORKING!");
        
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.Click);

    }
}
