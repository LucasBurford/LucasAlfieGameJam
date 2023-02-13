using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IDamageable
{
    public int maxGold;

    public void OnHit()
    {
        FindObjectOfType<PlayerControl>().AddGold(Random.Range(0, maxGold));
        Destroy(gameObject);
    }
}
