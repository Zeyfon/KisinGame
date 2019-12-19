using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdges : MonoBehaviour {
    private GameObject go_camEdgeLL;
    private GameObject go_camEdgeUR;

    private GameObject mainCamera;
    private CameraMovement cameraMovement;
    private Transform transform_camEdgeLL;
    private Transform transform_camEdgeUR;
    public int fmsVariable;
    private float right;
    private float left;
    private float upper;
    private float lower;
    // Use this for initialization
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraMovement = mainCamera.GetComponent<CameraMovement>();

        if (!cameraMovement.thisSceneManager)
        {
            cameraMovement.thisSceneManager = gameObject;
        }
    }
    void Start()
    {
        FindCamEdgeGameObjects();


        transform_camEdgeLL = go_camEdgeLL.GetComponent<Transform>();
        transform_camEdgeUR = go_camEdgeUR.GetComponent<Transform>();

        right = transform_camEdgeUR.position.x;
        left = transform_camEdgeLL.position.x;
        upper = transform_camEdgeUR.position.y;
        lower = transform_camEdgeLL.position.y;

        Debug.Log("right " + right + " left " + left + " upper " + upper + " lower " + lower);
        Debug.Log(gameObject);
    }
     
    void FindCamEdgeGameObjects()
    {
        for(int i = 0; i<2; i++)
        {
            Transform transform = gameObject.transform.GetChild(i);
            if (i == 0)
            {
                go_camEdgeUR = transform.gameObject;
            }
            else if (i == 1)
            {
                go_camEdgeLL = transform.gameObject;
            }
        }
    }

    public void UpdateCameraEdges()
    {
        //Debug.Log("Edges " + right  + "right" + " left " + left + " upper " + upper + " lower " + lower);
        cameraMovement.UpdateCameraEdges(right, left, upper, lower);
    }


        //cameraMovement.CameraEdgeRestrictions(right, left, upper, lower);
       // Debug.Log(cameraMovement.gameObject);
        //Debug.Log("right " + right + " left " + left + " upper " + upper + " lower " + lower);
       // Debug.Break();
}
