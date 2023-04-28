using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class PlayerData : ScriptableObject 
{
    //Spinledigimde Max Olabilecek Spin Time
    public float MaxSpinTime=10;

    public List<PlayerControl> AllSpinners=new List<PlayerControl>();
    public readonly List<Transform> AllSpawnPositions=new List<Transform>();


}
