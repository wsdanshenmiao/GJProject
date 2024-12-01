/*
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private PlayerController playerController;
    public bool isGround = true;
    public float groundCheckRadius = .2f;
    public Vector2 groundCheckOffset = Vector2.zero;
    public LayerMask layerMask;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhysics();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)groundCheckOffset, groundCheckRadius);
    }

    private void CheckPhysics()
    {
        Vector2 checkPoint = transform.position + (Vector3)groundCheckOffset;
        Collider2D stampColl = Physics2D.OverlapCircle( checkPoint, groundCheckRadius, layerMask);
        if(stampColl != null){
            isGround = true;
        }
        else{
            isGround = false;
        }
    }
}
*/