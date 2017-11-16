using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour
{

    public float playerJumpImpulse = 100;
    bool isOnGround = true;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            GetComponent<Rigidbody>().AddForce(0, playerJumpImpulse, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isOnGround = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isOnGround = false;
        }
    }
}
