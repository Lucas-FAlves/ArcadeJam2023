using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigArrow : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _stunTime;

    private Vector2 _shootDirection;
    private Rigidbody2D _rigidBody;
    private MovementScript otherMovementScript;
    // Start is called before the first frame update
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidBody.velocity = _shootDirection * _speed;
    }

    public void Shoot(Vector2 _fireDirection, float _damage, float _stunTime)
    {
        _shootDirection = _fireDirection;
        this._damage = _damage;
        this._stunTime = _stunTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform parentTranfrom = collision.gameObject.transform.parent;
            if (parentTranfrom != null)
            {
                otherMovementScript = collision.gameObject.GetComponentInParent<MovementScript>();
                if (otherMovementScript != null)
                    //Vector3 otherPos = Vector3.zero;
                    otherMovementScript.Hit((Vector2)(collision.gameObject.transform.position - transform.position).normalized * _damage, _damage, _stunTime);
                Destroy(this.gameObject);

            }
        }
        if (collision.gameObject.CompareTag("EndGameWalls"))
        {
            Destroy(this.gameObject);
        }
    }
}
