using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndingController : MonoBehaviour
{
    public GameObject score;

    public PlayerData data;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("testing final");

        int value = data.score;
        Debug.Log(value);
        score.GetComponent<UnityEngine.UI.Text>().text = value.ToString();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
