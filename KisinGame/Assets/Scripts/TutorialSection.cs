using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSection : MonoBehaviour
{
    [SerializeField] float fadeInTime = 0.5f;
    [SerializeField] float fadeOutTime = 1f;

    Image image = null;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        image = transform.GetChild(0).GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Fading Image In");
            StartCoroutine(FadingInImage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Fading Image Out");
            StartCoroutine(FadingOutImage());
        }
    }

    IEnumerator FadingInImage()
    {
        float alpha = 0;
        while(alpha < 1)
        {
            alpha += Time.deltaTime / fadeInTime;
            print(alpha);

            if (alpha > 1)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                yield break;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadingOutImage()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeOutTime;
            print(alpha);
            if (alpha < 0)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                yield break;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}
