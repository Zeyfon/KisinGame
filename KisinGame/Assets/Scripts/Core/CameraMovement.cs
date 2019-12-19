using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement: MonoBehaviour {
    private GameObject player;
    private Transform cameraTransform;
    private Transform playerTransform;
    private CameraEdges cameraEdges;

    public GameObject thisSceneManager;

    //Locations for the Player and Camera position every frame.
    private float playerPosX;
    private float playerPosY;
    private float cameraPosX;
    private float cameraPosY;
    private float cameraPosZ;

    //Locations the camera use after camera restrictions check
    private float camPosXAfterRes;
    private float camPosYAfterRes;

    //Location for Camera edges Resctrictions
    private float rightCamRes;
    private float leftCamRes;
    private float upCamRes;
    private float downCamRes;

    //Values for the Camera (Lerp Movement and Offset in Y Axis between Player and Camera)
    public float yOffset;
    public float lerpX = 3.0f;
    public float lerpY = 3.0f;

    private bool cameraIsSet = false;
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        if (!player)
        {
            Debug.LogWarning("No Player Found");
        }
        playerTransform = player.GetComponent<Transform>();
        cameraTransform = GetComponent<Transform>();
            if (!thisSceneManager)
            {
                Debug.LogWarning("No This Scene Manager yet");
            }
        GetCameraEdges();
        Initial_Camera_Position();
    }

    public void Initial_Camera_Position()
    {
        GetCameraEdges();
        //Debug.Log("Connection Works");
        GetPlayerPosition(); //Camera movement is determined by the players position
        AdjustCamera(); //If the camera goes to the edges needs to stop there
        CameraPositioning(); //Apply the necessary position of the camera depending on the previous methods

        cameraIsSet = true;

    }
    private void Update()
    {

    }

    void LateUpdate ()
    {



        if (cameraIsSet == true)
        {
            CameraRepositioning();
        }

    }

    void CameraRepositioning()
    {
        GetPlayerPosition(); //Camera movement is determined by the players position
        AdjustCamera(); //If the camera goes to the edges needs to stop there
        CameraMovementM(); //Apply the necessary position of the camera depending on the previous methods
    }

    void GetCameraEdges()
    {
        cameraEdges = thisSceneManager.GetComponent<CameraEdges>();
        cameraEdges.UpdateCameraEdges();
    }

    void GetPlayerPosition()
    {
        playerPosX = playerTransform.position.x;
        playerPosY = playerTransform.position.y;

    }

    void AdjustCamera()
    {
        //ForX
        if (playerPosX <= leftCamRes)
        {
            camPosXAfterRes = leftCamRes;
            //Debug.Log("X Section 1");
        }
        if (playerPosX >= rightCamRes)
        {
            camPosXAfterRes = rightCamRes;
            //Debug.Log("X Section 3");
        }
        else if (leftCamRes <= playerPosX && playerPosX <= rightCamRes)
        {
            camPosXAfterRes = playerPosX;
            //Debug.Log("X Section 2");
        }

        //ForY
        if ((playerPosY + yOffset) <= downCamRes)
        {
            camPosYAfterRes = downCamRes;
            //Debug.Log("Y Section 1");
        }
        if ((playerPosY + yOffset) >= upCamRes)
        {
            camPosYAfterRes = upCamRes;
            //Debug.Log("Y Section 3");
        }
        else if (downCamRes <= (playerPosY + yOffset) && (playerPosY + yOffset) <= upCamRes)
        {
            camPosYAfterRes = playerPosY + yOffset;
            //Debug.Log("Y Section 2");
        }
        cameraPosX = cameraTransform.position.x;
        cameraPosY = cameraTransform.position.y;
        cameraPosZ = cameraTransform.position.z;

    }

    void CameraPositioning() {
        cameraTransform.position = new Vector3(camPosXAfterRes, camPosYAfterRes, cameraPosZ);
    }

    void CameraMovementM()
    {
        if (rightCamRes == leftCamRes)
        {
            //Debug.Log("In Tunnel");
            GroundMovement();
        }
        else
        {
            //Debug.Log("Not In Tunnel");
            TransitionMovement();
        }
    }

    //Movement of the camera when the player is in a Ground Floor. No movement in the Y axis.
    void GroundMovement()
    {
        if (cameraPosY <= (downCamRes  - 0.01) || cameraPosY >= (downCamRes + 0.01))
        {
           // Debug.Log("Lerping");
            cameraTransform.position = new Vector3(Mathf.Lerp(cameraPosX, camPosXAfterRes, lerpX * Time.deltaTime), Mathf.Lerp(cameraPosY, camPosYAfterRes, lerpY * Time.deltaTime), cameraPosZ);
            //Debug.Log(cameraTransform.position);
        }
        else
        {
           // Debug.Log("Not Lerping");
            cameraTransform.position = new Vector3(Mathf.Lerp(cameraPosX, camPosXAfterRes, lerpX * Time.deltaTime), downCamRes, cameraPosZ);
        }
    }

    //Movement of the camera when the player is in a Transition Floor. Movement in the Y axis
    void TransitionMovement()
    {
        //Debug.Log("Free Movement");
        cameraTransform.position = new Vector3(Mathf.Lerp(cameraPosX, camPosXAfterRes, lerpX * Time.deltaTime), Mathf.Lerp(cameraPosY, camPosYAfterRes, lerpY*Time.deltaTime), cameraPosZ);
        //Debug.Log(cameraTransform.position);
    }

    public void SetCameraEdges( )
    {

        cameraEdges = thisSceneManager.GetComponent<CameraEdges>();
        cameraEdges.UpdateCameraEdges();
        Debug.Log(thisSceneManager);
        Debug.Break();
    }
    public void UpdateCameraEdges(float newRight, float newLeft, float newUpper, float newLower)
    {
        rightCamRes = newRight;
        leftCamRes = newLeft;
        upCamRes = newUpper;
        downCamRes = newLower;
        Debug.Log(rightCamRes + "  " + leftCamRes + "  "+ upCamRes + "  " + downCamRes+ "  ");
        Debug.Break();
    }
}
