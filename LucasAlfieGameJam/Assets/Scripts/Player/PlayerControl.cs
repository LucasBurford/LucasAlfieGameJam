using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform attackPoint;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public LayerMask groundLayer;
    public LayerMask damageableLayer;

    public enum States
    {
        Normal,
        Stunned
    }
    public States currentState;

    public enum AvailableAttacks
    {
        Melee,
        RockBlast,
        ChainLightning,
        Scorch
    }
    public AvailableAttacks selectedAttack;

    public int meleeComboCounter;

    public float moveSpeed;
    public float jumpHeight;

    public float meleeAttackDamage;
    public float meleeAttackRange;
    public float meleeAttackCooldown;

    public bool canJump;
    public bool canAttack;
    public bool isMeleeAttacking;

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
        UpdateAnimations();
    }

    private void GetMovementInput()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.mousePosition.x > 590)
        {
            spriteRenderer.transform.localScale = new Vector3(1, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
        }
        else
        {
            spriteRenderer.transform.localScale = new Vector3(-1, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
        }

        Vector2 movementVector = new Vector2(moveX, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            movementVector.y += jumpHeight;
        }

        rb.velocity = movementVector;
    }

    private void GetAttackInput()
    {
        #region Selecting attacks
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedAttack = AvailableAttacks.Melee;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedAttack = AvailableAttacks.RockBlast;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedAttack = AvailableAttacks.ChainLightning;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedAttack = AvailableAttacks.Scorch;
        }
        #endregion

        if (Input.GetButton("Fire1") && canAttack)
        {
            canAttack = false;

            switch (selectedAttack)
            {
                case AvailableAttacks.Melee:
                    {
                        isMeleeAttacking = true;
                        meleeComboCounter++;
                        StartMeleeAttack();
                        StartCoroutine(WaitToResetAttack(meleeAttackCooldown));
                        StartCoroutine(WaitToResetMeleeAttackAnim());
                    }
                    break;
                case AvailableAttacks.RockBlast:
                    {
                        AbilityController.abilities[0].Cast(this.gameObject);
                        StartCoroutine(WaitToResetAttack(AbilityController.abilities[0].cooldown));
                    }
                    break;
                case AvailableAttacks.ChainLightning:
                    {
                        AbilityController.abilities[1].Cast(this.gameObject);
                        StartCoroutine(WaitToResetAttack(AbilityController.abilities[1].cooldown));
                    }
                    break;
                case AvailableAttacks.Scorch:
                    {
                        AbilityController.abilities[2].Cast(this.gameObject);
                        StartCoroutine(WaitToResetAttack(AbilityController.abilities[2].cooldown));
                    }
                    break;
            }


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

    private void UpdateAnimations()
    {
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
        animator.SetBool("IsAttacking", isMeleeAttacking);
    }

    private IEnumerator WaitToResetAttack(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    private IEnumerator WaitToResetMeleeAttackAnim()
    {
        yield return new WaitForSeconds(0.3f);
        isMeleeAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }
}
