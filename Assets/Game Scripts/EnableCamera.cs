using UnityEngine;
using System.Collections;

public class EnableCamera : MonoBehaviour
{
    private PhotonView myPhotonView;

	// Use this for initialization
	void Start ()
    {
        myPhotonView = gameObject.GetComponent<PhotonView>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.SetActive(false);
        if (myPhotonView.isMine)
        {
            gameObject.SetActive(true);
            //float axisX = Input.GetAxis("Horizontal");
            //float axisY = Input.GetAxis("Vertical");

            //float horizontal = axisX * 1 * Time.deltaTime;
            //gameObject.GetComponent<Transform>().transform.Rotate(0, horizontal, 0);

            //float vertical = axisY * 1 * Time.deltaTime;
            //gameObject.GetComponent<Transform>().transform.Translate(0, 0, vertical);

            ////only rotate on the X and not the Y
            //gameObject.GetComponent<Transform>().transform.Rotate(0, 1 * Input.GetAxis("Mouse X"), 0);



        }


    }
}
