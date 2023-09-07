using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    public List<Collider2D> inRange;
    private float rangeRadius = 4f;
    [SerializeField] private LayerMask layerMask;
    
    private void Update() 
    { 
        inRange = Physics2D.OverlapCircle(gameObject.transform.position, rangeRadius, layerMask).results;
    }
}
