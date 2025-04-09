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
        // ScriptableObject'unuzun referans�n� al�n
        ScriptableObject scriptableObject = this;
        // SerializedObject olu�turun
        serializedObject = new SerializedObject(scriptableObject);
        // tileMaps alan�n� bulun
        tileMapsProperty = serializedObject.FindProperty("tileMaps");
    }

    public void AddTilemap()
    {
        serializedObject.Update();

        // tileMapsProperty i�in �zel bir b�rakma alan� olu�turun
        EditorGUILayout.PropertyField(tileMapsProperty, true);

        // S�r�kleme i�lemi
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

                // De�i�iklikleri uygulay�n
                serializedObject.ApplyModifiedProperties();
            }

            e.Use();
        }

        // SerializedObject'u temizleyin
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
