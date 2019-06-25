using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class PlayerData {
	public static PlayerData Data { get {return _data;} }

	private static PlayerData _data;
	private Vector2Ser _econGridSize;
	private Vector2Ser _warGridSize;
	private Vector2Ser _sciGridSize;

	private bool _loaded;

	public static void Load() {
		if(File.Exists(FilePath)) {
			if(PlayerPrefs.GetString("PlayerKey") == Serializer.CalculateMD5(FilePath)) {
				_data = Serializer.Load<PlayerData>(FilePath);
				if(_data != null) {
					Debug.Log("Existing Player Save data found and loaded.");
					Debug.Log(_data._econGridSize.x + ", " + _data._econGridSize.y);
					return;
				}
			}
			File.Delete(FilePath);
		}
		Debug.Log("Existing Player Save data either not found or tampered. Initializing new Save.");

		_data = new PlayerData();
		_data.Initialize();
	}

	public void Save() {
		Serializer.Save<PlayerData>(FilePath, _data);
		PlayerPrefs.SetString("PlayerKey", Serializer.CalculateMD5(FilePath));
		PlayerPrefs.Save();
	}

	private void Initialize() {
		_econGridSize = new Vector2Ser(new Vector2(3, 5));
		_warGridSize = new Vector2Ser(new Vector2(3, 5));
		_sciGridSize = new Vector2Ser(new Vector2(3, 5));
		Save();
	}

	// --------------------------------------------------------------------- //
	// File path where static game data is saved/loaded
	private static string FilePath {
		get { return Application.persistentDataPath + Path.DirectorySeparatorChar  + "Player.dat";}
	}
}
