using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Team7/LevelSO", order = 0)]
public class LevelSO : ScriptableObject
{

    [SerializeField] private Vector3[] respawn;
    [SerializeField] private int currentLevel;

    public Vector3 GetRespawn(int level)
    {
        return respawn[level];
    }

    public void SetCurrentLevel(int level) { currentLevel = level; }

    public int GetCurrentLevel() { return currentLevel; }
}