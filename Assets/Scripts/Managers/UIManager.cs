using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score,highscore,endingScore;

    [Header("Boss Control")]
    public TextMeshProUGUI bossHealthText;
    public TextMeshProUGUI bossNameText;
    public Image bossProgressBar;

    public GameDataV gameData;
    public PlayerData playerData;
    public BossData bossData;


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnUpdateUI, OnUIUpdate);
        EventManager.AddHandler(GameEvent.OnBossUpdate,OnBossUpdate);
        EventManager.AddHandler(GameEvent.OnBossDie,OnBossDie);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnUpdateUI, OnUIUpdate);
        EventManager.RemoveHandler(GameEvent.OnBossUpdate,OnBossUpdate);
        EventManager.RemoveHandler(GameEvent.OnBossDie,OnBossDie);
    }

    
    void OnUIUpdate()
    {
        score.SetText(gameData.score.ToString());
        score.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.2f).OnComplete(()=>score.transform.DOScale(new Vector3(1,1f,1f),0.2f));
    }

    void OnBossUpdate()
    {
        bossNameText.SetText(bossData.BossName.ToString());
        bossHealthText.SetText(bossData.BossHealth.ToString() + " HP");
        bossProgressBar.DOFillAmount((float)bossData.BossHealth/bossData.tempHealth,0.1f);
    }

    void OnBossDie()
    {
        bossData.tempHealth=bossData.BossHealth;
    }

    

    
}
