using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private LayerMask enemiesLayer;

    private Player player;
    private Rigidbody2D bulletRigidBody2D;
    private BoxCollider2D bulletBoxCollider2D;
    private float xSpeed;

    private void Awake()
    {
        bulletRigidBody2D = GetComponent<Rigidbody2D>();
        bulletBoxCollider2D = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        xSpeed = player.transform.transform.localScale.x * bulletSpeed;
    }

    private void Update()
    {
        bulletRigidBody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (bulletBoxCollider2D.IsTouchingLayers(enemiesLayer))
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
