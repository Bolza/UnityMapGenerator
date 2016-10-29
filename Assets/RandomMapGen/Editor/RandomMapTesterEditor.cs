﻿using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RandomMapTester))]
public class RandomMapTesterEditor : Editor {

	// Use this for initialization
	public override void OnInspectorGUI () {
		DrawDefaultInspector ();
		var script = (RandomMapTester)target;
		if (GUILayout.Button ("Generate Island")) {
			if (Application.isPlaying) {
				script.MakeMap ();
			}
		}
	}
}
