using UnityEngine;
using UnityEngine.UI;

namespace GDGames.Audio
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField]
        AudioManager[] audioManagers;

        Slider volumeSlider;


        void Awake()
        {
            GetSliderComponent();
        }

        void Start()
        {
            if (volumeSlider == null) return;
            
            LoadSliderValue();
        }

        void OnEnable()
        {
            if (volumeSlider == null) return;

            AddListenerToSlider();
        }

        void OnDisable()
        {
            if (volumeSlider == null) return;

            RemoveListenerFromSlider();
        }


        void GetSliderComponent()
        {
            volumeSlider = GetComponent<Slider>();
        }

        void LoadSliderValue()
        {
            volumeSlider.value = audioManagers[0].GetVolume();
        }

        void AddListenerToSlider()
        {
            volumeSlider.onValueChanged.AddListener(SliderValueChanged);
        }

        void RemoveListenerFromSlider()
        {
            volumeSlider.onValueChanged.RemoveListener(SliderValueChanged);
        }

        void SliderValueChanged(float value)
        {
            if (value == volumeSlider.minValue)
            {
                foreach (AudioManager audioManager in audioManagers)
                {
                    audioManager.MuteAudio();
                }
            }
            else
            {
                foreach (AudioManager audioManager in audioManagers)
                {
                    audioManager.SetVolume(value);
                }
            }
        }

        #region Unity Editor - Set Slider Default Values
#if UNITY_EDITOR
        void Reset()
        {
            if (volumeSlider == null)
            {
                GetSliderComponent();
            }

            if (volumeSlider == null) return;

            ResetSliderValues();
        }

        void ResetSliderValues()
        {
            volumeSlider.maxValue = 0;
            volumeSlider.minValue = -80;
        }
#endif
        #endregion
    }
}