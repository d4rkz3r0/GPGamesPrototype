using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SwordTrail))]
[CanEditMultipleObjects]
public class SwordTrailEditor : Editor
{
    public override void OnInspectorGUI()
    {

        EditorGUILayout.PropertyField(serializedObject.FindProperty("PointStart"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("PointEnd"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("TrailColor"));

        if (GUI.changed)
        {

            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(target);
        }
    }
}

