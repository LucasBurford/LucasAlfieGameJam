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

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;

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
        GameObject actualRock;
        Rigidbody2D rgRock;

        if (player.transform.localScale == new Vector3(-1, 2, 1)) // If facing left
        {
            actualRock = Instantiate(rock, new Vector2(player.transform.position.x - 2.0f, player.transform.position.y + 1.0f), player.transform.rotation);
            rgRock = actualRock.GetComponent<Rigidbody2D>();
            rgRock.AddForce(new Vector2(-1.0f, 0.0f) * rockBlast.projSpeed);
        }

        else if (player.transform.localScale == new Vector3(1, 2, 1)) // If facing right
        {
            actualRock = Instantiate(rock, new Vector2(player.transform.position.x + 2.0f, player.transform.position.y + 1.0f), player.transform.rotation);
            rgRock = actualRock.GetComponent<Rigidbody2D>();
            rgRock.AddForce(new Vector2(1.0f, 0.0f) * rockBlast.projSpeed);
        }

        //actualRock = Instantiate(rock, player.transform.position, player.transform.rotation);

        
        
    }    

    public void CastChainLightning()
    {
        print("Cast Chain Lightning");
    }

    public void CastScorch()
    {
        print("Cast Scorch");
    }
}
