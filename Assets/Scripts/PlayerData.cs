using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int health;
    public CardData[] deck;         // full 10-card deck
    public CardData[] selectedDeck; // chosen 7-card deck

    public PlayerData(string name)
    {
        playerName = name;
        health = 20; // starting HP
        deck = new CardData[10];
    }
}
