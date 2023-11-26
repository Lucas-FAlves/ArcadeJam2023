using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    public Transform enemy;
    public float influenceRange;
    public float intensity;
    private float distanceToPlayer;
    Vector2 pullForce;
    MovementScript movementScript;
    private float auxKnockback = 0f;
    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector2.Distance(enemy.position, transform.position);
        if(distanceToPlayer < influenceRange)
        {
            pullForce = (transform.position - enemy.position).normalized/ distanceToPlayer*intensity;
            movementScript.knockbackForceMultiplier -= movementScript.knockbackForceMultiplier  * 0.06f;
            movementScript.ApplyForce(pullForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Transform>();
        movementScript = collision.GetComponent<MovementScript>();
        auxKnockback = movementScript.knockbackForceMultiplier;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        movementScript.knockbackForceMultiplier = auxKnockback;
    }
}
