using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

}


// Static Tile data
public class TileData {
	public GridType Type {get;set;}
	public int ID {get;set;}
	public string Name {get;set;}
	
}

// Enum for tabs
public enum GridType {
	Econ = 0,
	War = 1,
	Sci = 2
}