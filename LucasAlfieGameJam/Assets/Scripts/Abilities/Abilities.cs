using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : ScriptableObject
{
    public string abilityName;
    public float mana;
    public float cooldown;
    //public AudioClip sound;
    //public ParticleSystem VFX;
    public float damage;
    private bool offCooldown;

    public abstract void Cast(GameObject user);



}
