using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SceneDataSO playerData;
    public PlayerBehaviour player;
    public static int localScore = 0;
    public static event Action sceneChangedEvent;

    public static void InvokeSceneChangedEvent()
    {
        sceneChangedEvent?.Invoke();
    }

    void OnEnable()
    {
        // subscribe methods to related events
        OnGoalEvent.goalEvent += goalReached;
        LoadSaveEvent.dataOperationEvent += DataOperationEventTriggered;
        OnScoreOrbPickupEvent.orbPickedUp += updateScore;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    public void TriggerStart()
    {
        Start();
    }

    public void updateScore()
    {
        localScore += 100;
        playerData.SetPlayerScore(localScore);
    }

    public void goalReached()
    {
        Debug.Log("Goal reached triggered");
        SceneManager.LoadScene("GoalScene");
        InvokeSceneChangedEvent();
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
        localScore = 0;
    }
}
