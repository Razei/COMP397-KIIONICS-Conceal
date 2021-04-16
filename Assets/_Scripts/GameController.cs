using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SceneDataSO playerData;
    public PlayerBehaviour player;
    public int localScore;
    public static event Action sceneChangedEvent;

    public static void InvokeSceneChangedEvent()
    {
        sceneChangedEvent?.Invoke();
    }

    void OnEnable()
    {
        OnGoalEvent.goalEvent += goalReached;
        LoadSaveEvent.dataOperationEvent += DataOperationEventTriggered;
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
