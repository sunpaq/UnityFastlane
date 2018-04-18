#if UNITY_EDITOR

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;

public static class BuildPostProcess
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string xcodeProjectPath)
    {
		if (target == BuildTarget.iOS) {
			ProcessForiOS (xcodeProjectPath);
		}
    }

	private static void ProcessForiOS (string path)
	{
		string pjPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
		PBXProject pj = new PBXProject ();
		pj.ReadFromString (File.ReadAllText (pjPath));

		string target = pj.TargetGuidByName ("Unity-iPhone");
		AddFrameworks (pj, target);

		pj.SetBuildProperty (target, "CLANG_ENABLE_MODULES", "YES");

		File.WriteAllText (pjPath, pj.WriteToString ());
	}

	private static void AddFrameworks(PBXProject project, string targetGUID)
	{
		project.AddFrameworkToProject (targetGUID, "WebKit.framework", false);
		project.AddFrameworkToProject (targetGUID, "Photos.framework", false);
		project.AddFrameworkToProject (targetGUID, "PhotosUI.framework", false);
	}
}

#endif