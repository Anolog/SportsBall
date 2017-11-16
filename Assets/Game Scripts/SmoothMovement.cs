using UnityEngine;
using System.Collections;
using Photon;

public class SmoothMovement : PunBehaviour
{

    void Awake()
    {
        gameObject.name = gameObject.name + photonView.viewID;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(GetComponent<Rigidbody>().velocity);
        }

        else
        {
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();
        }
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
