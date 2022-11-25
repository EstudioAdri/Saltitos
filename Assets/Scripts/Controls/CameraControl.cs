using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraControl : MonoBehaviour
{
    GameObject player;
    private bool freeCamera;
    [SerializeField] int moveCameraWithMouseBoundaries = 50;
    [SerializeField] float smoothingBoundaryX;
    [SerializeField] float smoothingBoundaryY;
    [SerializeField] float speed = 5;
    [SerializeField] float speedVariationX;
    [SerializeField] float speedVariationY;
    int ScreenWidth;
    int ScreenHeight;
    Camera mainCamera;   
    Vector3 panningY;
    Vector3 panningZ;
    Vector3 panningX;
    // Start is called before the first frame update
    void Start()
    {
        freeCamera = true;
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        mainCamera = Camera.main;

        player = FindObjectOfType<Player>().gameObject;

        panningX = this.transform.right;
        panningY = this.transform.forward;
        panningZ = this.transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>().gameObject;
        }
        if (!freeCamera)
        {            
            FixedCamera();
        }
        else
        {
            
            if (Input.mousePosition.x >= ScreenWidth - moveCameraWithMouseBoundaries)
            {
                this.transform.position += panningX * Time.deltaTime * speed;

            }

            if (Input.mousePosition.x <= 0 + moveCameraWithMouseBoundaries)
            {
                this.transform.position -= panningX * Time.deltaTime * speed;

            }

            if (Input.mousePosition.y >= ScreenHeight - moveCameraWithMouseBoundaries)
            {
                this.transform.position += panningY * Time.deltaTime * speed;
                this.transform.position += panningZ * Time.deltaTime * speed;
            }

            if (Input.mousePosition.y <= 0 + moveCameraWithMouseBoundaries)
            {
                this.transform.position -= panningY * Time.deltaTime * speed;
                this.transform.position -= panningZ * Time.deltaTime * speed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (freeCamera)
            {
                freeCamera = false;            
            }
            else
            {
                freeCamera = true;
            }
        }
        
    }

    void FixedCamera()
    {
        
        Vector3 relativePosition = mainCamera.transform.InverseTransformPoint(player.transform.position);
        Vector3 cameraPosition = this.transform.position;   

        if (relativePosition.x > smoothingBoundaryX)
        {
            cameraPosition += panningX * Time.deltaTime * (player.GetComponent<NavMeshAgent>().speed - speedVariationX);
        }
        else if (relativePosition.x < -smoothingBoundaryX)
        {
            cameraPosition -= panningX * Time.deltaTime * (player.GetComponent<NavMeshAgent>().speed - speedVariationX);
        }
        if (relativePosition.y > smoothingBoundaryY)
        {
            cameraPosition += panningY * Time.deltaTime * (player.GetComponent<NavMeshAgent>().speed - speedVariationY);
            cameraPosition += panningZ * Time.deltaTime * (player.GetComponent<NavMeshAgent>().speed - speedVariationY);
        }
        else if (relativePosition.y < -smoothingBoundaryY)
        {
            cameraPosition -= panningY * Time.deltaTime * (player.GetComponent<NavMeshAgent>().speed - speedVariationY);
            cameraPosition -= panningZ * Time.deltaTime * (player.GetComponent<NavMeshAgent>().speed - speedVariationY);
        }
        

        this.transform.position = cameraPosition;       
        
    }



}
