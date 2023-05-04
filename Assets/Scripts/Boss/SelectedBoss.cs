using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedBoss : MonoBehaviour
{
    [SerializeField] private string specialName;
    [SerializeField] private int specialHealth;

    public BossData bossData;

    
    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnBossDie,OnBossDie);
        EventManager.AddHandler(GameEvent.OnBossUpdate,OnBossUpdate);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnBossDie,OnBossDie);
        EventManager.RemoveHandler(GameEvent.OnBossUpdate,OnBossUpdate);
    }

    private void Start() 
    {
        bossData.BossName=specialName;
        bossData.BossHealth=specialHealth;
        bossData.tempHealth=specialHealth;
        //StartCoroutine(WaitForBoss());
        EventManager.Broadcast(GameEvent.OnBossUpdate);
    }

    private void OnBossDie()
    {
        //Die Effect
        StartCoroutine(WaitForBoss());
    }

    private void OnBossUpdate()
    {
        StartCoroutine(WaitForBoss());
    }

    private IEnumerator WaitForBoss()
    {
        yield return new WaitForEndOfFrame();
        bossData.BossName=specialName;
        bossData.BossHealth=specialHealth;
    }
}
