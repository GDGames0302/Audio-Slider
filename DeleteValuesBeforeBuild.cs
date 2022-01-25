#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace GDGames.Audio
{
    public class DeleteValuesBeforeBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        public void OnPostprocessBuild(BuildReport report)
        {

        }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif