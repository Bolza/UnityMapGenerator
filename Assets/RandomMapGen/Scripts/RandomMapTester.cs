﻿using UnityEngine;
using System.Collections;

public class RandomMapTester : MonoBehaviour {
	[Header("Map Dimensions")]
	public int mapWidth = 20;
	public int mapHeight = 20;

	[Space]
	[Header("Visualize Map")]
	public GameObject mapContainer;
	public GameObject tilePrefab;
	public Vector2 tileSize = new Vector2(16, 16);

	[Space]
	[Header("Visualize Map")]
	public Texture2D islandTrexture;


	public Map map;
	// Use this for initialization
	void Start () {
		map = new Map ();
	}

	// Update is called once per frame
	public void MakeMap () {
		map.NewMap (mapWidth, mapHeight);
		Debug.Log ("created");
		CreateGrid ();
	}

	void CreateGrid() {
		clearMap ();
		Sprite[] sprites = Resources.LoadAll<Sprite> (islandTrexture.name);
		var total = map.tiles.Length;
		var maxCols = map.coloumns;
		var cols = 0;
		var row = 0;

		for (int i = 0; i < total; i++) {
			cols = i % maxCols;

			var x = cols * tileSize.x;
			var y = -row * tileSize.y;

			var go = Instantiate (tilePrefab);
			go.name = "Tile" + i;
			go.transform.SetParent (mapContainer.transform);
			go.transform.position = new Vector2 (x, y);
			  
			var spriteId = 0;
			SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
			sr.sprite = sprites [spriteId]; 

			if (cols == maxCols - 1) {
				row++;
			}

		}
	}

	private void clearMap() {
		Transform[] children = mapContainer.transform.GetComponentsInChildren<Transform> ();
		for (int i = children.Length - 1; i > 0; i--) {
			Destroy (children [i].gameObject);
		}
	}


}