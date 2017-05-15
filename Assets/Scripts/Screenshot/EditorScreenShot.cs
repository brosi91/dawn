#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorScreenShot : EditorWindow
{
public static int Width = 1920;

[MenuItem("Window/EditorScreenShot")]
static void Init()
{
var window = GetWindow<EditorScreenShot>();
window.Show();
}

void OnGUI()
{
Width = EditorGUILayout.IntField(Width);
if(GUILayout.Button("Take Screenshot"))
{
TakeScreenshot();
}
}

public static void TakeScreenshot()
{

var path = EditorUtility.SaveFilePanel("Save image", Application.dataPath, "Editor Screenshot", "PNG, png");
if (path != Application.dataPath)
{
var width = GetMainGameViewSize().x;
var superSize = Mathf.CeilToInt((float)Width / width);
Application.CaptureScreenshot(path, superSize);
}
}

public static Vector2 GetMainGameViewSize()
{
System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
return (Vector2)Res;
}
}
#endif