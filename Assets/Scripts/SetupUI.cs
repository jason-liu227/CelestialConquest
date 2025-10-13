using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetupUI : MonoBehaviour
{
    public void CardSelection()
    {
        SceneManager.LoadScene("CardSelectionScene");
    }
}