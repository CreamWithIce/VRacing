using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RecordSystem))]
// Provides a button in the inspector to start recording an AI car
public class RecordGUI : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        RecordSystem Rec = (RecordSystem)target;

        if(GUILayout.Button("Start Recording")){
            Rec.currentlyRecording = true;
        }
        if(GUILayout.Button("Save")){
            Rec.Save();
        }

        
    }
} 