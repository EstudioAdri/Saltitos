using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    GameObject player;
    private bool freeCamera;
    public float zVariation;
    public float xVariation;
    public int Boundary = 50;
    public float speed = 5;
    int ScreenWidth;
    int ScreenHeight;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        freeCamera = true;
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        camera = Camera.main;
        player = FindObjectOfType<Player>().gameObject;
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
            Vector3 temp = player.transform.position;
            temp.y = this.transform.position.y;
            temp.z -= zVariation;
            temp.x -= xVariation;
            this.transform.position = temp;
        }
        else
        {
            Vector3 panningY = this.transform.forward;
            Vector3 panningZ = this.transform.up;
            Vector3 panningX = this.transform.right;
            if (Input.mousePosition.x >= ScreenWidth - Boundary)
            {
                this.transform.position += panningX * Time.deltaTime * speed;

            }

            if (Input.mousePosition.x <= 0 + Boundary)
            {
                this.transform.position -= panningX * Time.deltaTime * speed;

            }

            if (Input.mousePosition.y >= ScreenHeight - Boundary)
            {
                this.transform.position += panningY * Time.deltaTime * speed;
                this.transform.position += panningZ * Time.deltaTime * speed;
            }

            if (Input.mousePosition.y <= 0 + Boundary)
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

    void MoveCameraWithMouse()
    {

    }



}
