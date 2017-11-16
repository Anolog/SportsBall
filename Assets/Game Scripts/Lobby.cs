using UnityEngine;
using System.Collections;

public class Lobby : MonoBehaviour
{
    GameObject Player;
    GameObject canvas;
    bool click;
    bool buttonClicked = false;
    bool inLobby = false;
    bool inNaming = false;
    private Vector2 scrollPosition;

    //Room info
    private Room[] roomList;
    private string roomName = "Default Name";
    bool connecting = false;
    private int maxPlayer = 2;
    private string maxPlayerString = "2";
    public string version = "0.1";


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
        buttonClicked = true;
        click = true;
    }

    private void OnGUI()
    {
        GUI.color = Color.blue;
        if (buttonClicked == true && inLobby == false && inNaming == false)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Username: ");
            PhotonNetwork.player.NickName = GUILayout.TextField(PhotonNetwork.player.NickName);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Multi Player"))
            {
                inLobby = true;
                //Connect();
                Debug.Log("After Connect");
                inNaming = true;
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        if (inLobby == true)
        {
            Debug.Log("Inside Lobby");
            GUI.Box(new Rect(Screen.width / 2.5f, Screen.height / 3f, 400, 550), "");
            GUI.color = Color.white;
            GUILayout.BeginArea(new Rect(Screen.width / 2.5f, Screen.height / 3, 400, 500));
            GUI.color = Color.cyan;
            GUILayout.Box("Lobby");
            GUI.color = Color.white;

            GUILayout.Label("Session Name:");
            roomName = GUILayout.TextField(roomName);
            GUILayout.Label("Max amount of players 1 - 2:");
            maxPlayerString = GUILayout.TextField(maxPlayerString, 2);
            if (maxPlayerString != "")
            {

                maxPlayer = int.Parse(maxPlayerString);

                if (maxPlayer > 2) maxPlayer = 2;
                if (maxPlayer == 0) maxPlayer = 1;
            }
            else
            {
                maxPlayer = 1;
            }

            if (GUILayout.Button("Create Room "))
            {
                if (roomName != "" && maxPlayer > 0)
                {
                    RoomOptions ro = new RoomOptions();
                    ro.MaxPlayers = 2;
                    PhotonNetwork.CreateRoom(roomName, ro, null);
                    inLobby = false;
                }
            }

            GUILayout.Space(20);
            GUI.color = Color.yellow;
            GUILayout.Box("Sessions Open");
            GUI.color = Color.red;
            GUILayout.Space(20);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true, GUILayout.Width(400), GUILayout.Height(300));


            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                GUI.color = Color.green;
                GUILayout.Box(game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers + " " + game.IsVisible);
                if (GUILayout.Button("Join Session"))
                {
                    PhotonNetwork.JoinRoom(game.Name);
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }


    // Update is called once per frame
    public void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.GetActive() == true)
            canvas.SetActive(false);

        if (buttonClicked == false)
        {
            return;
        }

        if (buttonClicked == true)
        {

        }

    }
}
