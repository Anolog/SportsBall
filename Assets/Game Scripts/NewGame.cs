using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour
{
    GameObject canvas;
    GameObject Player;
    bool click;
    // Use this for initialization
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnMouseDown()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom("ServerName", roomOptions, null);
        click = true;


    }

   void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetActive() == true)
            canvas.SetActive(false);

    }
}
