using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData player1;
    public PlayerData player2;

    public int selectionCount = 7; // how many cards to pick

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ResetGame()
    {
        player1 = new PlayerData("Player 1");
        player2 = new PlayerData("Player 2");

        // Starting HP
        player1.health = 20;
        player2.health = 20;

        // Create 10-card decks
        for (int i = 0; i < 10; i++)
        {
            player1.deck[i] = new CardData($"P1 Card {i + 1}", Random.Range(1, 4), 0);
            player2.deck[i] = new CardData($"P2 Card {i + 1}", Random.Range(1, 4), 0);
        }

        // Initialize selectedDeck
        player1.selectedDeck = new CardData[selectionCount];
        player2.selectedDeck = new CardData[selectionCount];
    }
}
