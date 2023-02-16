using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Abilities : ScriptableObject
{
    public string abilityName;
    public float mana;
    public float cooldown;
    //public AudioClip sound;
    //public ParticleSystem VFX;
    public float damage;
    public float timeToDestroy;
    public float projSpeed;
    public float chainDistance;

}
