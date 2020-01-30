using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetActiveSceneAction: MonoBehaviour
{
    int sceneToActivate;

    //Method Called inside GameManager GameManager FSM
    public void SetActiveScene(int sceneToActivate)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToActivate));
    }
}
