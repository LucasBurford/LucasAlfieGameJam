using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Scorch : Abilities
{
     
  
    public override void Cast(GameObject user)
    {
        //MonoBehaviour.print("Scorch");

        //Abilities Scorch = ActiveAbilityList.Find(x => x.Name == "Scorch");

        Debug.Log("Scorch");

    }
}
