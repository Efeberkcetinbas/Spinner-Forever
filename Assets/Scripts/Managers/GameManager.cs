using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameDataV gameData;
    public PlayerData playerData;

    private void Start() 
    {
        ClearData();
    }
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.AddHandler(GameEvent.OnTargetSpin,OnIncreaseScore);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver,OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnTargetSpin,OnIncreaseScore);
    }

    void OnGameOver()
    {
        gameData.isGameEnd=true;
        /*FailPanel.SetActive(true);
        FailPanel.transform.DOScale(Vector3.one,1f).SetEase(ease);
        playerData.playerCanMove=false;
        gameData.isGameEnd=true;

        if(gameData.score>gameData.highScore)
        {
            gameData.highScore=gameData.score;
            PlayerPrefs.SetInt("highscore",gameData.highScore);
        }

        EventManager.Broadcast(GameEvent.OnUpdateGameOverUI);*/
    }


    void OnIncreaseScore()
    {
        DOTween.To(GetScore,ChangeScore,gameData.score+playerData.MaxDamageAmount,.2f).OnUpdate(UpdateUI);
    }

    private int GetScore()
    {
        return gameData.score;
    }

    private void ChangeScore(int value)
    {
        gameData.score=value;
    }

    private void UpdateUI()
    {
        EventManager.Broadcast(GameEvent.OnUpdateUI);
    }


    void ClearData()
    {
        gameData.score=0;
        gameData.isGameEnd=false;
    }
}
