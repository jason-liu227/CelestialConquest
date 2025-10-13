using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public TextMeshProUGUI logText;

    private PlayerData p1;
    private PlayerData p2;

    void Start()
    {
        p1 = GameManager.Instance.player1;
        p2 = GameManager.Instance.player2;

        logText.text = "Battle Start!";
        StartCoroutine(RunBattle());
    }

    IEnumerator RunBattle()
    {
        int round = 0;

        while (p1.health > 0 && p2.health > 0)
        {
            yield return new WaitForSeconds(1f);

            bool p1Turn = (round % 2 == 0);
            CardData card;

            if (p1Turn)
            {
                card = p1.selectedDeck[round % p1.selectedDeck.Length];
                ApplyCard(card, p1, p2);
                logText.text += $"\nP1 plays {card.cardName} (Deals {card.damage})";
            }
            else
            {
                card = p2.selectedDeck[round % p2.selectedDeck.Length];
                ApplyCard(card, p2, p1);
                logText.text += $"\nP2 plays {card.cardName} (Deals {card.damage})";
            }

            round++;
        }

        // end result
        if (p1.health <= 0 && p2.health <= 0)
            logText.text += "\nIt's a Draw!";
        else if (p1.health <= 0)
            logText.text += "\nPlayer 2 Wins!";
        else
            logText.text += "\nPlayer 1 Wins!";
    }

    void ApplyCard(CardData card, PlayerData attacker, PlayerData defender)
    {
        defender.health -= card.damage;
    }
}