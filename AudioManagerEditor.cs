#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GDGames.Audio
{
    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            AudioManager audioManager = (AudioManager)target;

            audioManager.Refresh();
            
            base.OnInspectorGUI();

            if (audioManager.exposedParametersNames != null && audioManager.exposedParametersNames.Length > 0)
            {
                GUIContent arrayLabel = new GUIContent("Volume Name");
                audioManager.exposedParametersIndex = EditorGUILayout.Popup(arrayLabel, audioManager.exposedParametersIndex, audioManager.exposedParametersNames);

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}
#endif