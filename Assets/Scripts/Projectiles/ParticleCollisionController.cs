using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionController : MonoBehaviour
{
    public float force = 10f;
    public float damage = 1;
    private MovementScript otherMovementScript;
    private ParticleSystem ps;
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[8];

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        if(otherMovementScript == null)
            otherMovementScript = other.GetComponent<MovementScript>();

        
        ps.GetParticles(particles);
        Vector3 otherPos = Vector3.zero;
        foreach(var p in particles)
            if(p.position != Vector3.zero)
            {
                otherPos = p.position;
                break;
            }
        otherMovementScript.Hit((Vector2)(other.transform.position - otherPos).normalized * force, damage);

    }
}
