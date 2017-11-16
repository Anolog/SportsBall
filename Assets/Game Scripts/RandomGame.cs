﻿using UnityEngine;
using System.Collections;

public class RandomGame : MonoBehaviour
{
    bool click;
    GameObject Player;
    GameObject canvas;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void OnMouseDown()
    {
        PhotonNetwork.JoinRandomRoom();
        click = true;
    }


    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetActive() == true)
            canvas.SetActive(false);
    }
}
