using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreObject : MonoBehaviour
{
    [SerializeField] GameObject persistantObject;
    [SerializeField] GameObject easySaveGameObject;
    [SerializeField] GameObject menus;

    static GameObject coreObject;
    static GameObject menusStatic;
    // Start is called before the first frame update

    IEnumerator Start()
    {

        if (coreObject == null)
        {
            coreObject = this.gameObject;
            Instantiate(persistantObject);

            DontDestroyOnLoad(gameObject);
            if (EasySave.easySave == null)
            {
                Instantiate(easySaveGameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        yield return new WaitForEndOfFrame();
        Instantiate(menus, transform);

    }
}
