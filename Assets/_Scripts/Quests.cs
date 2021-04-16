using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quests : MonoBehaviour
{
    public Quest quest;
    public PlayerBehaviour player;

    public GameObject questWindow;

    public Text titleText;
    public Text descriptionText;
    public Text scoreText;

  



    public void Start()
    {
        questWindow.SetActive(false);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        scoreText.text = quest.scoreReward.ToString();
        quest.isActive = true;
    }

}

[System.Serializable]
public  class Quest {
   
        public bool isActive;
        public string title;
        public string description;
        public int scoreReward;
    
}