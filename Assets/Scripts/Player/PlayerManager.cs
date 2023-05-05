using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;

    public PlayerControl Spinner;
    
    public List<Transform> SpawnPositions=new List<Transform>();

    private int index=-1;

    public bool canCreateSpinner=true;
    private void Start() 
    {
        SetSpawnPositionsToData();
        playerData.AllSpinners.Clear();
        playerData.AllSpawnPositions.Clear();
        CreateSpinner();
    }

    public void CreateSpinner()
    {
        SetSpawnerPosition();
        if(canCreateSpinner)
        {
            PlayerControl spinner=Instantiate(Spinner,SpawnPositions[index].position,SpawnPositions[index].transform.rotation);
            playerData.AllSpinners.Add(spinner);
            //Daha sonra bu position bos olan yerlere bakicak
        }
        
    }

    private void SetSpawnerPosition()
    {
        
        if(index<SpawnPositions.Count-1)
        {
            index++;
        }

        else
        {
            Debug.Log("ITS FULL");
            canCreateSpinner=false;
            //Pop-up cikar
            //Buy new Area 10*10 iken 11*11 olur
            
        }
    }

    private void SetSpawnPositionsToData()
    {
        /*for (int i = 0; i < SpawnPositions.Count; i++)
        {
            playerData.AllSpawnPositions.Add(SpawnPositions[i]);
        }*/
    }
    


}
