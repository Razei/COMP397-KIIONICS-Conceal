using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerBehaviour player;
    public Text scoreText;
    private int score = 0;
    void OnEnable()
    {
        OnScoreOrbPickupEvent.orbPickedUp += updateScore;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore()
    {
        score += 100;
        scoreText.text = $"Score: {score}";
    }

    public void activateInvisibility()
    {
        player.activateInvisibility();
    }
}
