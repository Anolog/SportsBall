using UnityEngine;
using System.Collections;

public class BallPickup : Photon.MonoBehaviour
{
    GameObject Player;
    public float pickupRange;
    ThrowBallScript ballScript;
    //Vector3 zero;
    // Use this for initialization
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        pickupRange = 5;
    }


    // Update is called once per frame
    void Update()
    {

        //make sure we find the player
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");

        Vector2 playerPos = new Vector2(0, 0);
        if (Player != null)
        { 
            playerPos = new Vector2(Player.transform.position.x, Player.transform.position.z);
        Vector2 ballPos = new Vector2(this.transform.position.x, this.transform.position.z);
        ballScript = Player.GetComponent<ThrowBallScript>();

        Vector2 Difference = new Vector2();

        Difference.x = Mathf.Abs(playerPos.x - ballPos.x);
        Difference.y = Mathf.Abs(playerPos.y - ballPos.y);

        float Distance = Mathf.Sqrt((Difference.x * Difference.x) + (Difference.y * Difference.y));

        //If player is within the range
        if (Distance < pickupRange && Input.GetKeyDown(KeyCode.E) && ballScript.hasBall==false)
        {
            //Disable gravity
            GetComponent<Rigidbody>().useGravity = false;
            //set ball to players rotation
            transform.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
            //teleport ball in front of player
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + GameObject.FindGameObjectWithTag("Player").transform.forward * 2;
            //set the ball as a child of the player
            transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);

            //Send message that player has ball - Change variable

            ballScript.hasBall = true;

        }
    }
	}
}
