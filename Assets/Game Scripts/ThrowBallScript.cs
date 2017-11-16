using UnityEngine;
using System.Collections;

public class ThrowBallScript : MonoBehaviour
{
    //public Transform BallPrefab;
    public bool hasBall;
    public float ballSpeed;

    private float throwForce;
    private float maxForce;
    GameObject ball;

    // Use this for initialization
    void Start()
    {

        hasBall = false;
        ballSpeed = 10.0f;
        throwForce = 10.0f;
        maxForce = 50.0f;
    }
  
    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.Find("Ball"))
         ball = gameObject.transform.Find("Ball").gameObject;
        //keep the ball locked to the player as long as he has the ball
        if (hasBall == true )
        {
            Vector3 finalPos = new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z);
            ball.transform.position = finalPos;
        }


        //begin throw
        if (hasBall == true && Input.GetMouseButton(0))
        {

            //increment throwForce based on time
            throwForce += (throwForce * 0.75f) * Time.deltaTime;

            //if we reached maximum force, throw the ball
            if (throwForce > maxForce)
                throwBall();

        }

        //player released , if he has the ball throw it.
        if (Input.GetMouseButtonUp(0) && hasBall == true)
        {
            throwBall();
        }

    }

    void FixedUpdate()
    {

        if (hasBall == true)
        {

            //set ball to idle physics when picked up
            if (ball.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().useGravity = false;


            }


        }

    }

    void throwBall()
    {

        //turn gravity back on for it
        ball.GetComponent<Rigidbody>().useGravity = true;

        //Throw the fucking ball
        ball.GetComponent<Rigidbody>().velocity = ball.transform.forward * throwForce;

        //unparent
       ball.transform.parent = null;
       
         //player no longer has the ball.
        hasBall = false;

        //reset force
        throwForce = 10.0f;
    }

    void onGUI()
    {
        GUI.Box(new Rect(10, 10, 300, 50), "Throw Strength: " + throwForce);
    }
}
