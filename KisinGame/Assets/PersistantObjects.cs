using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjects : MonoBehaviour
{
    static PersistantObjects persistantObjects;

    private void Awake()
    {
        if (persistantObjects == null)
        {
            persistantObjects = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
