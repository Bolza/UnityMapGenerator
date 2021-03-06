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
	[Header("Decorate Map")]
	[Range(0, 0.9f)]
	public float erodePercent = 0.5f;
	public int erodeIterations = 2;
	[Range(0, 0.9f)]
	public float treePercent = .2f;
	[Range(0, 0.9f)]
	public float mountainPercent  = .2f;
	[Range(0, 0.9f)]
	public float hillPercent  = .2f;
	[Range(0, 0.9f)]
	public float townPercent  = .05f;
	[Range(0, 0.9f)]
	public float monsterPercent  = .1f;
	[Range(0, 0.9f)]
	public float lakePercent  = .05f;

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
		map.createIsland (
			erodePercent, 
			erodeIterations, 
			treePercent, 
			mountainPercent, 
			hillPercent, 
			townPercent, 
			monsterPercent,
			lakePercent
		);
		CreateGrid ();
		CenterMapOnTile (map.playerTile.id);
	}

	void CreateGrid() {
		clearMap ();

		// Loading sprite map
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

			// Choose which n sprite from sprite-map
			var tile = map.tiles[i];
			var spriteId = tile.autotileId;
			if (spriteId >= 0) {
				SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
				sr.sprite = sprites [spriteId]; 
			}

			// Go to new row if this is last coloumn
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

	void CenterMapOnTile(int index ){
		var camPos = Camera.main.transform.position;
		var width = map.coloumns;
		camPos.x = (index % width) * tileSize.x;
		camPos.y = -((index / width) * tileSize.y);
		Camera.main.transform.position = camPos;
	}
}
