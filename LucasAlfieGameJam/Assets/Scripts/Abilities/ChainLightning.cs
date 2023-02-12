using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChainLightning : Abilities
{
    public float chainDistance;

    public override void Cast(GameObject user)
    {
        //MonoBehaviour.print("Casting ChainLightning");

        //Abilities ChainLightning = ActiveAbilityList.Find(x => x.Name == "ChainLightning");

        Debug.Log("Chain Lightning");

    }
}
