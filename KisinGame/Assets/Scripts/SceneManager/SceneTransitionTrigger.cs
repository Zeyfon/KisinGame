using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        transform.parent.GetComponent<SceneTransition>().CheckForActiveSceneChange();
    }
}
