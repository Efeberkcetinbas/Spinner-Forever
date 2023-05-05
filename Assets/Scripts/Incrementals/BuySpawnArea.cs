using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySpawnArea : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameDataV gameData;

    public GameObject Button;
    public List<Transform> spawnArea=new List<Transform>();


    [SerializeField] private GameObject smokeParticle;

    [SerializeField] private Transform spawnPos;


    public void BuyNewArea(int amount)
    {
        if(gameData.score>=amount)
        {
            gameData.score-=amount;
            for (int i = 0; i < spawnArea.Count; i++)
            {
                spawnArea[i].gameObject.SetActive(true);
                playerManager.SpawnPositions.Add(spawnArea[i]);
                EventManager.Broadcast(GameEvent.OnAreaOpen);
                Instantiate(smokeParticle,spawnPos.position,spawnPos.rotation);
            }
            //Particle Effect
            Button.SetActive(false);
            playerManager.canCreateSpinner=true;


        }
    }
}
