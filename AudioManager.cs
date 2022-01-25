using UnityEngine;
using UnityEngine.Audio;

namespace GDGames.Audio
{
    [CreateAssetMenu]
    public class AudioManager : ScriptableObject
    {
        [HideInInspector]
        public int exposedParametersIndex;

        [HideInInspector]
        public string[] exposedParametersNames;

        [SerializeField]
        AudioMixer audioMixer;

        float volume;

        string volumeParameterName;
        float initialMixerVolume;

        const int minVolume = -80;
        const int maxVolume = 20;

        public float GetVolume() { return volume; }


        void OnEnable()
        {
            if (audioMixer == null) return;

            GetVolumeParameterName();
            GetInitialMixerVolume();
            LoadVolume();
        }


        public void SetVolumeToInitialMixerValue()
        {
            SetVolume(initialMixerVolume);
        }

        public void MuteAudio()
        {
            SetVolume(minVolume);
        }

        public void SetVolume(float newVolume, bool saveVolume = true)
        {
            volume = GetClampedVolume(newVolume);

            audioMixer.SetVolume(volumeParameterName, volume);

            if (saveVolume)
            {
                SaveVolume();
            }
        }

        public void LoadMixerVolume()
        {
            SetVolume(volume, false);
        }


        void GetVolumeParameterName()
        {
            volumeParameterName = exposedParametersNames[exposedParametersIndex];
        }

        void GetInitialMixerVolume()
        {
            if (audioMixer.GetVolume(volumeParameterName) != minVolume)
            {
                initialMixerVolume = audioMixer.GetVolume(volumeParameterName);
            }
        }

        void SaveVolume()
        {
            SaveManager.SaveVolume(GetInstanceID().ToString(), volume);
        }

        void LoadVolume()
        {
            volume = SaveManager.LoadVolume(GetInstanceID().ToString(), initialMixerVolume);
        }

        float GetClampedVolume(float volume)
        {
            return Mathf.Clamp(volume, minVolume, maxVolume);
        }

        #region Unity Editor
#if UNITY_EDITOR
        void OnValidate()
        {
            Refresh();
        }

        public void Refresh()
        {
            CheckIfNoAudioMixerSet();
            CheckIfNoExposedParameters();
        }

        void CheckIfNoAudioMixerSet()
        {
            if (audioMixer == null)
            {
                Debug.LogWarning(name + " has no AudioMixer. Please assign an Audio Mixer to the empty slot in the inspector!");
                exposedParametersNames = new string[0];
            }
        }

        void CheckIfNoExposedParameters()
        {
            if (audioMixer == null) return;

            if (!audioMixer.hasExposedParameters())
            {
                exposedParametersNames = new string[0];
                Debug.LogWarning("No exposed parameters found in corresponding Audio Mixer. Please make sure " + audioMixer.name + " has at least one exposed parameter!");
            }
            else if (exposedParametersNames != audioMixer.GetParametersList())
            {
                exposedParametersNames = audioMixer.GetParametersList();
            }
        }
#endif
        #endregion
    }
}