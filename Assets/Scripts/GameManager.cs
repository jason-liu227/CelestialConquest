using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerData player1;
    public PlayerData player2;

    public int currentRound;

    public int selectionCount = 7; // how many cards to pick

    public CardData[] allCards; // assign in inspector

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ResetGame();
    }

    public void ResetGame()
    {
        selectionCount = 7;
        currentRound = 1;

        player1 = new PlayerData("Player 1");
        player2 = new PlayerData("Player 2");

        player1.selectedDeck = new CardData[selectionCount];
        player2.selectedDeck = new CardData[selectionCount];

        Debug.Log("Game reset complete - decks initialized");
        GenerateDecks();
    }

    void GenerateDecks()
    {
        for (int i = 0; i < 10; i++)
        {
            player1.deck[i] = allCards[Random.Range(0, allCards.Length)];
            player2.deck[i] = allCards[Random.Range(0, allCards.Length)];
        }
    }

}
