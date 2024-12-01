using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] PhysicsMaterial2D normalMat;
    [SerializeField] PhysicsMaterial2D wallMat;

    CapsuleCollider2D coll;
    Collider2D[] colliders = new Collider2D[1];

    public Vector2 checkOffset = Vector2.zero;

    public bool isGrounded => Physics2D.OverlapCircleNonAlloc(
        transform.position + (Vector3)checkOffset, detectionRadius, colliders, groundLayer) != 0;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + (Vector3)checkOffset, detectionRadius);
    }

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        coll.sharedMaterial = isGrounded ? normalMat : wallMat;
    }
}
