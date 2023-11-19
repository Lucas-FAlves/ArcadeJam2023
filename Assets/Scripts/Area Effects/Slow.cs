using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    [SerializeField] private float slowSpeed;
    [SerializeField] private float areaLife;
    private float timer;
    private float auxPlayerSpd;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        MovementScript slowedPlayer = collision.GetComponent<MovementScript>();
        auxPlayerSpd = slowedPlayer.maxSpeed;
        slowedPlayer.maxSpeed = slowSpeed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MovementScript slowedPlayer = collision.GetComponent<MovementScript>();
        slowedPlayer.maxSpeed = auxPlayerSpd;
    }
}
