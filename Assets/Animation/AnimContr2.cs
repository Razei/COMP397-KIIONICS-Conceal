using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimContr2 : MonoBehaviour 
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("WakeUp");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
