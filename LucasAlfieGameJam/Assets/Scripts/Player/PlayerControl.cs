using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform groundCheck;

    public LayerMask groundLayer;

    public float moveSpeed;
    public float jumpHeight;

    public bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        GroundCheck();
    }

    private void GetInput()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;

        Vector2 movementVector = new Vector2(moveX, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            movementVector.y += jumpHeight;
        }

        rb.velocity = movementVector;
    }

    private void GroundCheck()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundCheck.position, 0.5f, groundLayer);

        canJump = groundObjects.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
