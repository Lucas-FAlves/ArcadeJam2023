using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunCollisionController : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private float stunTime = 0;
    private MovementScript otherMovementScript;
    public ParticleSystem shotPs {  get; private set; }
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[30];

    
    private void Awake()
    {
        shotPs = GetComponent<ParticleSystem>();
    }

    public void Shoot(float angle, float damage, float stunTime)
    {
        var shape = shotPs.shape;
        //shape.angle = angle;
        shotPs.Emit(30);
        this.damage = damage;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (otherMovementScript == null)
            otherMovementScript = other.GetComponent<MovementScript>();

        shotPs.GetParticles(particles);
        Vector3 otherPos = Vector3.zero;
        foreach(var particle in particles)
            if(particle.position != Vector3.zero)
            {
                otherPos = particle.position;
                break;
            }
        otherMovementScript.Hit((Vector2)(other.transform.position - transform.position).normalized * damage, damage, stunTime);
    }

}
