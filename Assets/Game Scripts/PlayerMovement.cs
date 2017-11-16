using UnityEngine;
using System.Collections;

public class PlayerMovement : Photon.MonoBehaviour
{

    public float rotationSpeed;
    public float movementSpeed;
    GameObject Ball;
    GameObject HeldBall;
    public float horizontalSensitivity;
    public float verticalSensitivity;
    public float horizontalMovement;
    public float verticalMovement;

    private Vector3 localVelocity;

    public float playerJumpImpulse;
    bool isOnGround;

    private PhotonView myPhotonView;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Rigidbody>().freezeRotation = true;
        rotationSpeed = 65;
        movementSpeed = 15;
        playerJumpImpulse = 200;
        isOnGround = true;

        horizontalSensitivity = 1.0f;
        verticalSensitivity = 1.0f;
        localVelocity = new Vector3(0, 0, 0);

        myPhotonView = gameObject.GetComponent<PhotonView>();
        Ball = GameObject.FindGameObjectWithTag("Ball");

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * 1;
        verticalMovement = Input.GetAxis("Vertical") * 1;
        //update the players positions to those connected 
        myPhotonView.RPC("Something", PhotonTargets.All);
    }


    public void UpdatePos(int viewID)
    {
        
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        float horizontal = axisX * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = axisY * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        //only rotate on the X and not the Y
        transform.Rotate(0, horizontalSensitivity * Input.GetAxis("Mouse X"), 0);
        Ball.transform.position = Ball.GetComponent<Transform>().position;

       



    }
    [PunRPC]
    void Something()
    {

        if (myPhotonView.isMine)
        {

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
            {
                isOnGround = false;
                GetComponent<Rigidbody>().AddForce(this.GetComponent<Rigidbody>().transform.up * playerJumpImpulse);
            }
            UpdatePos(0);

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
