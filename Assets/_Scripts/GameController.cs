using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SceneDataSO playerData;
    public PlayerBehaviour player;
    public InventoryObject playerInventory;
    private bool loadData = false;
    public static int localScore;

    void OnEnable()
    {
        // subscribe methods to related events
        OnGoalEvent.goalEvent += goalReached;
        LoadSaveEvent.dataOperationEvent += DataOperationEventTriggered;
        OnScoreOrbPickupEvent.orbPickedUp += updateScore;
        PlayerHitEvent.playerHitEvent += removeScore;
    }

    void OnDisable()
    {
        // subscribe methods to related events
        OnGoalEvent.goalEvent -= goalReached;
        LoadSaveEvent.dataOperationEvent -= DataOperationEventTriggered;
        OnScoreOrbPickupEvent.orbPickedUp -= updateScore;
        PlayerHitEvent.playerHitEvent -= removeScore;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            player = FindObjectOfType<PlayerBehaviour>();

            if (loadData)
            {
                // trigger the load event
                LoadSaveEvent.LoadData();
            }
        } 
        else
        {
            loadData = false;
        }
    }

    public void ResetGame()
    {
        localScore = 0;
        playerInventory.ClearInventory();
    }

    public void TriggerStart()
    {
        Start();
    }
    public void SetLoad(bool value)
    {
        loadData = value;
    }

    public void updateScore(int value)
    {
        localScore += value;
        playerData.SetPlayerScore(localScore);
    }

    public void removeScore(int value)
    {
        if (localScore <= 0)
        {
            localScore = 0;
        } else
        {
            localScore -= value;
        }

        playerData.SetPlayerScore(localScore);
    }

    public void goalReached()
    {
        Debug.Log("Goal reached triggered");
        SceneManager.LoadScene("GoalScene");
    }

    public void DataOperationEventTriggered(DataOperation dataOperation)
    {
        switch (dataOperation)
        {
            case DataOperation.SAVE:
                if (player)
                {
                    playerData.playerPosition = player.transform.position;
                    playerData.playerRotation = player.transform.rotation;
                }
                break;
            case DataOperation.LOAD:
                if (player)
                {
                    if (player.controller)
                    {
                        player.controller.enabled = false;
                        player.transform.position = playerData.playerPosition;
                        player.transform.rotation = playerData.playerRotation;
                        player.controller.enabled = true;
                    }
                    else
                    {
                        player.transform.position = playerData.playerPosition;
                        player.transform.rotation = playerData.playerRotation;
                    }
                    localScore = playerData.playerScore;
                }
                break;
            default:
                break;
        }
    }

    void OnApplicationQuit()
    {
        ResetGame();
    }
}
