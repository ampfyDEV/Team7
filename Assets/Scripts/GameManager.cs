using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private LevelSO respawnLocation;
    [SerializeField] private GameObject player;
    [SerializeField] private int level;
    public Action<bool> onLevelComplete;

    private void OnEnable()
    {
        Instance = this;
    }
    private void Awake()
    {
        level = respawnLocation.GetCurrentLevel();
        Debug.Log(level);
    }
    private void Start()
    {
        player.transform.localPosition = respawnLocation.GetRespawn(level);
    }
    public void GameOver()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void updateLevel(int updatedLevel)
    {
        level = updatedLevel;
        respawnLocation.SetCurrentLevel(updatedLevel);
    }
}
