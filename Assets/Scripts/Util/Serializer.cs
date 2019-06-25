using System;
using System.IO;
using UnityEngine;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer {
	public static T Load<T>(string filename) where T: class {
		if(File.Exists(filename)) {
			try {
				using (Stream stream = File.OpenRead(filename)) {
					BinaryFormatter formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as T;
				}
			}
			catch(Exception e) {
				Debug.Log(e.Message);
			}
		}
		return default(T);
	}

	public static void Save<T>(string filename, T data) where T: class {
		using (Stream stream = File.OpenWrite(filename)) {    
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, data);
		}
	}

	public static string CalculateMD5(string filename) {
		using(MD5 md5 = MD5.Create()) {
			using(FileStream stream = File.OpenRead(filename)) {
				byte[] hash = md5.ComputeHash(stream);
				return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
			}
		}
	}
}

// ------------------------------------------------------------------------- //
// Custom serialization types
[Serializable]
public struct Vector2Ser {
	public float x;
	public float y;

	public Vector2Ser(Vector2 value) {
		x = value.x;
		y = value.y;
	}

	public Vector2 Value {
		get { return new Vector2(x, y); }
	}
}