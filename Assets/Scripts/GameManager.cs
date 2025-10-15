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
        selectionCount = 7;

        player1 = new PlayerData("Player 1");
        player2 = new PlayerData("Player 2");

        player1.health = 20;
        player2.health = 20;

        player1.deck = new CardData[10];
        player2.deck = new CardData[10];

        // Initialize selected decks as empty arrays
        player1.selectedDeck = new CardData[0];
        player2.selectedDeck = new CardData[0];

        for (int i = 0; i < 10; i++)
        {
            player1.deck[i] = new CardData($"P1 Card {i + 1}", Random.Range(1, 4), 0);
            player2.deck[i] = new CardData($"P2 Card {i + 1}", Random.Range(1, 4), 0);
        }

        Debug.Log("Game reset complete - decks initialized");
    }

}
