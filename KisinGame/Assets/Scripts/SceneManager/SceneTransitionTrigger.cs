using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] Transform thisSceneVCameraTransform; 
    [SerializeField] int mySceneIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player")) return;
        //print("Current scene " + SceneManager.GetActiveScene().buildIndex + "Wants to change to " + mySceneIndex);
        if (SceneManager.GetActiveScene().buildIndex == mySceneIndex) return;
        PerformCameraChange(collision);
        UpdateActiveScene();

    }

    private void PerformCameraChange(Collider2D collision)
    {
        print("VCameraChanged");
        DisableOtherSceneVCamera(collision);
        EnableThisSceneVCamera();

    }

    private void EnableThisSceneVCamera()
    {
        thisSceneVCameraTransform.GetComponent<VirtualCamera>().EnableVCamera();
    }

    private static void DisableOtherSceneVCamera(Collider2D collision)
    {
        VirtualCamera activeCamera = collision.GetComponent<PlayerIdentifer>().GetVCamera();
        activeCamera.DisableVCamera();
    }

    private void UpdateActiveScene()
    {
        print("New Active Scene is "  + SceneManager.GetActiveScene().name);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(mySceneIndex));
    }
}
