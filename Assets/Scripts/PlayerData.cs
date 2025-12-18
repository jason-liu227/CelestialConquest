using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;

    public int roundHealth;
    public int totalHealth;

    public CardData[] deck;         // full 10-card deck
    public CardData[] selectedDeck; // chosen 7-card deck

    public PlayerData(string name)
    {
        playerName = name;
        totalHealth = 20; // starting Total HP
        roundHealth = 20; // starting Round HP
        deck = new CardData[10];
    }
}
