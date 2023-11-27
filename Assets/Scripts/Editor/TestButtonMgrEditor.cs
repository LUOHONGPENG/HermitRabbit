using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestButtonMgr))]
public class TestButtonMgrEditor : Editor
{
    int Day = 0;

    int MapID = 0;


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        TestButtonMgr myScript = (TestButtonMgr)target;

        Day = EditorGUILayout.IntField("Day", Day);

        if (GUILayout.Button("SetDay"))
        {
            PublicTool.GetGameData().numDay = Day;
        }

        MapID = EditorGUILayout.IntField("MapID", MapID);

        if (GUILayout.Button("AddMapClip"))
        {
            PublicTool.GetGameData().AddMapClipHeld(MapID);
        }
    }
}