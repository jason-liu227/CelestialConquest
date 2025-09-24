using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public string cardName;
    public int damage;
    public int heal;

    public CardData(string name, int dmg, int healAmt)
    {
        cardName = name;
        damage = dmg;
        heal = healAmt;
    }
}
