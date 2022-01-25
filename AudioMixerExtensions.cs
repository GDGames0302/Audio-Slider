using UnityEngine;
using UnityEngine.Audio;
using System;

namespace GDGames.Audio
{
    public static class AudioMixerExtensions
    {
        public static string[] GetParametersList(this AudioMixer audioMixer)
        {
            Array parameters = (Array)audioMixer.GetType().GetProperty("exposedParameters").GetValue(audioMixer, null);
            string[] exposedParameters = new string[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterValue = parameters.GetValue(i);
                string parameter = (string)parameterValue.GetType().GetField("name").GetValue(parameterValue);
                exposedParameters[i] = parameter;
            }

            return exposedParameters;
        }

        public static bool hasExposedParameters(this AudioMixer audioMixer)
        {
            return audioMixer.GetParametersList() != null && audioMixer.GetParametersList().Length > 0;
        }

        public static float GetVolume(this AudioMixer audioMixer, string volumeName)
        {
            float audioVolume;
            audioMixer.GetFloat(volumeName, out audioVolume);
            return audioVolume;
        }

        public static void SetVolume(this AudioMixer audioMixer, string volumeName, float volume)
        {
            audioMixer.SetFloat(volumeName, volume);
        }
    }
}