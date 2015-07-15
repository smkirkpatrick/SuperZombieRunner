using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

	private Animator animator;
	private InputState inputState;

	void Awake() {
		animator = GetComponent<Animator> ();
		inputState = GetComponent<InputState> ();
	}

	// Update is called once per frame
	void Update () {

		var running = true;
		// ^ Default state of the player

		if (inputState.absVelocityX > 0 && inputState.absVelocityY < inputState.standingThreshold) {
			running = false;
		}

		animator.SetBool ("Running", running);
	}
}
