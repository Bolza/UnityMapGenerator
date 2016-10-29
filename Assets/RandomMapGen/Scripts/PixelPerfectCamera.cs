using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {
	public static float pixelToUnits = 1.0f;
	public static float scale = 1.0f;
	public Vector2 nativeResoultion = new Vector2 (160, 144);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Awake () {
		var camera = GetComponent<Camera> ();

		// Based on height of the screen optimal for landascape games
		if (camera.orthographic) {
			var dir = Screen.height;
			var res = nativeResoultion.y;

			scale = dir / res;
			pixelToUnits *= scale;

			camera.orthographicSize = (dir / 2f) / pixelToUnits;  
		}

	}
}
