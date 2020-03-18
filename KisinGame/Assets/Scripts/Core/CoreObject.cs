using UnityEngine;

public class CoreObject : MonoBehaviour
{
    [SerializeField] GameObject persistantObject;
    [SerializeField] GameObject easySaveGameObject;
    static GameObject coreObject;
    // Start is called before the first frame update
    void Start()
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

    }
}
