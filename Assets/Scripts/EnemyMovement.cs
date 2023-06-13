using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed;

    private Rigidbody2D enemyRigidBody2D;

    private void Awake()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        enemyRigidBody2D.velocity = new Vector2(enemyMoveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        FlippedEnemyFacing();
    }

    private void FlippedEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody2D.velocity.x)), 1f);
    }
}
