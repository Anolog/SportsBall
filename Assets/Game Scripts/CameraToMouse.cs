using UnityEngine;
using System.Collections;

public class CameraToMouse : MonoBehaviour 
{

    float horizontalSpeed;
    float verticalSpeed;

	// Use this for initialization
	void Start () 
	{
        horizontalSpeed = 1000.0f;
        verticalSpeed = 1000.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.Translate(horizontalSpeed * Input.GetAxis("Mouse X"), verticalSpeed * Input.GetAxis("Mouse Y"), 0 );
	}
}
