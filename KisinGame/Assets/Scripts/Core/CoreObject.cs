using UnityEngine;

public class CoreObject : MonoBehaviour
{
    [SerializeField] GameObject persistantObject;
    static GameObject coreObject;
    // Start is called before the first frame update
    void Start()
    {
        if (coreObject == null)
        {
            coreObject = this.gameObject;
            GameObject persistantObjectClone = Instantiate(persistantObject);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
