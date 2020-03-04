using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsToFollowMe : MonoBehaviour
{
    ElementsFollows[] objectsToFollowMe;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.2f);
        objectsToFollowMe = transform.parent.GetComponentsInChildren<ElementsFollows>();
        foreach(ElementsFollows element in objectsToFollowMe)
        {
            element.StartFollowing(transform);
        }
        yield return null;
    }

}
