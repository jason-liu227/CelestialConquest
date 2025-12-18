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
        if (GameManager.Instance == null)
        {
            Debug.LogError(" GameManager.Instance is NULL when battle starts!");
            return;
        }

        p1 = GameManager.Instance.player1;
        p2 = GameManager.Instance.player2;

        if (p1.selectedDeck == null)
        {
            Debug.LogError(" p1.selectedDeck is NULL!");
            return;
        }

        if (p1.selectedDeck.Length == 0)
        {
            Debug.LogError(" p1.selectedDeck has LENGTH 0!");
            return;
        }

        Debug.Log($"Battle Start!\nP1 Total HP: {p1.totalHealth}\nP2 Total HP: {p2.totalHealth}");
        logText.text = "Battle Start!";
       
        p1 = GameManager.Instance.player1;
        p2 = GameManager.Instance.player2;

        p1.roundHealth = 20;
        p2.roundHealth = 20;

        logText.text = "Battle Start!";
        StartCoroutine(RunBattle());
    }

    IEnumerator RunBattle()
    {
        int round = 0;

        while (p1.roundHealth > 0 && p2.roundHealth > 0)
        {
            yield return new WaitForSeconds(1f);

            bool p1Turn = (round % 2 == 0);
            CardData card;

            if (p1Turn)
            {
                card = p1.selectedDeck[round % p1.selectedDeck.Length];
                ApplyCard(card, p1, p2);
                logText.text += $"\nP1 plays {card.cardName} (Deals {card.damage}) p2 Remaining Health {p2.roundHealth}";
            }
            else
            {
                card = p2.selectedDeck[round % p2.selectedDeck.Length];
                ApplyCard(card, p2, p1);
                logText.text += $"\nP2 plays {card.cardName} (Deals {card.damage}) p1 Remaining Health {p1.roundHealth}";
            }

            round++;
        }

        // end result
        if (p1.roundHealth <= 0 && p2.roundHealth <= 0)
            logText.text += "\nIt's a Draw!";
        else if (p1.roundHealth <= 0)
            logText.text += "\nPlayer 2 Wins!";
        else
            logText.text += "\nPlayer 1 Wins!";
    }

    void ApplyCard(CardData card, PlayerData attacker, PlayerData defender)
    {
        defender.roundHealth -= card.damage;
    }
}