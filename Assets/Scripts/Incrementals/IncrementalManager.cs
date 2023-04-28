using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementalManager : MonoBehaviour
{
    public PlayerData playerData;

    public int increseEarning;
    public void IncreaseEarning()
    {
        //Yeterli score'a sahipse
        increseEarning+=1;
        playerData.MaxMoneyAmount+=increseEarning;
    }
}
