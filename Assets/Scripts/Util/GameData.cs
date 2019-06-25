using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

[Serializable]
public class GameData {

	public static GameData Data { get {return _data;} }
	public TileData[] EconTiles { get {return _econTiles;} }
	public TileData[] WarTiles { get {return _warTiles;} }
	public TileData[] SciTiles { get {return _sciTiles;} }

	private static GameData _data;
	private TileData[] _econTiles;
	private TileData[] _warTiles;
	private TileData[] _sciTiles;

	private string _version;

	// Parses static data into game-classes
	public static void Load() {
		JObject data = JObject.Parse(Resources.Load<TextAsset>("Static/static").text);
		string version = data["version"].Value<string>();
		string key = data["key"].Value<string>();

		if(File.Exists(FilePath)) {
			if(key == Serializer.CalculateMD5(FilePath)) {
				_data = Serializer.Load<GameData>(FilePath);
				if(_data != null) {
					if(version == _data._version) {
						Debug.Log("Static data for v" + version + " already loaded. Skipping Parse.");
						return;
					}
				}
			}
			File.Delete(FilePath);
		}
		Debug.Log("Static data for v" + version + " not found. Parsing static data.");

		_data = new GameData();
		_data._version = version;

		// Load Tile Data -------------------------------------------------- //
		data = data["tiles"].Value<JObject>();
		// Econ Tiles
		_data._econTiles = new TileData[data["num_econ"].Value<int>()];
		JArray econTiles = data["econ"].Value<JArray>();
		for(int id = 0; id < econTiles.Count; id++) {
			_data._econTiles[id] = new TileData(GridType.Econ, econTiles[id].Value<JObject>());
		}
		// War Tiles
		_data._warTiles = new TileData[data["num_war"].Value<int>()];
		JArray warTiles = data["war"].Value<JArray>();
		for(int id = 0; id < warTiles.Count; id++) {
			_data._warTiles[id] = new TileData(GridType.War, warTiles[id].Value<JObject>());
		}
		// Sci Tiles
		_data._sciTiles = new TileData[data["num_sci"].Value<int>()];
		JArray sciTiles = data["sci"].Value<JArray>();
		for(int id = 0; id < sciTiles.Count; id++) {
			_data._sciTiles[id] = new TileData(GridType.Sci, sciTiles[id].Value<JObject>());
		}


		// Save the loaded class so this can just be loaded next time instead of parsed
		Serializer.Save<GameData>(FilePath, _data);
	}

	// --------------------------------------------------------------------- //
	// File path where static game data is saved/loaded
	private static string FilePath {
		get { return Application.persistentDataPath + Path.DirectorySeparatorChar  + "Static.dat";}
	}

	// Static Tile data
	[Serializable]
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
}