using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BattleManager : MonoBehaviour
{
    public TextMeshProUGUI logText;
   
    public Button returnButton;

    PlayerData p1;
    PlayerData p2;

    void Start()
    {
        p1 = GameManager.Instance.player1;
        p2 = GameManager.Instance.player2;

        returnButton.gameObject.SetActive(false); // hide at first

        StartCoroutine(RunBattle());
    }

    IEnumerator RunBattle()
    {
        int round = 0;
        bool p1Turn = p1.hp <= p2.hp;

        while (p1.hp > 0 && p2.hp > 0)
        {
            yield return new WaitForSeconds(1f);

            int index = round % 10;
            CardData card;

            if (p1Turn)
            {
                card = p1.deck[index];
                ApplyCard(card, p1, p2);
                logText.text += $"\nP1 plays {card.cardName} (Deals {card.damage})";
              
            }
            else
            {
                card = p2.deck[index];
                ApplyCard(card, p2, p1);
                logText.text += $"\nP2 plays {card.cardName} (Deals {card.damage})";
            }

            logText.text += $"\nHP: P1={p1.hp}, P2={p2.hp}";

            p1Turn = !p1Turn;
            round++;
        }

        logText.text += p1.hp <= 0 ? "\nPlayer 2 Wins!" : "\nPlayer 1 Wins!";
        returnButton.gameObject.SetActive(true); // show button
    }

    void ApplyCard(CardData card, PlayerData caster, PlayerData target)
    {
        target.hp -= card.damage;
        caster.hp += card.heal;
        if (caster.hp > 20) caster.hp = 20;
    }

    public void ReturnToSetup()
    {
        SceneManager.LoadScene("SetupScene");
    }
}

