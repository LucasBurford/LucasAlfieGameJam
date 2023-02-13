using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public Sprite sprite;

    public bool interacted;

    public bool Interacted { get { return interacted; } set { interacted = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        FindObjectOfType<PlayerControl>().AddGold(50);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!interacted)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<PlayerControl>().HandleInteractable(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!interacted)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                FindObjectOfType<PlayerControl>().RemoveInteractText();
            }
        }
    }
}
