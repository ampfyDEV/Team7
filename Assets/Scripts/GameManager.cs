using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelSO respawnLocation;
    [SerializeField] private GameObject player;
    [SerializeField] private int level;
    public void GameOver()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        player.transform.position = respawnLocation.GetRespawn(level);
    }
}
