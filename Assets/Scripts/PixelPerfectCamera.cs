using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public static float pixelsToUnits = 1f;
	public static float scale = 1f;

	public Vector2 nativeResolution = new Vector2 (240, 160);

	// Use this to run logic before Start()
	void Awake () {
		var camera = GetComponent<Camera> ();

		if (camera.orthographic) {
			scale = Screen.height / nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = Mathf.Ceil ((Screen.height / 2.0f) / pixelsToUnits);
			Debug.Log (string.Format ("Screen height: {0}, scale: {1}, pixelsToUnits: {2}, camera scaled: {3}",
			                          Screen.height, scale, pixelsToUnits, camera.orthographicSize));
		}
	}
}
