using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int hp = 20;
    public CardData[] deck = new CardData[10];

    public PlayerData(string name)
    {
        playerName = name;
    }
}

