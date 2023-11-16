using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Map map = target as Map;

        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }
}
