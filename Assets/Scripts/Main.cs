using UnityEngine;

public class Main : MonoBehaviour {
	
	void Awake() {
		InitializeGame();
	}

	private void InitializeGame() {
		GameData.Load();
		PlayerData.Load();
	}
}
