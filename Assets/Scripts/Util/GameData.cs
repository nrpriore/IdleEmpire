using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class GameData {

	public static GameData Data { get {return _data;} }
	public TileData[] EconTiles { get {return _econTiles;} }
	public TileData[] WarTiles { get {return _warTiles;} }
	public TileData[] SciTiles { get {return _sciTiles;} }

	private static GameData _data;
	private TileData[] _econTiles;
	private TileData[] _warTiles;
	private TileData[] _sciTiles;

	private static string _version = "";

	// Parses static data into game-classes
	public static void Load() {
		if(_version != "") {
			Debug.Log("Static data for v" + _version + " already loaded. Skipping Load call");
			return;
		}

		_data = new GameData();
		JObject data = JObject.Parse(Resources.Load<TextAsset>("Static/static").text);
		_version = data["version"].Value<string>();

		// Load Tile Data -------------------------------------------------- //
		data = data["tiles"].Value<JObject>();
		// Econ Tiles
		_data._econTiles = new TileData[data["num_econ"].Value<int>()];
		JArray econTiles = data["econ"].Value<JArray>();
		for(int id = 0; id < econTiles.Count; id++) {
			_data._econTiles[id] = new TileData(0, econTiles[id].Value<JObject>());
		}
		// War Tiles
		_data._warTiles = new TileData[data["num_war"].Value<int>()];
		JArray warTiles = data["war"].Value<JArray>();
		for(int id = 0; id < warTiles.Count; id++) {
			_data._warTiles[id] = new TileData(0, warTiles[id].Value<JObject>());
		}
		// Sci Tiles
		_data._sciTiles = new TileData[data["num_sci"].Value<int>()];
		JArray sciTiles = data["sci"].Value<JArray>();
		for(int id = 0; id < sciTiles.Count; id++) {
			_data._sciTiles[id] = new TileData(0, sciTiles[id].Value<JObject>());
		}
	}
}

// Static Tile data
public class TileData {
	public GridType TypeID { get {return _typeID;} }
	public int ID { get {return _id;} }
	public string Name { get {return _name;} }

	private GridType _typeID;
	private int _id;
	private string _name;
	public TileData(GridType typeID, JObject data) {
		_typeID = typeID;
		_id = data["id"].Value<int>();
		_name = data["name"].Value<string>();
	}
}

// Enum for tabs
public enum GridType {
	Econ = 0,
	War = 1,
	Sci = 2
}