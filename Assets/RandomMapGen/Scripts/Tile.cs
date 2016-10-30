using UnityEngine;
using System.Collections;
using System;
using System.Text;

public enum Sides { bottom, right, left, top };

public class Tile {
	public int id = 0;
	public Tile[] neighbors = new Tile[4];
	public int autotileId;

	public void AddNeighbour(Sides side, Tile tile) {
		neighbors[(int)side] = tile;
		CalculateAutotileId ();
	}

	public void RemoveNeighbour(Tile tile) {
		var total = neighbors.Length;
		for (int i = 0; i < total; i++) {
			if (neighbors [i] != null && neighbors [i].id == tile.id) {
				neighbors [i] = null;
			}
		}
		CalculateAutotileId ();
	}

	public void ClearNeighbours() {
		var total = neighbors.Length;
		for (int i = 0; i < total; i++) {
			var neTile = neighbors [i];
			if (neTile != null) {
				neTile.RemoveNeighbour (this);
				neighbors [i] = null;
			}
		}
		CalculateAutotileId ();
	}

	private void CalculateAutotileId() {
		var sideValues = new StringBuilder ();
		foreach (Tile tile in neighbors) {
			sideValues.Append (tile == null ? "0" : "1");
		}
		autotileId = Convert.ToInt32 (sideValues.ToString (), 2);
	}

}
