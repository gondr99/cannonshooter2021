using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private PolygonCollider2D _polygonCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        if(_polygonCollider != null)
        {
            Destroy(_polygonCollider);
        }
        _polygonCollider = gameObject.AddComponent<PolygonCollider2D>(); //새로 넣어주면 크기 맞춰 들어간다.
    }

    public void AddForce(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);

        Destroy(gameObject, 3f);
    }
}
