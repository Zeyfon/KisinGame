using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;

public class Credtis : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float titleFadeInTime = 2;
    [SerializeField] float namesFadeInTime = 3;

    [SerializeField] CanvasGroup creatorsGroup =null;
    [SerializeField] CanvasGroup collaboratorsGroup = null;
    [SerializeField] Text creatorsText = null;
    [SerializeField] Text collaboratorsText = null;
    [SerializeField] Text calyStudiosText = null;

    private void Start()
    {
        creatorsGroup.alpha = 0;
        collaboratorsGroup.alpha = 0;
        creatorsText.color = new Color(1, 1, 1, 0);
        collaboratorsText.color = new Color(1, 1, 1, 0);
        calyStudiosText.color = new Color(1, 1, 1, 0);
    }

    public void StartCredits()
    {
        print("Credits Start");
        StartCoroutine(Credits());
    }

    IEnumerator Credits()
    {
        yield return ShowCreatorsText();
        yield return new WaitForSeconds(1.5f);
        yield return ShowCreatorsNames();
        yield return new WaitForSeconds(3);
        yield return ShowCollaboratorsText();
        yield return new WaitForSeconds(1.5f);
        yield return ShowCollaboratorsNames();
        yield return new WaitForSeconds(3);
        yield return ShowCalyStudiosText();
        yield return new WaitForSeconds(10);
        FSMFadeOut();
        yield return new WaitForSeconds(3);
        MusicFadeOut();
        yield return new WaitForSeconds(2);
        DestroyObjects();
        LoadInitialScene();
    }

    IEnumerator ShowCreatorsText()
    {
        float alpha = 0;
        while (creatorsText.color.a != 1)
        {
            alpha += (Time.deltaTime / titleFadeInTime);
            if (alpha >= 1) alpha = 1;
            creatorsText.color = new Color(1, 1, 1, alpha);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ShowCreatorsNames()
    {
        float alpha = 0;
        while (creatorsGroup.alpha != 1)
        {
            alpha += (Time.deltaTime / namesFadeInTime);
            if (alpha >= 1) alpha = 1;
            creatorsGroup.alpha = alpha;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ShowCollaboratorsText()
    {
        float alpha = 0;
        while (collaboratorsText.color.a != 1)
        {
            alpha += (Time.deltaTime / titleFadeInTime);
            if (alpha >= 1) alpha = 1;
            collaboratorsText.color = new Color(1, 1, 1, alpha); ;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ShowCollaboratorsNames()
    {
        float alpha = 0;
        while (collaboratorsGroup.alpha != 1)
        {
            alpha += (Time.deltaTime / namesFadeInTime);
            if (alpha >= 1) alpha = 1;
            collaboratorsGroup.alpha = alpha;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator ShowCalyStudiosText()
    {
        float alpha = 0;
        while (calyStudiosText.color.a != 1)
        {
            alpha += (Time.deltaTime / titleFadeInTime);
            if (alpha >= 1) alpha = 1;
            calyStudiosText.color = new Color(1, 1, 1, alpha); ;
            yield return new WaitForEndOfFrame();
        }
    }

    void FSMFadeOut()
    {
        cameraTransform.GetComponent<PlayMakerFSM>().SendEvent("FadeOut");
    }

    void MusicFadeOut()
    {
        GameObject.FindObjectOfType<MusicManager>().FadeOut();
    }
    void DestroyObjects()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameManager").transform.parent.gameObject);
        Destroy(FindObjectOfType<DialogueSystemController>().gameObject);
    }

    void LoadInitialScene()
    {
        SceneManager.LoadScene(1);
    }
}
