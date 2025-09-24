using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // stays when scenes change
        }
        else
        {
            Destroy(gameObject); // avoid duplicates
        }
    }

    public void StartBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void ReturnToSetup()
    {
        SceneManager.LoadScene("SetupScene");
    }
}
