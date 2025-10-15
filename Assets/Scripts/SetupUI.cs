using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetupUI : MonoBehaviour
{
    public void CardSelection()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("CardSelectionScene");
    }
}