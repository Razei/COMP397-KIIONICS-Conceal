using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Pauseable : MonoBehaviour
{

    public List<MonoBehaviour> scripts;
    public List<NavMeshAgent> agents;
    public bool isGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
        agents = FindObjectsOfType<NavMeshAgent>().ToList();

        //use whatever the enemy script name is like CryptoBehaviour
        foreach (var enemy in FindObjectsOfType<EnemyBehaviourScript>())
        {
            scripts.Add(enemy);

        }

        scripts.Add(FindObjectOfType<PlayerBehaviour>());
        scripts.Add(FindObjectOfType<CameraController>());

    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        foreach (var script in scripts)
        {
            script.enabled = !isGamePaused;
        }

        foreach (var agent in agents)
        {
            agent.enabled = !isGamePaused;
        }
    }
}
