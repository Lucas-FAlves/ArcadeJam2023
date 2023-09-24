using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenagers : MonoBehaviour
{
    // instancia do singleton
    public static GameMenagers Instance { get; private set; }
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
}
