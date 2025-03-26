#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class SoftriseLevelEditor : EditorWindow
{
    public Grid selectedGrid;
    public Tilemap[] tileMaps;
    SerializedObject serializedObject;
    SerializedProperty tileMapsProperty;

    [MenuItem("Softrise/Softrise Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<SoftriseLevelEditor>("Level Editor");
    }

    private void OnEnable()
    {
        ReferencedTilemap();
    }

    private void OnGUI()
    {
        selectedGrid = EditorGUILayout.ObjectField(selectedGrid, typeof(Grid), true) as Grid;

        if (GUILayout.Button("Draw"))
        {
            // Draw button clicked
        }

        if (GUILayout.Button("Eraser"))
        {
            // Eraser button clicked
        }

        AddTilemap();
    }

    public void ReferencedTilemap()
    {
        // ScriptableObject'unuzun referansýný alýn
        ScriptableObject scriptableObject = this;
        // SerializedObject oluþturun
        serializedObject = new SerializedObject(scriptableObject);
        // tileMaps alanýný bulun
        tileMapsProperty = serializedObject.FindProperty("tileMaps");
    }

    public void AddTilemap()
    {
        serializedObject.Update();

        // tileMapsProperty için özel bir býrakma alaný oluþturun
        EditorGUILayout.PropertyField(tileMapsProperty, true);

        // Sürükleme iþlemi
        Event e = Event.current;
        if (e.type == EventType.DragUpdated || e.type == EventType.DragPerform)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (e.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();

                foreach (Tilemap draggedObject in DragAndDrop.objectReferences)
                {
                    if (draggedObject is Tilemap)
                    {
                        // Tilemap nesnesini diziye ekleyin
                        tileMapsProperty.arraySize++;
                        tileMapsProperty.GetArrayElementAtIndex(tileMapsProperty.arraySize - 1).objectReferenceValue = draggedObject;
                    }
                }

                // Deðiþiklikleri uygulayýn
                serializedObject.ApplyModifiedProperties();
            }

            e.Use();
        }

        // SerializedObject'u temizleyin
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
