using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            BoxExplosion(Vector2.one, 5f);
        }
    }

    public void OnDamage(int damage, Vector3 point, Vector3 normal)
    {
        BoxExplosion(-1*normal, 3f);
    }

    private void BoxExplosion(Vector3 dir, float power)
    {
        float boxWidth = _spriteRenderer.bounds.size.x;
        float textureWidth = _spriteRenderer.sprite.texture.width;

        GameManager.Instance.MakeDebris(textureWidth / boxWidth, _spriteRenderer.sprite, transform.position, dir, power);

        Destroy(gameObject);
    }

    

    
}
