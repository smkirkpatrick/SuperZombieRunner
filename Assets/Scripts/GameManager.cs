﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Text continueText;

	private float blinkTime = 0f;
	private float blink;
	private bool gameStarted;
	private TimeManager timeManager;
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;

	void Awake() {
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		timeManager = GetComponent<TimeManager> ();
	}

	// Use this for initialization
	void Start () {
		var floorHeight = floor.transform.localScale.y;
		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);
		floor.transform.position = pos;

		spawner.active = false;

		Time.timeScale = 0; // Tells the game to wait for a button press to start
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted && Time.timeScale == 0) {
			if (Input.anyKeyDown) {
				timeManager.ManipulateTime(1, 1f);
				ResetGame ();
			}
		}
	}

	void OnPlayerKilled() {
		spawner.active = false;
		
		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled; 
		// ^ Be sure to remove the delegate reference or the garbage collector can't
		//   clean up the object because of the outstanding reference.

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		timeManager.ManipulateTime (0, 5.5f);

		gameStarted = false;
	}

	void ResetGame() {
		spawner.active = true;

		player = GameObjectUtil.Instantiate (playerPrefab, new Vector3 (0, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 + 100, 0));

		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;

		gameStarted = true;
	}
}
