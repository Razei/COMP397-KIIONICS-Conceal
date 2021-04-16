using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerBehaviour player;
    public Text scoreText;

    void OnEnable()
    {
        SceneDataSO.scoreUpdated += updateScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        scoreText.text = $"Score: {GameController.localScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void activateInvisibility()
    {
        player.activateInvisibility();
    }
}
