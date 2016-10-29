using UnityEngine;
using System.Collections;

public class Map {
	public Tile[] tiles;
	public int coloumns;
	public int rows;

	public void NewMap(int width, int height) {
		coloumns = width;
		rows = height;
		tiles = new Tile[ coloumns * rows ];
		CreateTiles ();
	}
		
	private void CreateTiles() {
		int total = tiles.Length;
		for (int i = 0; i < total; i++) {
			var tile = new Tile ();
			tile.id = i;
			tiles [i] = tile;
		}
	}
}
