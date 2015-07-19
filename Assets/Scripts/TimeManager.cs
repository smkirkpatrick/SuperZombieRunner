using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public void ManipulateTime(float newTime, float duration) {

		if (Time.timeScale == 0)
			Time.timeScale = 0.1f;
		// ^ Allows running routines a bit of buffer to complete

		StartCoroutine (FadeTo (newTime, duration));

	}

	IEnumerator FadeTo(float value, float time) {

		for (float t = 0f; t < 1; t += Time.deltaTime / time) {
			// ^ The incremental step calculation is based on the difference of
			//   time from one frame to the next, rather than running the loop so
			//   fast that the transition isn't visible

			Time.timeScale = Mathf.Lerp (Time.timeScale, value, t);

			if (Mathf.Abs (value - Time.timeScale) < 0.01f) {
				Time.timeScale = value;
				// ^ Once Lerp brings us close enough to the target value, we'll
				//   just set the timeScale to the value and exit the loop
				return false;
			}

			yield return null;
			// ^ this yield statement is required to let the loop run over
			//   multiple frames.
		}
	}
}
