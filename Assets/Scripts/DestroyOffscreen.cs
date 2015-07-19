using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {

	public float offset = 16f; // Overridden by the Object that the script is attached to.
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;

	private bool offscreen;
	private float offscreenX = 0;
	private Rigidbody2D body2d;

	void Awake() {
		body2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		//Debug.LogFormat ("Screen width: {0}, pixelToUnits: {1}, offset: {2}", Screen.width, PixelPerfectCamera.pixelsToUnits, offset);
		offscreenX = ((Screen.width / PixelPerfectCamera.pixelsToUnits) / 2) + offset;
	}
	
	// Update is called once per frame
	void Update () {
		var posX = transform.position.x;
		var dirX = body2d.velocity.x;

		if (Mathf.Abs (posX) > offscreenX) {

			if (dirX < 0 && posX < -offscreenX) {
				//Debug.LogFormat ("Moving left, posX: {0} vs offscreenX: {1}", posX, -offscreenX);
				offscreen = true;
			} else if (dirX > 0 && posX > offscreenX) {
				//Debug.LogFormat ("Moving right, posX: {0} vs offscreenX: {1}", posX, offscreenX);
				offscreen = true;
			}
		} else {
			offscreen = false;
		}

		if (offscreen) {
			//Debug.Log ("Destroying offscreen object");
			OnOutOfBounds();
		}
	}

	public void OnOutOfBounds() {
		offscreen = false;
		GameObjectUtil.Destroy (gameObject);

		if (DestroyCallback != null) {
			DestroyCallback();
		}
	}
}
