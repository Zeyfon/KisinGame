using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrapController : MonoBehaviour
{
    [SerializeField] string keyName = null;
    [SerializeField] Transform trapDoor1Transform = null;
    [SerializeField] Transform trapDoor2Transform = null;

    [SerializeField] AudioClip doorSound = null;

    Door trap1Door;
    Door trap2Door;
    AudioSource audioSource;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.3f);
        trap1Door = trapDoor1Transform.GetComponent<Door>();
        trap2Door = trapDoor2Transform.GetComponent<Door>();
        OpenedDoorState();
        if (ES3.KeyExists(keyName))
        {
            GetComponent<Collider2D>().enabled = false;
            yield break;
        }

    }

    void OpenedDoorState()
    {
        trap1Door.OpenedState();
        trap2Door.OpenedState();
    }

    void CloseDoors()
    {
        audioSource.PlayOneShot(doorSound);
        trap1Door.CloseDoor();
        trap2Door.CloseDoor();
    }

    void OpenDoors()
    {
        audioSource.PlayOneShot(doorSound);
        trap1Door.OpenDoor();
        trap2Door.OpenDoor();
    }

    public void DisableTrap()
    {
        ES3.Save<bool>(keyName, true);
        OpenDoors();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseDoors();
            GetComponent<Collider2D>().enabled = false;
        }
    }


}
