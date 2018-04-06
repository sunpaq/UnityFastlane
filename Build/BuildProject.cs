#if UNITY_EDITOR

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildProject : MonoBehaviour {

	const string outputPath = "Output";
	const string scenePath = "Scene";

	/*
	static string[] scenes = new [] { 
		"Assets/Scene/main.unity" 
	};
	*/

	static string[] findAllScenes() {
		string folderName = Application.dataPath + "/" + scenePath;
		var dirInfo = new DirectoryInfo(folderName);
		var allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);
		List<string> scenes = new List<string>();
		foreach (var fileInfo in allFileInfos)
		{
			var name = fileInfo.FullName;
			if (name != null) {
				scenes.Add (name);
				Debug.Log ("find scene: " + name);
			}
		}
		return scenes.ToArray();
	}

	[MenuItem("Build/iOS/adhoc")]
	public static void BuildIOSAdhoc() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = findAllScenes();
		buildPlayerOptions.locationPathName = outputPath + "/ios_adhoc";
		buildPlayerOptions.target = BuildTarget.iOS;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

	[MenuItem("Build/iOS/store")]
	public static void BuildIOSStore() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = findAllScenes();
		buildPlayerOptions.locationPathName = outputPath + "/ios_store";
		buildPlayerOptions.target = BuildTarget.iOS;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

	[MenuItem("Build/Android/adhoc")]
	public static void BuildAndroidAdhoc() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = findAllScenes();
		buildPlayerOptions.locationPathName = outputPath + "/android_adhoc.apk";
		buildPlayerOptions.target = BuildTarget.Android;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

	[MenuItem("Build/Android/store")]
	public static void BuildAndroidStore() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		buildPlayerOptions.scenes = findAllScenes();
		buildPlayerOptions.locationPathName = outputPath + "/android_store.apk";
		buildPlayerOptions.target = BuildTarget.Android;
		buildPlayerOptions.options = BuildOptions.None;
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}


}

#endif
