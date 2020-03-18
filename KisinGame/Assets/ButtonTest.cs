using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        if (!ES3.FileExists("SaveData"))
        {
            gameObject.SetActive(false);
            yield break;
        }
        
    }

    public void LoadLastSavedScene()
    {
        int lastSavedScene = 0;
        lastSavedScene = (int)ES3.Load<object>("lastSavedScene");
        SceneManager.LoadScene(lastSavedScene);
    }
}
