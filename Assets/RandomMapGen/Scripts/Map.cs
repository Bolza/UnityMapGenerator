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
		FindNeighbors ();
	}

	private void FindNeighbors() {
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < coloumns; c++) {
				var currentTile = tiles [coloumns * r + c];

				// Not the last row
				if (r < rows - 1) {
					currentTile.AddNeighbour (Sides.bottom, tiles [coloumns * (r + 1) + c]);
				}
				// Not the first row
				if (r > 0) {
					currentTile.AddNeighbour (Sides.top, tiles [coloumns * (r - 1) + c]);
				}
				// Not the first column
				if (c > 0) {
					currentTile.AddNeighbour (Sides.left, tiles [coloumns * r + (c-1) ]);
				}
				// Not the last column
				if (c < coloumns - 1) {
					currentTile.AddNeighbour (Sides.right, tiles [coloumns * r + (c+1) ]);
				}

			}
		}
	}
}
