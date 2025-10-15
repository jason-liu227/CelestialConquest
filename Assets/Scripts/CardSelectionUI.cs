using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardSelectionUI : MonoBehaviour
{
    public Button[] cardButtons; // assign exactly 10 buttons in Inspector
    private bool[] selected;
    private int selectionCount = 0;
    private int maxSelection;

    void Start()
    {
        // Ensure GameManager is initialized
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        maxSelection = GameManager.Instance.selectionCount;

        // Safety: ensure arrays match number of buttons
        selected = new bool[cardButtons.Length];

        // setup listeners
        for (int i = 0; i < cardButtons.Length; i++)
        {
            int index = i;
            cardButtons[i].onClick.AddListener(() => ToggleCard(index));
        }

        RefreshUI();
    }

    void ToggleCard(int index)
    {
        if (index < 0 || index >= selected.Length) return; // safety check

        if (selected[index])
        {
            selected[index] = false;
            selectionCount--;
        }
        else
        {
            if (selectionCount >= maxSelection) return; // cap at max
            selected[index] = true;
            selectionCount++;
        }

        RefreshUI();
    }

    void RefreshUI()
    {
        if (GameManager.Instance?.player1?.deck == null) return;

        int count = Mathf.Min(cardButtons.Length, GameManager.Instance.player1.deck.Length);

        for (int i = 0; i < count; i++)
        {
            var text = cardButtons[i].GetComponentInChildren<Text>();
            if (text != null && i < GameManager.Instance.player1.deck.Length)
            {
                text.text = GameManager.Instance.player1.deck[i].cardName;
            }

            var colors = cardButtons[i].colors;
            colors.normalColor = selected[i] ? Color.green : Color.white;
            cardButtons[i].colors = colors;
        }
    }

    public void StartBattle()
    {
        // Safety checks
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        if (GameManager.Instance.player1 == null || GameManager.Instance.player1.deck == null)
        {
            Debug.LogError("Player1 or deck is not initialized!");
            return;
        }

        // Count how many cards are selected
        int picked = 0;
        for (int i = 0; i < selected.Length; i++)
        {
            if (selected[i]) picked++;
        }

        if (picked != maxSelection)
        {
            Debug.LogWarning($"You must select exactly {maxSelection} cards! Selected: {picked}");
            return;
        }

        // Create new selected deck array
        List<CardData> selectedCards = new List<CardData>();

        // Copy selected cards safely
        for (int i = 0; i < selected.Length; i++)
        {
            if (selected[i] && i < GameManager.Instance.player1.deck.Length)
            {
                if (GameManager.Instance.player1.deck[i] != null)
                {
                    selectedCards.Add(GameManager.Instance.player1.deck[i]);
                }
            }
        }

        // Assign to player's selected deck
        GameManager.Instance.player1.selectedDeck = selectedCards.ToArray();

        // Give AI random cards safely
        List<CardData> aiSelectedCards = new List<CardData>();
        for (int i = 0; i < maxSelection; i++)
        {
            if (GameManager.Instance.player2.deck != null && GameManager.Instance.player2.deck.Length > 0)
            {
                int randomIndex = Random.Range(0, GameManager.Instance.player2.deck.Length);
                if (GameManager.Instance.player2.deck[randomIndex] != null)
                {
                    aiSelectedCards.Add(GameManager.Instance.player2.deck[randomIndex]);
                }
            }
        }
        GameManager.Instance.player2.selectedDeck = aiSelectedCards.ToArray();

        // Debug: Verify the transfer
        Debug.Log($"P1 selected {GameManager.Instance.player1.selectedDeck.Length} cards for battle");
        Debug.Log($"P2 selected {GameManager.Instance.player2.selectedDeck.Length} cards for battle");

        // Load battle scene
        SceneManager.LoadScene("BattleScene");
    }
}