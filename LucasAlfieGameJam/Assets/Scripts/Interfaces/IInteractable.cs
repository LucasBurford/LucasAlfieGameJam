using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool Interacted
    {
        get;
        set;
    }

    void OnInteract();
}
