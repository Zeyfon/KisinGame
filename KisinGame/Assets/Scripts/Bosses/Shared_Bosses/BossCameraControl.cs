using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossCameraControl : MonoBehaviour
{
    [SerializeField] Camera bossCamera;
    [SerializeField] Transform target;
    [SerializeField] float lerpSpeed = 0.5f;

    Camera mainCamera;

    private void Start()
    {
        GameObject mainCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera = mainCameraGO.GetComponent<Camera>();
        //print(mainCamera.name);
    }

    public void InternalCollision()
    {
        if (!bossCamera.enabled)
        {
            StartCoroutine(InternalChange());
        }
    }

    public void ExternalCollision()
    {

        if (!mainCamera.enabled)
        {
            StartCoroutine(ExternalChange());
        }
    }



    IEnumerator InternalChange()
    {
        bossCamera.transform.position = mainCamera.transform.position;
        print("Internal Change");
        bossCamera.enabled = true;
        bossCamera.gameObject.GetComponent<AudioListener>().enabled = true;
        mainCamera.enabled = false;
        mainCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        float lerp = 0f;
        while (lerp < 1.01)
        {
            yield return new WaitForEndOfFrame();
            bossCamera.transform.position = Vector3.Lerp(bossCamera.transform.position, target.position, lerp);
            lerp += Time.deltaTime * lerpSpeed;
        }
    }

    IEnumerator ExternalChange()
    {
        print("Internal Change");
        mainCamera.transform.position = target.position;
        bossCamera.enabled = false;
        bossCamera.gameObject.GetComponent<AudioListener>().enabled = false;
        mainCamera.enabled = true;
        mainCamera.gameObject.GetComponent<AudioListener>().enabled = true;
        yield return null;
    }
}
