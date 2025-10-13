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
        int count = Mathf.Min(cardButtons.Length, GameManager.Instance.player1.deck.Length);

        for (int i = 0; i < count; i++)
        {
            var text = cardButtons[i].GetComponentInChildren<Text>();
            text.text = GameManager.Instance.player1.deck[i].cardName;

            var colors = cardButtons[i].colors;
            colors.normalColor = selected[i] ? Color.green : Color.white;
            cardButtons[i].colors = colors;
        }
    }

    public void StartBattle()
    {
        // Count how many cards are selected
        int picked = 0;
        for (int i = 0; i < selected.Length; i++)
            if (selected[i]) picked++;

        if (picked != maxSelection)
        {
            Debug.LogWarning($"You must select exactly {maxSelection} cards! Selected: {picked}");
            return;
        }

        // Save chosen cards safely
        int j = 0;
        int deckLength = Mathf.Min(GameManager.Instance.player1.selectedDeck.Length, maxSelection);

        for (int i = 0; i < selected.Length; i++)
        {
            if (selected[i])
            {
                if (j >= deckLength) break; // prevent overflow
                if (i >= GameManager.Instance.player1.deck.Length) continue; // safety if deck < buttons
                GameManager.Instance.player1.selectedDeck[j] = GameManager.Instance.player1.deck[i];
                j++;
            }
        }

        // Give AI 7 random cards safely
        for (int i = 0; i < deckLength; i++)
        {
            GameManager.Instance.player2.selectedDeck[i] =
                GameManager.Instance.player2.deck[Random.Range(0, GameManager.Instance.player2.deck.Length)];
        }

        SceneManager.LoadScene("BattleScene");
    }
}