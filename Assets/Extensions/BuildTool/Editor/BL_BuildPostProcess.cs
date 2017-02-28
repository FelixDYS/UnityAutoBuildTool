
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using UnityEditor.iOS.Xcode;
using System.IO;

public class BL_BuildPostProcess
{
	public static bool isDever = true;
	public static bool isMagikid = true;

	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
	{
		#if UNITY_IOS
		if (buildTarget == BuildTarget.iOS)
		{
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));
			string target = proj.TargetGuidByName("Unity-iPhone");

			proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
			proj.SetBuildProperty (target, "PROVISIONING_PROFILE", 
                        "********-*****-****-****-************");
				
			proj.AddFrameworkToProject (target, "libz.1.2.5.tbd", true);
			proj.AddFrameworkToProject (target, "libsqlite3.tbd", true);

			File.WriteAllText(projPath, proj.WriteToString());

//			EditorPlist (path);
//			EditorFile (path);
//          EditorCode (path);
		}

		#endif
	}

	private static void EditorFile(string filePath)
	{
		File.Copy ("/Users/magikid/Documents/Math/Keyboard.mm", filePath + "/Classes/UI/Keyboard.mm", true);
	}

	private static void EditorCode(string filePath)
	{
		//读取UnityAppController.mm文件
		XClass UnityAppController = new XClass(filePath + "/Libraries/Plugins/iOS/UpdateExpirationDate.mm");

		//在指定代码后面增加一行代码
		UnityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"","#import <ShareSDK/ShareSDK.h>");

		//在指定代码中替换一行
		UnityAppController.Replace("@\"https://sandbox.itunes.apple.com/verifyReceipt\"","@\"https://buy.itunes.apple.com/verifyReceipt\"");

		//在指定代码后面增加一行
		UnityAppController.WriteBelow("UnityCleanup();\n}","- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url\r{\r    return [ShareSDK handleOpenURL:url wxDelegate:nil];\r}");
	}

	private static void EditorPlist(string plistPath)
	{
		string infoPath = plistPath + "/info.plist";
		PlistDocument info = new PlistDocument ();
		info.ReadFromString (File.ReadAllText (infoPath));

		info.root.SetBoolean ("ITSAppUsesNonExemptEncryption", false);
        info.root.SetString("NSCameraUsageDescription", "App需要使用您的相机");
        info.root.SetString("NSMicrophoneUsageDescription", "App需要使用您的麦克风");

		PlistElementArray addArray = info.root.CreateArray ("LSApplicationQueriesSchemes");
		addArray.AddString ("weixin");
		addArray.AddString ("wechat");
		addArray.AddString ("wtloginmqq2");
		addArray.AddString ("mqqopensdkapiV3");
		addArray.AddString ("mqqwpa");

		File.WriteAllText(infoPath, info.WriteToString());
	}


}
