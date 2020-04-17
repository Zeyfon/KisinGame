using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.EventSystems;
using System;

public class DoubleJumpEvent : MonoBehaviour
{
    [SerializeField] float waitingTime = 5;
    private void Start()
    {
        DisableChildren();

    }

    public void DoubleJumpSkillAcquiringEvent()
    {
        print("doublejump triggered");
        GetComponent<AudioSource>().Play();
        StartCoroutine(EnableDoubleJumpEventAssets());
        GetComponent<PlayMakerFSM>().SendEvent("UnlockDoubleJumpInPlayerActions");
    }

    IEnumerator EnableDoubleJumpEventAssets()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        transform.GetChild(2).gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(transform.GetChild(2).gameObject);
    }

    public void EndDoubleJumpEvent()
    {
        DisableChildren();
        StartCoroutine(EnablePlayerController());
    }

    private void DisableChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    IEnumerator EnablePlayerController()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<DialogueSystemController>().GetComponent<PlayMakerFSM>().SendEvent("EnablePlayerController");

    }
}
