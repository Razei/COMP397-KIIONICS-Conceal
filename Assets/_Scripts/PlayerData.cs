using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] position;
    public int score;
    public GameObject scoreObj;
    //public Transform player;

    public PlayerData(PlayerBehaviour player)
    {
        //var playerObject = GameObject.Find("Player");
        // Debug.Log(playerObject);
        //level = player.level;
        //health = player.health;
        scoreObj = GameObject.Find("Score");
        score = int.Parse(scoreObj.GetComponent<UnityEngine.UI.Text>().text);
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        

        Debug.Log("SAVE FOR X COORDINATE" + player.transform.position.x);
        Debug.Log("SAVE FOR Y COORDINATE" + player.transform.position.y);
        Debug.Log("SAVE FOR Z COORDINATE" + player.transform.position.z);
    }


}