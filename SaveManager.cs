using UnityEngine;

namespace GDGames.Audio
{
    public static class SaveManager
    {
        public static void SaveVolume(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static float LoadVolume(string key, float defaultValue)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : defaultValue;
        }
    }
}