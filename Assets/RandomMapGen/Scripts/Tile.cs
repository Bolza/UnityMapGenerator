using UnityEngine;
using System.Collections;
using System;
using System.Text;

public enum Sides { bottom, right, left, top };

public class Tile {
	public int id = 0;
	public Tile[] neighbors = new Tile[4];
	public autotileId;

	public void AddNeighbour(Sides side, Tile tile) {
		neighbors[(int)side] = tile;
	}



}
