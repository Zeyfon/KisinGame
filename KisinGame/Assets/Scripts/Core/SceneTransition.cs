using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    [SerializeField] SpriteRenderer foreground;
    [SerializeField] int mySceneIndex = 0;

    static SceneTransition savedSceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        if(savedSceneTransition == null)
        {
            EnableNewVCamera();
            this.foreground.enabled = false;
            SetCurrentSceneTransitionToThis();
        }
        else
        {
            this.foreground.enabled = true;
        }
    }

    public void CheckForActiveSceneChange()
    {
        if (SceneManager.GetActiveScene().buildIndex == mySceneIndex) return;
        ActiveSceneChange();
    }

    private void ActiveSceneChange()
    {
        CinemachineVirtualCamera currentVCamera = savedSceneTransition.GetComponentInChildren<CinemachineVirtualCamera>();
        DisableCurrentVCamera(currentVCamera);
        EnableNewVCamera();
        UpdateActiveScene();
        PerformForegroundChange();
        SetCurrentSceneTransitionToThis();
    }

    private void PerformForegroundChange()
    {
        this.foreground.enabled = false;
        savedSceneTransition.foreground.enabled = true;
    }

    private void SetCurrentSceneTransitionToThis()
    {
        savedSceneTransition = this;
    }

    private void DisableCurrentVCamera(CinemachineVirtualCamera currentVCamera)
    {
        currentVCamera.Priority = 0;
        currentVCamera.Follow = null;
    }

    private void EnableNewVCamera()
    {
        CinemachineVirtualCamera newVCamera = this.GetComponentInChildren<CinemachineVirtualCamera>();
        newVCamera.Priority = 20;
        newVCamera.Follow = FindObjectOfType<PlayerIdentifer>().transform;
    }

    private void UpdateActiveScene()
    {
        print("New Active Scene is " + SceneManager.GetActiveScene().name);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(mySceneIndex));
    }
}
