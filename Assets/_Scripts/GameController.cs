using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SceneDataSO sceneData;
    public PlayerBehaviour player;
    public static event Action sceneChangedEvent;
    public SaveSystem save;
  


    public static void InvokeSceneChangedEvent()
    {
        sceneChangedEvent?.Invoke();
    }

    void OnEnable()
    {
        OnGoalEvent.goalEvent += goalReached;
        LoadSaveEvent.dataOperationEvent += DataOperationEventTriggered;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        
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
                    sceneData.playerPosition = player.transform.position;
                    sceneData.playerRotation = player.transform.rotation;
                }
                break;
            case DataOperation.LOAD:
                if (player)
                {
                    player.controller.enabled = false;
                    player.transform.position = sceneData.playerPosition;
                    player.transform.rotation = sceneData.playerRotation;
                    player.controller.enabled = true;
                }
                break;
            default:
                break;
        }
    }

    void OnApplicationQuit()
    {
        sceneData.playerScore = 0;
    }
}
