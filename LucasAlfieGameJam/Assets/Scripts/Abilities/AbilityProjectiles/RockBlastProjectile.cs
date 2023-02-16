using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlastProjectile : MonoBehaviour
{
    private GameObject projectile;
    private Rigidbody2D rgProjectile;

    // Start is called before the first frame update
    void Start()
    {
        projectile = this.gameObject;
        rgProjectile = projectile.GetComponent<Rigidbody2D>();

        Invoke("ActivateDrop", AbilityController.unlockedAbilities[0].timeToDestroy);

        rgProjectile.AddForce((projectile.transform.right * AbilityController.unlockedAbilities[0].projSpeed));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateDrop()
    {
        rgProjectile.gravityScale = 3.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) //6 is damageable layer
        {
            collision.gameObject.GetComponent<IDamageable>().OnHit();
            
        }

        Destroy(projectile);

    }
}
