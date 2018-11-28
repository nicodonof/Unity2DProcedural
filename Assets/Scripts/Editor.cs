using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Editor : EditorWindow {

	static LevelCreator lc;

	public int difficulty;

	public int size;

	public int seed;
	
	private int chunkSize;

	[MenuItem("Window/LevelEditor")]
	static void Init() {
		lc = GameObject.Find("LevelManager").GetComponent<LevelCreator>();
		// Get existing open window or if none, make a new one:
		Editor window = (Editor)GetWindow(typeof(Editor));
		window.Show();
	}
	
	private void OnGUI() {
		lc = GameObject.Find("LevelManager").GetComponent<LevelCreator>();
		if (GUILayout.Button("Make Level")) {
			lc.EditorCreateChunks(difficulty, size, chunkSize, seed);	
		}

		if (GUILayout.Button("Delete Level")) {
			lc.DeleteAll();
		}

		difficulty = EditorGUILayout.IntSlider("Difficulty", difficulty, 0, 10);
		size = EditorGUILayout.IntSlider("MaxSize", size, 0, 10);
		seed = EditorGUILayout.IntField("Seed", seed);
		chunkSize = EditorGUILayout.IntField("Number of Chunks", chunkSize);
	}

}
