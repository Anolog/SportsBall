using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    //Menu icon GO
    public GameObject gameMenu;
    public GameObject createNewGameMenu;
    public GameObject createRandomGameMenu;

    //Game spawn
    public GameObject[] spawnPoints;

	// Use this for initialization
	void Start ()
    {

        PhotonNetwork.player.NickName = "DefaultName";

	    if (spawnPoints.Length == 0)
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        }
        
	}
	
    void OnJoinedLobby()
    {
        this.createRandomGameMenu.SetActive(true);
        this.createNewGameMenu.SetActive(true);
        this.gameMenu.SetActive(false);
    }

    void OnJoinedRoom()
    {
        this.createRandomGameMenu.SetActive(false);
        this.createNewGameMenu.SetActive(false);
        this.gameMenu.SetActive(true);

        int spawn = (Random.Range(0, spawnPoints.Length));
        GameObject NewPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/PlayerPrefab", spawnPoints[spawn].transform.position, Quaternion.identity, 0);
        GameObject.FindGameObjectWithTag("PlayerCamera").SetActive(true);

    }

	// Update is called once per frame
	void Update ()
    {
	    
	}
}
