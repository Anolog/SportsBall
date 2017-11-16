using UnityEngine;
using System.Collections;

public class ApplyGravityBoost : MonoBehaviour
{
    public float gravityBoostForce;
    Vector3 boostVelocity;
    float timeCounter;

    //GameObject Player;

	// Use this for initialization
	void Start ()
    {
        gravityBoostForce = 500.0f;
        timeCounter = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ball"))
        {
            //other.gameObject.GetComponent<Rigidbody>().velocity = boostVelocity;

            Debug.Log("Inside if statement\n");

            timeCounter += Time.deltaTime;

            if (timeCounter < 5.0f)
            {
                Debug.Log("Boosting\n");
                boostVelocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().velocity.x,
                            other.gameObject.GetComponent<Rigidbody>().velocity.y + gravityBoostForce,
                            other.gameObject.GetComponent<Rigidbody>().velocity.z);

                timeCounter = 0;
                other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.GetComponent<Rigidbody>().transform.up * gravityBoostForce);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ball"))
        {
            timeCounter = 0;
        }
    }
}
