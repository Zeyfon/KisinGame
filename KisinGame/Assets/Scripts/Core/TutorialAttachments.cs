using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;

public class TutorialAttachments : MonoBehaviour
{
    [SerializeField] List<Sprite> tutorialImages = new List<Sprite>();
    [SerializeField] Image image;

    int i = 0;

    public void ChangeImage()
    {
        i++;
        if (i == 1) return;
        print(gameObject.name);
        image.sprite = tutorialImages[i-2];
        if (!image.enabled) image.enabled = true;
    }
    public void DisableImage()
    {
        image.enabled = false;
    }
}
