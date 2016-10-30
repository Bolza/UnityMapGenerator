using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public float speed = 6f;
	private Vector3 startPosition;
	bool moving;

	// Update is called once per frame
	void FixedUpdate () {
		ClickAndDragCamera ();
	}

	void ClickAndDragCamera() {
		if (Input.GetMouseButtonDown (0)) {
			startPosition = Input.mousePosition;
			moving = true;
		}
		if (Input.GetMouseButtonUp (0) && moving) {
			moving = false;
		}
		if (moving) {
			Vector3 position = Camera.main.ScreenToViewportPoint (Input.mousePosition - startPosition);
			Debug.Log ("In px " + (Input.mousePosition - startPosition));
			Debug.Log ("In WU " + position);
			Vector3 move = new Vector3 (position.x * speed, position.y * speed, 0);
			transform.Translate (move, Space.Self);
		}
	}
}
