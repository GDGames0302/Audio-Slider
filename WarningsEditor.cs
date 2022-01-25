#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace GDGames.Audio
{
    [InitializeOnLoad]
    public static class WarningsEditor
    {
        static WarningsEditor()
        {
            UnityEditor.SceneManagement.EditorSceneManager.sceneOpened += SceneOpenedCallback;
        }

        static void SceneOpenedCallback(Scene scene, UnityEditor.SceneManagement.OpenSceneMode mode)
        {
            CheckIfAudioSourcesDontHaveAGroup(scene);
        }

        static void CheckIfAudioSourcesDontHaveAGroup(Scene scene)
        {
            AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();

            bool isFirstAudioSourceWithNoMixerOutput = true;

            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.outputAudioMixerGroup == null)
                {
                    if (isFirstAudioSourceWithNoMixerOutput)
                    {
                        isFirstAudioSourceWithNoMixerOutput = false;
                        Debug.Log("Audio Sources that have no Audio Mixer Group as output from " + scene.name + " scene are ->");
                    }

                    Debug.LogWarning(audioSource.name + " has no Audio Mixer Group as output. Make sure it has the corresponding Audio Mixer Group as output!");
                }
            }
        }
    }
}
#endif