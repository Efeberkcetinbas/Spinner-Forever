using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
public class GameDataV : ScriptableObject
{
    public bool isGameEnd;

    public int score;

    public float spinTimer;
}
