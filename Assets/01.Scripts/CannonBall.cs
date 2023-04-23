using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Action OnCompleteExplosion; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 dir)
    {
        _rigidbody.AddForce(dir, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCompleteExplosion?.Invoke();

        Destroy(gameObject);
    }

}
