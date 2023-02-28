using Clicker.Localization;
using UnityEditor;
using UnityEngine;

namespace Clicker
{
    public static class LocalizationBuilderEditorResolver
    {
        private static LocalizationBuilder s_localizator;
        public static LocalizationBuilder LabelsLocalizator;

        private static LocalizationBuilder FindInstance()
        {
            var guids = AssetDatabase.FindAssets($"t: {nameof(LocalizationBuilder)}");
            if (guids.Length == 0)
                return null;
            if (guids.Length > 1)
                Debug.LogWarning($"More than 1 {nameof(LocalizationBuilder)} created");
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<LocalizationBuilder>(path);
        }
    }
}