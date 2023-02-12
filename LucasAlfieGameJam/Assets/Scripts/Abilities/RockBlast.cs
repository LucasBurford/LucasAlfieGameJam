using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlast : Abilities
{
    
    // Start is called before the first frame update
    void Start()
    {
        //Name = "RockBlast";
        //Mana = 2;
        //Cooldown = 30;
        ////Sound;
        ////VFX;
        //Damage = 10;

        //ActiveAbilityList.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void Cast()
    {
        print("Casting RockBlast");

        //Abilities Rockblast = ActiveAbilityList.Find(x => x.Name == "RockBlast");
        
    }
}
