using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnPoint spawnPoint;
    public GameObject playerPrefab;
    public int slotCount = 100;
    
    public void Start()
    {
        // divide by 3 because we have 3 spawn points
        int spawnCount = slotCount / 3;
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPlayer("Scissor");
        }
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPlayer("Rock");
        }
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPlayer("Paper");
        }
    }
    
    public void SpawnPlayer(string _Team)
    {
        switch (_Team)
        {
            case "Scissor":
                var aiScissor = Instantiate(playerPrefab, spawnPoint.spawnScissors.position, Quaternion.identity);
                aiScissor.GetComponent<AIController>().SetTeam(AIController.Team.Scissor);
                break;
            case "Rock":
                var aiRock = Instantiate(playerPrefab, spawnPoint.spawnRock.position, Quaternion.identity);
                aiRock.GetComponent<AIController>().SetTeam(AIController.Team.Rock);
                break;
            case "Paper":
                var aiPaper = Instantiate(playerPrefab, spawnPoint.spawnPaper.position, Quaternion.identity);
                aiPaper.GetComponent<AIController>().SetTeam(AIController.Team.Paper);
                break;
        }
    }
}


[System.Serializable]
public class SpawnPoint
{
    public Transform spawnPaper;
    public Transform spawnRock;
    public Transform spawnScissors;
}