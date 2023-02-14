using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform attackPoint;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public TextMeshProUGUI interactText;
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

    public int currentGold;
    public int meleeComboCounter;

    public float moveSpeed;
    public float jumpHeight;
    public float climbSpeed;

    public float meleeAttackDamage;
    public float meleeAttackRange;
    public float meleeAttackCooldown;

    public bool canJump;
    public bool canAttack;
    public bool isMeleeAttacking;
    public bool isOnLadder;

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
        float moveY = Input.GetAxis("Vertical") * moveSpeed;

        // If player is on a ladder, let them climb up it by adding/subtracting from Y position
        if (isOnLadder)
        {
            rb.gravityScale = 0;

            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                rb.velocity = new Vector2(0, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + climbSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - climbSpeed * Time.deltaTime);
            }
        }
        // If not, apply regular gravity
        else
        {
            rb.gravityScale = 1;
        }

        if (Input.mousePosition.x > 590)
        {
            //spriteRenderer.transform.localScale = new Vector3(1, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
            this.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z); // Changed to work with game object rotation and uses angles instead of scale
        }
        else
        {
            //spriteRenderer.transform.localScale = new Vector3(-1, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
            this.gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z); // Changed to work with game object rotation and uses angles instead of scale
        }

        // Create a vector to supply movement to rigid body
        Vector2 movementVector = new Vector2(moveX, rb.velocity.y);

        // Allow player to jump by adding speed to Y position
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            movementVector.y += jumpHeight;
        }

        // Apply the move vector to the rigid body
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

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            //canAttack = false;

            switch (selectedAttack)
            {
                case AvailableAttacks.Melee:
                    {
                        canAttack = false;
                        isMeleeAttacking = true;
                        meleeComboCounter++;
                        StartMeleeAttack();
                        StartCoroutine(WaitToResetAttack(meleeAttackCooldown));
                        StartCoroutine(WaitToResetMeleeAttackAnim());
                    }
                    break;
                case AvailableAttacks.RockBlast:
                    {
                        if (AbilityController.unlockedAbilities.Contains(GetComponent<AbilityController>().rockBlast))
                        {
                            canAttack = false;
                            GetComponent<AbilityController>().CastRockBlast();
                            StartCoroutine(WaitToResetAttack(GetComponent<AbilityController>().rockBlast.cooldown));
                        }
                        else print("RockBlast not yet unlocked");

                        
                    }
                    break;
                case AvailableAttacks.ChainLightning:
                    {
                        if (AbilityController.unlockedAbilities.Contains(GetComponent<AbilityController>().chainLightning))
                        {
                            canAttack = false;
                            GetComponent<AbilityController>().CastChainLightning();
                            StartCoroutine(WaitToResetAttack(GetComponent<AbilityController>().chainLightning.cooldown));
                        }
                        else print("ChainLightning not yet unlocked");
                    }
                    break;
                case AvailableAttacks.Scorch:
                    {
                        if (AbilityController.unlockedAbilities.Contains(GetComponent<AbilityController>().scorch))
                        {
                            canAttack = false;
                            GetComponent<AbilityController>().CastScorch();
                            StartCoroutine(WaitToResetAttack(GetComponent<AbilityController>().scorch.cooldown));
                        }
                        else print("Scorch not yet unlocked");
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

    public void AddGold(int amount)
    {
        currentGold += amount;
    }

    public void HandleInteractable(GameObject obj)
    {
        interactText.text = "E: Interact";

        if (Input.GetKeyDown(KeyCode.E))
        {
            obj.GetComponent<IInteractable>().OnInteract();
            obj.GetComponent<IInteractable>().Interacted = true;
            obj.GetComponent<Collider2D>().isTrigger = true;
            RemoveInteractText();
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

    public void RemoveInteractText()
    {
        interactText.text = "";
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        isOnLadder = collision.gameObject.CompareTag("Ladder");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = false;
        }
    }
}
