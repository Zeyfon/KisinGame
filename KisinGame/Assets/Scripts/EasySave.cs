using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySave : MonoBehaviour
{
    public static EasySave easySave;
    // Start is called before the first frame update
    void Start()
    {
        if (easySave == null)
        {
            easySave = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
