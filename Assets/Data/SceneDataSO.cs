using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
public class SceneDataSO : ScriptableObject
{
    // simple trigger
    public static event Action<int> scoreUpdated;

    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int playerHealth;
    public int playerScore;

    public void SetPlayerScore(int score)
    {
        playerScore = score;
        scoreUpdated?.Invoke(score);
    }
}
