using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform attackPoint;
    public LayerMask groundLayer;
    public LayerMask damageableLayer;

    public enum States
    {
        Normal,
        Stunned
    }
    public States currentState;

    public float moveSpeed;
    public float jumpHeight;

    public float meleeAttackDamage;
    public float meleeAttackRange;
    public float meleeAttackCooldown;

    public bool canJump;
    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        GetAttackInput();
        GroundCheck();
    }

    private void GetMovementInput()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;

        Vector2 movementVector = new Vector2(moveX, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            movementVector.y += jumpHeight;
        }

        rb.velocity = movementVector;
    }

    private void GetAttackInput()
    {
        if (Input.GetButton("Fire1") && canAttack)
        {
            canAttack = false;
            StartMeleeAttack();
            StartCoroutine(WaitToResetMeleeAttack());
        }
    }

    private void StartMeleeAttack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange, damageableLayer);

        foreach (Collider2D col in hitObjects)
        {
            col.gameObject.GetComponent<IDamageable>().OnHit();
        }
    }

    private void GroundCheck()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundCheck.position, 0.5f, groundLayer);

        canJump = groundObjects.Length > 0;
    }

    private IEnumerator WaitToResetMeleeAttack()
    {
        yield return new WaitForSeconds(meleeAttackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
    }
}
