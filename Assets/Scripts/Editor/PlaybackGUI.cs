using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlaybackSystem))]
// Provides a custom button for testing the playback system of files
public class PlaybackGUI : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        PlaybackSystem pb = (PlaybackSystem)target;
        if(GUILayout.Button("Playback file")){
            pb.playFileBack = true;
        }

        
    }
}