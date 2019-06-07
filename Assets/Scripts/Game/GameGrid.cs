using UnityEngine;

// The governing class for within each 'tab' (Econ, Sci, War, etc.)
public class GameGrid : MonoBehaviour {
	private const int Height = 5;
	private const int Width = 4;

	private Tile[][] _tiles;

	// Runs on GameObject instantiation. In this case, when the game loads
	void Start() {
		BuildGrid();
	}

	private void BuildGrid() {
		_tiles = new Tile[Height][];
		for(int i = 0; i < _tiles.Length; i++) {
			_tiles[i] = new Tile[Width];
		}
	}
}
