using UnityEngine;

namespace GDGames.Audio
{
    public class AudioLoader : MonoBehaviour
    {
        [SerializeField]
        AudioManager[] audioManagers;


        void Start()
        {
            LoadVolume();
        }


        void LoadVolume()
        {
            foreach (AudioManager audioManager in audioManagers)
            {
                audioManager.LoadMixerVolume();
            }
        }
    }
}