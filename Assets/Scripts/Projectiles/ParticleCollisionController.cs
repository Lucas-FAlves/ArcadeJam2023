using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionController : MonoBehaviour
{
    private float damage = 1;
    private float stunTime ;
    private MovementScript otherMovementScript;
    public ParticleSystem Ps{get; private set;}
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[8];

    private void Awake() {
        Ps = GetComponent<ParticleSystem>();
    }

    public void Shoot(float angle, float damage, float stunTime)
    {
        var shape = Ps.shape;
        shape.angle = angle;
        Ps.Emit(1);
        this.damage = damage;
        this.stunTime = stunTime;
    }

    private void OnParticleCollision(GameObject other) {
        if(otherMovementScript == null)
            otherMovementScript = other.GetComponent<MovementScript>();

        
        Ps.GetParticles(particles);
        Vector3 otherPos = Vector3.zero;
        foreach(var p in particles)
            if(p.position != Vector3.zero)
            {
                otherPos = p.position;
                break;
            }
        otherMovementScript.Hit((Vector2)(other.transform.position - transform.position).normalized * damage, damage, stunTime);

    }
}
