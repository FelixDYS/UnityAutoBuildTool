
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;


public class BuildTool  : EditorWindow
{
	static int index1 = 0;
	static int index2 = 0;
	static int index3 = 0;
    static string name = "ituse";
    static string path = "/Users/magikid/Desktop/Build" + "/";

    static string[] androidChannels = new string[]{ 
        "QQ_Store", 
        "360_Store",
        "Baidu_Store",
        "LeTV_Store",
        "Wandoujia_Store",
        "YunOS_Store",
        "Meizu_Store",
        "Huawei_Store",
        "Dangbei_Store",
        "XiaoMi_Store",
        "OPPO_Store",
        "VIVO_Store",
        "Other_Store",
        "AppInstaller"
    };

	[MenuItem("FELIX/Show Build")]
	private static void ShowWindow()
	{
		EditorWindow.GetWindow<BuildTool> ();
	}
		
	void OnGUI()
	{
		#if UNITY_IOS
		EditorGUILayout.BeginVertical ();
		GUILayoutOption[] options = new GUILayoutOption[2]{GUILayout.Height (30), GUILayout.Width (100)};
		GUIStyle style1 = new GUIStyle ();
		style1.fontSize = 15;
		style1.fixedWidth = 150;
		style1.normal.textColor = Color.white;

		GUIStyle style2 = new GUIStyle ();
		style2.fontSize = 16;
		style2.fixedWidth = 150;
		style2.normal.textColor = Color.cyan;

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.LabelField("Build Type：", style1, options);
		index1 = EditorGUILayout.Popup (index1, new string[]{ "Deverlop", "Distribution" }, style2, options);

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.LabelField("Bundle Id：", style1, options);
		index2 = EditorGUILayout.Popup (index2, new string[]{ "Magikid", "Angellecho" }, style2, options);

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal ();

		EditorGUILayout.LabelField("Store Type：", style1, options);
		index3 = EditorGUILayout.Popup (index3, new string[]{ "APPSTORE", "SELLCHECK", "UNLOCK"}, style2, options);

		EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal ();

        EditorGUILayout.LabelField("Build Version：", style1, options);
        EditorGUILayout.LabelField(PlayerSettings.bundleVersion, style1, options);

        EditorGUILayout.EndHorizontal();

//        EditorGUILayout.BeginHorizontal ();

//        EditorGUILayout.LabelField("Build Code：", style1, options);
//        EditorGUILayout.LabelField(PlayerSettings.Android.bundleVersionCode.ToString() , style1, options);

//        EditorGUILayout.EndHorizontal();

		GUILayout.Space (20);
		EditorGUILayout.BeginHorizontal ();
		GUILayout.Space (60);
		if (GUILayout.Button("Build",GUILayout.Height(80),GUILayout.Width(100)))
		{
			
            if (EditorUtility.DisplayDialog ("提示", "确定压包么？,请清空Xcode的生成APP", "Yes", "No"))
			{
				if (index1 == 0)
				{
					EditorApplication.delayCall += VBuildDeverlop; 
				}
				else
				{
					EditorApplication.delayCall += VBulidDistribution; 
				}
			}
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical ();

        #endif
        /*
		#elif UNITY_ANDROID
		GUILayout.Space (20);
        EditorGUILayout.BeginVertical ();

        GUILayoutOption[] options = new GUILayoutOption[2]{GUILayout.Height (30), GUILayout.Width (100)};
        GUIStyle style1 = new GUIStyle ();
        style1.fontSize = 15;
        style1.fixedWidth = 150;
        style1.normal.textColor = Color.white;

        GUIStyle style2 = new GUIStyle ();
        style2.fontSize = 16;
        style2.fixedWidth = 150;
        style2.normal.textColor = Color.cyan;


        EditorGUILayout.LabelField("Android Channel：", style1, options);

        EditorGUILayout.BeginHorizontal ();
        GUILayout.Space (60);
        index1 = EditorGUILayout.Popup (index1, androidChannels , style2, options);
        EditorGUILayout.EndHorizontal();

		GUILayout.Space (30);
        EditorGUILayout.BeginHorizontal ();
        GUILayout.Space (60);
		if (GUILayout.Button("Build",GUILayout.Height(80),GUILayout.Width(100)))
		{
			EditorApplication.delayCall += VBulidAndroid; 
		}
        EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal ();
        GUILayout.Space (60);
		if (GUILayout.Button("BuildRun",GUILayout.Height(80),GUILayout.Width(100)))
		{
			EditorApplication.delayCall += VBulidAndRunAndroid; 
		}
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
		#endif
*/
	}

    /*

	protected virtual void VBulidAndroid()
	{
        ChangeProjectConfig();
		BulidAndroid ();
	}

	protected virtual void VBulidAndRunAndroid()
	{
        ChangeProjectConfig();
		BulidAndRunAndroid ();
	}

    private static void ChangeProjectConfig()
    {
        string channel = androidChannels[index1];
        ProjectConfig projectConfig = new ProjectConfig();
        projectConfig.platform = "android";
        projectConfig.channel = channel;
        AssetDatabase.CreateAsset(Instantiate(projectConfig), "Assets/Resources/ProjectConfig/ProjectConfig.asset");
    }

	private static void BulidAndroid()
	{
        string androidPath = "/Users/magikid/Desktop/Build/Android/";
        string name = "Channel " + 
            (Resources.Load<ProjectConfig>("ProjectConfig/ProjectConfig")).channel.Trim() + " " + PlayerSettings.bundleVersion + ".apk";

        if (!Directory.Exists(androidPath)) {
            Directory.CreateDirectory(androidPath);
        }

        Debug.Log(name);
		List<string> scenes = new List<string>();
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			scenes.Add(EditorBuildSettings.scenes [i].path);
		}

        BuildPipeline.BuildPlayer (scenes.ToArray(), androidPath + name,
            BuildTarget.Android, BuildOptions.None); 
	}

	private static void BulidAndRunAndroid()
	{
		string androidPath = "/Users/magikid/Desktop/Build/Android/";
        string name = "Channel " + 
            (Resources.Load<ProjectConfig>("ProjectConfig/ProjectConfig")).channel.Trim() + " " + PlayerSettings.bundleVersion + ".apk";

        if (!Directory.Exists(androidPath)) {
            Directory.CreateDirectory(androidPath);
        }

        Debug.Log(name);
		List<string> scenes = new List<string>();
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			scenes.Add(EditorBuildSettings.scenes [i].path);
		}

        BuildPipeline.BuildPlayer (scenes.ToArray(), androidPath + name,
            BuildTarget.Android, BuildOptions.AutoRunPlayer); 
	}
*/
	protected virtual void VBuildDeverlop()
	{
		BuildDeverlop ();
	}

	protected virtual void VBulidDistribution()
	{
		BulidDistribution ();
	}

	private static void ChangeBundleID()
	{
		if (index2 == 0)
		{
			BL_BuildPostProcess.isMagikid = true;
			PlayerSettings.bundleIdentifier = PlayerSettings.bundleIdentifier.Split ('.') [0] + ".magikid."
				+ PlayerSettings.bundleIdentifier.Split ('.') [2];
		}
		else
		{
			BL_BuildPostProcess.isMagikid = false;
			PlayerSettings.bundleIdentifier = PlayerSettings.bundleIdentifier.Split ('.') [0] + ".angellecho."
				+ PlayerSettings.bundleIdentifier.Split ('.') [2];
		}
	}

	private static void ChangeDistributionType()
	{
	}

	public static void BuildDeverlop()
	{
		BL_BuildPostProcess.isDever = true;
		ChangeDistributionType ();
		ChangeBundleID ();
		Bulid ();
	}


	public static void BulidDistribution()
	{
		BL_BuildPostProcess.isDever = false;
		ChangeDistributionType ();
		ChangeBundleID ();
		Bulid ();
	}

	private static void Bulid()
	{
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
		}

		List<string> scenes = new List<string>();
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
            if(EditorBuildSettings.scenes [i].enabled)
            {
                scenes.Add(EditorBuildSettings.scenes[i].path);   
            }
		}

        BuildPipeline.BuildPlayer (scenes.ToArray(), path + PlayerSettings.bundleIdentifier + PlayerSettings.bundleVersion, 
            BuildTarget.iOS, BuildOptions.AcceptExternalModificationsToPlayer); 
	}

}
