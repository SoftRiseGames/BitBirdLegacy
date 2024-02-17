using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class SoftriseLevelEditor : EditorWindow
{
    public static event Action DrawListener;
    public static event Action EraserListener;
    [MenuItem("Softrise/Softrise Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<SoftriseLevelEditor>("Level Editor");
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Draw"))
            DrawListener.Invoke();
        
            
        if (GUILayout.Button("Eraser"))
            EraserListener.Invoke();
    }
}
