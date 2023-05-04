using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BossData", menuName = "Data/BossData", order = 3)]
public class BossData : ScriptableObject 
{

    public int BossHealth;
    public int tempHealth;
    public int index;
    public string BossName;
}
