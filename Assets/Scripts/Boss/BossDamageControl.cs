using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageControl : MonoBehaviour
{
    public BossData bossData;
    public PlayerData playerData;

    public List<GameObject> Bosses=new List<GameObject>();
    
    [SerializeField] private ParticleSystem DieParticle;
    [SerializeField] private ParticleSystem[] electricParticles;



    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTargetSpin,OnTargetSpin);
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTargetSpin,OnTargetSpin);
    }

    private void Start() 
    {
        EventManager.Broadcast(GameEvent.OnBossUpdate);
        
        for (int i = 0; i < Bosses.Count; i++)
        {
            Bosses[i].SetActive(false);
        }

        //Effect ile
        Bosses[bossData.index].SetActive(true);
    }

    private void OnTargetSpin()
    {
        bossData.BossHealth-=playerData.MaxDamageAmount;
        EventManager.Broadcast(GameEvent.OnBossUpdate);
        for (int i = 0; i < electricParticles.Length; i++)
        {
            electricParticles[i].Play();
        }
        
        if(bossData.BossHealth<=0)
        {
            //index 1 arttirip asagidaki boss'a gec
            bossData.index++;
            DieParticle.Play();
            EventManager.Broadcast(GameEvent.OnBossUpdate);
            EventManager.Broadcast(GameEvent.OnBossDie);
            Debug.Log("HOW MANY TIMES");
        }

        for (int i = 0; i < Bosses.Count; i++)
        {
            Bosses[i].SetActive(false);
        }

        //Effect ile
        Bosses[bossData.index].SetActive(true);
        //Damage Effect
        //Olunce de index 1 arttirip diger boss'a gecis
        //Oldukten index 1 artirip gectikten sonra 
    }

    
}
