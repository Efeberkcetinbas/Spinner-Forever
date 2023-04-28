using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameDataV gameData;
    public PlayerData playerData;

    private void Start() 
    {
        ResetGame();
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
    }

    void OnGameOver()
    {
        gameData.isGameEnd=true;
    }

    void ResetGame()
    {
        playerData.AllSpinners.Clear();
        playerData.AllSpawnPositions.Clear();
    }
}
