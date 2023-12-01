using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private LevelSO respawnLocation;
    [SerializeField] private GameObject player;
    [SerializeField] private int level;
    public Action<bool> onLevelComplete;
    [SerializeField] private GameObject PauseScreen;


    private void OnEnable()
    {
        Instance = this;
    }
    private void Awake()
    {
        level = respawnLocation.GetCurrentLevel();
    }
    private void Start()
    {
        Time.timeScale = 0;
        player.transform.localPosition = respawnLocation.GetRespawn(level);
    }

    public void OnPlay()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
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

    public void onMenuStart(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }
}
