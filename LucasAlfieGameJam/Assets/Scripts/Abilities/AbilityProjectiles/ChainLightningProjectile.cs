using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningProjectile : MonoBehaviour
{
    GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        projectile = this.gameObject;

        Destroy(projectile, AbilityController.unlockedAbilities[1].timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) //6 is damageable layer
        {
            collision.gameObject.GetComponent<IDamageable>().OnHit();

            //Need to figure out the chain part here

            Destroy(projectile, 0.1f);
        }

    }
}
