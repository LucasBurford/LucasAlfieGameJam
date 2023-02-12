using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public string Name;
    public uint Mana;
    public uint Cooldown;
    public  AudioClip Sound;
    public ParticleSystem VFX;
    public uint Damage;

    static public List<Abilities> ActiveAbilityList = new List<Abilities>();

    // Start is called before the first frame update
    void Start()
    {
        ActiveAbilityList.Add(CreateActive("RockBlast", 30, 2, 10));
        ActiveAbilityList.Add(CreateActive("ChainLightning", 10, 1, 20));
        ActiveAbilityList.Add(CreateActive("FireBreath", 10, 2, 5));
    }

    // Update is called once per frame
    void Update()
    {
        //print(ActiveAbilityList[0].Name);

        Abilities Rockblast = ActiveAbilityList.Find(x => x.Name == "RockBlast");
        print(Rockblast.Mana);
        print(Rockblast.Cooldown);
        RockBlast.Cast();
    }

    Abilities CreateActive(string name, uint mana, uint cooldown, uint damage)
    {
        Name = name;
        Mana = mana;
        Cooldown = cooldown;
        //Sound = sound;
        //VFX = vfx;
        Damage = damage;

        return this;
    }
}
