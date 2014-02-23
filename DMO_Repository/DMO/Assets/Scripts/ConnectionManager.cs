using UnityEngine;
using System.Collections;

public class ConnectionManager : MonoBehaviour
{
	string roomName;
	Vector2 scrollPos;
	string playerName;
	string gameVersion;
	bool connectFailed = false;
	
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		
		playerName = "player" + Random.Range (1, 9999);
		roomName = playerName + "_room";
		gameVersion = "0.0.1";
		
		if (!PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings (gameVersion);
		}
		PhotonNetwork.playerName = playerName;
	}
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI ()
	{
		if (!PhotonNetwork.connected) {
			GUI_Disconnected ();		
		} else {
			if (PhotonNetwork.room != null) {
				GUI_Connected_Room ();
			} else {
				GUI_Connected_Lobby ();
			}            
		}
	}

	void GUI_Disconnected ()
	{
		if (PhotonNetwork.connectionState == ConnectionState.Connecting) {
			GUILayout.Label ("Connecting...");
		} else {
			GUILayout.Label ("Not connected. Check console output. (" + PhotonNetwork.connectionState + ")");
		}
		
		if (this.connectFailed) {
			GUILayout.Label ("Connection failed. Check setup and use Setup Wizard to fix configuration.");
			GUILayout.Label (string.Format ("Server: {0}:{1}", PhotonNetwork.PhotonServerSettings.ServerAddress, PhotonNetwork.PhotonServerSettings.ServerPort));
			GUILayout.Label (string.Format ("AppId: {0}", PhotonNetwork.PhotonServerSettings.AppID));

			if (GUILayout.Button ("Try Again", GUILayout.Width (100))) {
				this.connectFailed = false;
				PhotonNetwork.ConnectUsingSettings ("1.0");
			}
		}
	}
	
	void GUI_Connected_Lobby ()
	{
		GUILayout.BeginArea (new Rect ((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400, 300));

		GUILayout.Label ("Main Menu");

		// Player name
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Player name:", GUILayout.Width (150));
		PhotonNetwork.playerName = GUILayout.TextField (PhotonNetwork.playerName);
		if (GUI.changed) {
			PlayerPrefs.SetString ("playerName" + Application.platform, PhotonNetwork.playerName);
		}

		GUILayout.EndHorizontal ();
		GUILayout.Space (15);


		// Join room by title
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("JOIN ROOM:", GUILayout.Width (150));
		this.roomName = GUILayout.TextField (this.roomName);
		if (GUILayout.Button ("GO")) {
			PhotonNetwork.JoinRoom (this.roomName);
		}
		GUILayout.EndHorizontal ();


		// Create a room (fails if already exists!)
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("CREATE ROOM:", GUILayout.Width (150));
		this.roomName = GUILayout.TextField (this.roomName);
		if (GUILayout.Button ("GO")) {
			PhotonNetwork.CreateRoom (this.roomName, true, true, 10);
		}

		GUILayout.EndHorizontal ();
		//Show a list of all current rooms
		GUILayout.Label ("ROOM LISTING:");
		if (PhotonNetwork.GetRoomList ().Length == 0) {
			GUILayout.Label ("..no games available..");
		} else {
			// Room listing: simply call GetRoomList: no need to fetch/poll whatever!
			this.scrollPos = GUILayout.BeginScrollView (this.scrollPos);
			foreach (RoomInfo game in PhotonNetwork.GetRoomList()) {
				GUILayout.BeginHorizontal ();
				GUILayout.Label (game.name + " " + game.playerCount + "/" + game.maxPlayers);
				if (GUILayout.Button ("JOIN")) {
					PhotonNetwork.JoinRoom (game.name);
				}

				GUILayout.EndHorizontal ();
			}

			GUILayout.EndScrollView ();
		}

		GUILayout.EndArea ();
	}

	void GUI_Connected_Room ()
	{
		GUILayout.Label ("We are connected to room: " + PhotonNetwork.room);
		GUILayout.Label ("Players: ");
		foreach (PhotonPlayer player in PhotonNetwork.playerList) {
			GUILayout.Label ("ID: " + player.ID + " Name: " + player.name);
		}

		if (GUILayout.Button ("Leave room")) {
			PhotonNetwork.LeaveRoom ();
		}
	}

	private void OnJoinedRoom ()
	{
		Debug.Log ("We have joined a room.");
		StartCoroutine (MoveToGameScene ());
	}

	private void OnCreatedRoom ()
	{
		Debug.Log ("We have created a room.");
		//When creating a room, OnJoinedRoom is also called, so we don't have to do anything here.
	}

	private IEnumerator MoveToGameScene ()
	{
		//Wait for the 
		while (PhotonNetwork.room == null) {
			yield return 0;
		}

		Debug.LogWarning ("Normally we would load the game scene right now.");
		/*
            PhotonNetwork.LoadLevel( LEVEL TO LOAD);
         */

	}

}
