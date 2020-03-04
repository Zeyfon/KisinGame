using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.EventSystems;
using System;

public class DoubleJumpEvent : MonoBehaviour
{
    [SerializeField] float waitingTime = 5;
    [SerializeField] Transform imageTransform;
    [SerializeField] Transform buttonTransform;

    public void DoubleJumpSkillAcquiringEvent()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(EnableDoubleJumpEventAssets());
    }

    IEnumerator EnableDoubleJumpEventAssets()
    {
        yield return new WaitForSeconds(0.5f);
        imageTransform.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitingTime);
        buttonTransform.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttonTransform.gameObject);
    }

    public void EndDoubleJumpEvent()
    {
        imageTransform.gameObject.SetActive(false);
        buttonTransform.gameObject.SetActive(false);
        StartCoroutine(EnablePlayerController());
    }

    IEnumerator EnablePlayerController()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<DialogueSystemController>().GetComponent<PlayMakerFSM>().SendEvent("EnablePlayerController");

    }
}
