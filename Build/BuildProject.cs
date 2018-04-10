#if UNITY_EDITOR

using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildProject : MonoBehaviour {

	const string bundleid = "com.premiumartsinc.kiryyu";
	const string outputPath = "Output";
	const string scenePath = "Scenes";

	static void debugLog(string str) {
		UnityEngine.Debug.Log (str);
	}

	static void runCMD(string cmd) {
		Process.Start (@"cmd", "/c " + cmd);
	}
	/*
	static string[] scenes = new [] { 
		"Assets/Scene/main.unity" 
	};
	*/

	static string firstSceneName = "Assets/Scenes/UI/Splash.unity";

	static string[] findAllScenes() {
		string folderName = Application.dataPath + "/" + scenePath;
		var dirInfo = new DirectoryInfo(folderName);
		var allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);
		List<string> scenes = new List<string>();
		scenes.Add (firstSceneName);
		foreach (var fileInfo in allFileInfos)
		{
			var name = fileInfo.FullName;
			if (name != null) {
				scenes.Add (name);
				debugLog("find scene: " + name);
			}
		}
		return scenes.ToArray();
	}

	static void openFolder(string folder) {
		var rootdir = Directory.GetCurrentDirectory ();
		var finalpath = Path.Combine (rootdir, folder);
		debugLog("try to open folder: " + finalpath);
		Process.Start(@finalpath);
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
		BuildPipeline.BuildPlayer (buildPlayerOptions);
		openFolder (outputPath);
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

	[MenuItem("ADB/uninstall")]
	public static void BuildADBUninstall() {
		runCMD("adb uninstall " + bundleid);
	}

	[MenuItem("ADB/install-adhoc")]
	public static void BuildADBInstallAdhoc() {
		var rootdir = Directory.GetCurrentDirectory ();
		var output = Path.Combine (rootdir, outputPath);
		var apkpath = Path.Combine (output, "android_adhoc.apk");
		runCMD ("adb install " + apkpath);
	}

	[MenuItem("ADB/install-store")]
	public static void BuildADBInstallStore() {
		var rootdir = Directory.GetCurrentDirectory ();
		var output = Path.Combine (rootdir, outputPath);
		var apkpath = Path.Combine (output, "android_store.apk");
		runCMD ("adb install " + apkpath);
	}
}

#endif
