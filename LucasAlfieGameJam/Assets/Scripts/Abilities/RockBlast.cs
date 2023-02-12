using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RockBlast : Abilities
{
    public float maxDistance;
    public float projSpeed;

    //public GameObject rock;

    public override void Cast(GameObject user)
    {
        //MonoBehaviour.print("Casting RockBlast");

        //Abilities Rockblast = ActiveAbilityList.Find(x => x.Name == "RockBlast");

        Debug.Log("Rock Blast");

        user.GetComponent<AbilityController>().GenerateRockBlastProjectile(projSpeed);
       
    }
}
