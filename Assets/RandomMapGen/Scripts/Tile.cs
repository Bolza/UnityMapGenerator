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

	private void CalculateAutotileId() {
		var sideValues = new StringBuilder ();
		foreach (Tile tile in neighbors) {
			sideValues.Append (tile == null ? "0" : "1");
		}
		autotileId = Convert.ToInt32 (sideValues.ToString (), 2);
	}

}
