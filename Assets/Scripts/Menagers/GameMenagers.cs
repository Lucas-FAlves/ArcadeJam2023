using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenagers : MonoBehaviour
{
    // instancia do singleton
    public static GameMenagers Instance { get; private set; }
    public HealthBar[] healthBar;
    public GameObject text;
    public string winner;
    public Action OnGameOver;
    void Awake()
    {
        //cria a instancia do singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHealthBar(float health, int player)
    {
        healthBar[player - 1].SetHealth(health);
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        text.GetComponent<TextMeshProUGUI>().text = winner + " Wins";
        text.gameObject.SetActive(true);

        StartCoroutine(EndGame());
        
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);        
    }
}
