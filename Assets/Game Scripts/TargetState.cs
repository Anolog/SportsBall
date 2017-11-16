using UnityEngine;
using System.Collections;

public class TargetState : MonoBehaviour
{
    enum TargetStates
    {
        STATE_MOVING,
        STATE_HIT
    };

    public System.Collections.Generic.List<Vector3> movementPoints = new System.Collections.Generic.List<Vector3>();
    public float movementSpeed;
    public bool isHit;
    private float totalTime;
    public int movementPointsStart;
    public float invincTimer;

    private Vector3 startLerp;
    private Vector3 endLerp;
    private float lerpLength;

    private TargetStates currentState;

    //Enum states
    //If state == this, i.e., MoveState
    //Have generic movestate using the index


    // Use this for initialization
    void Start ()
    {
        currentState = TargetStates.STATE_MOVING;
        
        Vector3 pointOne = new Vector3(-6.358936f, 4.66f, -21.06f);
        Vector3 pointTwo = new Vector3(-6.358936f, 4.66f, -16);
        Vector3 pointThree = new Vector3(2.41f, 4.66f, -16);
        Vector3 pointFour = new Vector3(2.41f, 4.66f, -21.06f);

        movementPoints.Add(pointOne);
        movementPoints.Add(pointTwo);
        movementPoints.Add(pointThree);
        movementPoints.Add(pointFour);

        

        isHit = false;
        totalTime = 0;
        movementPointsStart = 0;

        invincTimer = 0;

        startLerp = new Vector3();
        endLerp = new Vector3();
    }
	
	// Update is called once per frame
	void Update ()
    {
        totalTime += Time.deltaTime;

        if (currentState == TargetStates.STATE_MOVING)
        {
            MoveState();
        }

        else if (currentState == TargetStates.STATE_HIT)
        {
            invincTimer += Time.deltaTime;
            HitState();
        }
	}

    /* cool draw stuff that shows points before the object is there
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(movementPoints[0], Vector3.one);
    }
    */
    void MoveState()
    {
        //If at the next point then reset the total time.
        //Check indexing so it doesn't go out of bounds

        //Issue relies in here
        if (transform.position == movementPoints[movementPointsStart + 1 ] || transform.position == movementPoints[0] && movementPointsStart == 3)
        {
            movementPointsStart++;
            return;
        }

        if (movementPointsStart !=3)
        {
            startLerp = new Vector3(movementPoints[movementPointsStart].x, movementPoints[movementPointsStart].y, movementPoints[movementPointsStart].z);
            endLerp = new Vector3(movementPoints[movementPointsStart + 1].x, movementPoints[movementPointsStart + 1].y, movementPoints[movementPointsStart + 1].z);
            lerpLength = Vector3.Distance(startLerp, endLerp);

            float distCovered = totalTime * movementSpeed;
            float fracJourney = distCovered / lerpLength;
            transform.position = Vector3.Lerp(startLerp, endLerp, fracJourney);

            if (this.transform.position == endLerp)
            {
                totalTime = 0;
            }
        }
        
        else if (movementPointsStart + 1 == 4)
        {
            startLerp = new Vector3(movementPoints[movementPointsStart].x, movementPoints[movementPointsStart].y, movementPoints[movementPointsStart].z);
            endLerp = new Vector3(movementPoints[0].x, movementPoints[0].y, movementPoints[0].z);
            lerpLength = Vector3.Distance(startLerp, endLerp);

            float distCovered = totalTime * movementSpeed;
            float fracJourney = distCovered / lerpLength;
            transform.position = Vector3.Lerp(startLerp, endLerp, fracJourney);

            if (this.transform.position == endLerp)
            {
                totalTime = 0;
                movementPointsStart = 0;
            }

            //movementPointsStart = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            currentState = TargetStates.STATE_HIT;
        }
    }

    void HitState()
    {
        this.GetComponent<Rigidbody>().isKinematic = false;
        if(invincTimer >= 5.0f)
        {
            invincTimer = 0.0f;
            this.GetComponent<Rigidbody>().isKinematic = true;
            currentState = TargetStates.STATE_MOVING;
        }
    }
}
