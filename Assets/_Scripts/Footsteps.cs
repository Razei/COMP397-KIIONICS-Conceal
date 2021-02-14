using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    CharacterController cc;
    public float stepRate = 0.5f;
    public float stepCoolDown;
    public AudioClip footStep;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
       
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        stepCoolDown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCoolDown < 0f)
        {
         
            GetComponent<AudioSource>().Play();
            stepCoolDown = stepRate;
        }
        if (cc.isGrounded == true && cc.velocity.magnitude > 2f)
        {
            GetComponent<AudioSource>().Play();
        }

        if (cc.velocity.magnitude > 0)
        {
            //GetComponent<AudioSource>().Play();
        }

        if (cc.isGrounded == false)
        {
            //GetComponent<AudioSource>().Play();
        }

    }
}
