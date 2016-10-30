using UnityEngine;
using System.Collections;
using System.Linq;

public class Map {
	public Tile[] tiles;
	public int coloumns;
	public int rows;
	public enum TileType { 
		empty = -1,
		single = 0,
		grass = 15, 
		tree = 16, 
		hill = 17, 
		mountain = 18, 
		town = 19, 
		castle = 20, 
		monster = 21 
	};
	public Tile[] coastTiles {
		get {
			return tiles.Where (t => t.autotileId < (int)TileType.grass && t.autotileId > -1).ToArray();
		}
	}
	public Tile[] landTiles {
		get {
			return tiles.Where (t => t.autotileId == (int)TileType.grass).ToArray();
		}
	}
	public Tile playerTile {
		get {
			return tiles.FirstOrDefault (t => t.autotileId == (int)TileType.castle);
		}
	}
	public Tile[] singleTiles {
		get {
			return tiles.Where (t => t.autotileId < (int)TileType.single).ToArray();
		}
	}
	public Tile[] tooSmallTiles {
		get {
			return coastTiles.Where (t => {
				var howManyCenterTiles = t.neighbors.Where(n => n != null && n.autotileId >= (int)TileType.grass );
				return howManyCenterTiles.ToArray().Length < 1;
			})
			.ToArray();
		}
	}

	public void createIsland(
		float erodePercent,
		int erodeIterations,
		float treePercent,
		float mountainPercent,
		float hillPercent,
		float townPercent,
		float monsterPercent,
		float lakePercent
	) {
		decorateTiles (landTiles, lakePercent, TileType.empty);
		for (int i = 0; i < erodeIterations; i++) {
			decorateTiles(coastTiles, erodePercent, TileType.empty);
		}
		decorateTiles (tooSmallTiles, 1, TileType.empty);

		createPlayerPosition ();

		decorateTiles (landTiles, treePercent, TileType.tree);
		decorateTiles (landTiles, hillPercent, TileType.hill);
		decorateTiles (landTiles, mountainPercent, TileType.mountain);
		decorateTiles (landTiles, townPercent, TileType.town);
		decorateTiles (landTiles, monsterPercent, TileType.monster);


	}

	private void createPlayerPosition() {
		var openTiles = landTiles;
		randomizeTileArray (openTiles);
		openTiles [0].autotileId = (int)TileType.castle;

	}

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

	private void decorateTiles(Tile[] tiles, float percent, TileType type) {
		// Get an int number that is the percentage on the total 
		var total = Mathf.FloorToInt (tiles.Length * percent);
		randomizeTileArray (tiles);
		for (int i = 0; i < total; i++) {
			var tile = tiles [i];
			if (type == TileType.empty)
				tile.ClearNeighbours ();
			tile.autotileId = (int)type;
		}
	}

	public void randomizeTileArray (Tile[] tiles) {
		for (int i = 0; i < tiles.Length; i++) {
			var tmp = tiles [i];
			var r = Random.Range (i, tiles.Length);
			tiles [i] = tiles [r];
			tiles [r] = tmp;
		}
	}

}
