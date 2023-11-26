using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    [SerializeField] BaseChar baseChar;

    public GameObject player;

    [SerializeField] MovementScript slowedPlayer;

    [SerializeField] private float slowSpeed;
    [SerializeField] private float areaLife;
    private float timer;
    private float auxPlayerSpd;

    private void Awake()
    {
        //baseChar = player.GetComponent<BaseChar>();
        //slowedPlayer = baseChar.MovementScript.otherPlayer.GetComponent<MovementScript>();
        //auxPlayerSpd = slowedPlayer.maxSpeed;
    }

    private void Start()
    {
        timer = areaLife;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }

/*
    private void FixedUpdate()
    {
        if((baseChar.MovementScript.otherPlayer.position - transform.position).magnitude < transform.localScale.x/2){
            Debug.Log("Slow");
            slowedPlayer.maxSpeed = slowSpeed;
        }else{
            slowedPlayer.maxSpeed = auxPlayerSpd;
        }

    }*/
}
