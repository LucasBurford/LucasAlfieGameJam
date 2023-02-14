using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    static public List<Abilities> unlockedAbilities = new();
    float cooldowns;

    public RockBlast rockBlast;
    public ChainLightning chainLightning;
    public Scorch scorch;

    public GameObject rockProjectile; //Prefab for rockblast
    public GameObject lightningProjectile; //Prefab for chain lightning
    public GameObject scorchProjectile; //Prefab for scorch 

    GameObject user;

    GameObject actualRock;
    Rigidbody2D rgRock;
    GameObject actualChainLightning;

    // Start is called before the first frame update
    void Start()
    {
        user = this.gameObject;

        unlockedAbilities.Add(rockBlast);
        unlockedAbilities.Add(chainLightning);
        unlockedAbilities.Add(scorch);

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void CastRockBlast() // Called here as a Scriptable Object Class cannot instantiate an object due to it not existing in the scene;
    {
        print("Cast Rock Blast");

        if (user.transform.rotation.y == -1) // If facing left
        {
            actualRock = Instantiate(rockProjectile, new Vector2(user.transform.position.x - 2.0f, user.transform.position.y + 1.0f), user.transform.rotation);
            rgRock = actualRock.GetComponent<Rigidbody2D>();
            rgRock.AddForce(new Vector2(-rockBlast.projSpeed, 0.0f));
        }

        else if (user.transform.rotation.y == 0) // If facing right
        {
            actualRock = Instantiate(rockProjectile, new Vector2(user.transform.position.x + 2.0f, user.transform.position.y + 1.0f), user.transform.rotation);
            rgRock = actualRock.GetComponent<Rigidbody2D>();
            rgRock.AddForce(new Vector2(rockBlast.projSpeed, 0.0f));
        }
    }    

    public void CastChainLightning()
    {
        print("Cast Chain Lightning");

        if (user.transform.rotation.y == -1) // If facing left
        {
            actualChainLightning = Instantiate(lightningProjectile, new Vector2(user.transform.position.x - 5.0f , user.transform.position.y + 4.0f), user.transform.rotation);
            
        }

        else if (user.transform.rotation.y == 0) // If facing right
        {
            actualChainLightning = Instantiate(lightningProjectile, new Vector2(user.transform.position.x + 5.0f , user.transform.position.y + 4.0f), user.transform.rotation);
            
        }
    }

    public void CastScorch()
    {
        print("Cast Scorch");
    }
}
