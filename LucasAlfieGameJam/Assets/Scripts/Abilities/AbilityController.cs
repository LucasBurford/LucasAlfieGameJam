using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    static public List<Abilities> abilities = new();
    float cooldowns;

    public GameObject rock;

    // Start is called before the first frame update
    void Start()
    {
        abilities.Add(ScriptableObject.CreateInstance<RockBlast>());
        abilities.Add(ScriptableObject.CreateInstance<ChainLightning>());
        abilities.Add(ScriptableObject.CreateInstance<Scorch>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRockBlastProjectile(float projSpeed) // Called here as a Scriptable Object Class cannot instantiate an object due to it not existing in the scene;
    {
        GameObject player = this.gameObject;

        Debug.Log(rock);

        GameObject actualRock;
        actualRock = Instantiate(rock, player.transform.position, player.transform.rotation);

        Rigidbody2D rgRock;
        rgRock = actualRock.GetComponent<Rigidbody2D>(); 

        rgRock.AddForce(player.transform.forward * projSpeed); 
        
    }    

    public void GenerateChainLightning()
    {

    }

    public void GenerateScorch()
    {

    }
}
