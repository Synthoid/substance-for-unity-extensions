using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Handles initializing and drawing UI for substance extension user preferences.
    /// </summary>
    public static class SubstanceExtensionsEditorPreferences
    {
        //private const string kLogUpdateWarningsKey = "sos-log-update-warnings";
        private const string kHighlightColorKey = "sos-highlight-color";

        //Asset updates
        //private static readonly GUIContent kAssetUpdatesLabel = new GUIContent("Asset Updates", "Settings that affect asset update operations.");
        //private static readonly GUIContent kLogUpdateWarningsLabel = new GUIContent("Log Update Warnings", "If true, log harmless warnings about the update process. ie Any inputs or outputs that were removed during .sbsar updates.");
        //Colors
        private static readonly GUIContent kColorsLabel = new GUIContent("Colors", "Colors used in various inspectors and console logs.");
        private static readonly GUIContent kHighlightColorLabel = new GUIContent("Highlight Color", "Color used when highlighting important text in the console.");

        private static bool initialized = false;
        //private static bool logUpdateWarnings = true;
        private static Color highlightColor = new Color32(255, 200, 0, 255);
        private static string highlightColorHtml = "FFC800";

        /*public static bool LogUpdateWarnings
        {
            get
            {
                Initialize();

                return logUpdateWarnings;
            }
        }*/


        public static Color HighlightColor
        {
            get
            {
                Initialize();

                return highlightColor;
            }
        }

        public static string HighlightColorHtml
        {
            get
            {
                Initialize();

                return highlightColorHtml;
            }
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if(initialized) return;

            initialized = true;

            //logUpdateWarnings = EditorPrefs.GetBool(kLogUpdateWarningsKey, true);
            highlightColorHtml = EditorPrefs.GetString(kHighlightColorKey, "FFC800");

            if(!ColorUtility.TryParseHtmlString(highlightColorHtml, out highlightColor))
            {
                highlightColor = new Color32(255, 200, 0, 255);
            }
        }

        [SettingsProvider]
        public static SettingsProvider CreateExtensionsPreferencesSettingsProvider()
        {
            return new SettingsProvider("Preferences/Adobe Substance 3D Extensions", SettingsScope.User)
            {
                keywords = new string[] { "Adobe", "Substance", "3D", "Extensions", "sbsar" },
                guiHandler = OnPreferencesGUI
            };
        }


        private static void OnPreferencesGUI(string searchString)
        {
            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = SubstanceExtensionsEditorUtility.kSettingsLabelWidth;

            //DrawAssetUpdatesSection();
            DrawColorsSection();

            EditorGUIUtility.labelWidth = labelWidth;
        }


        /*private static void DrawAssetUpdatesSection()
        {
            EditorGUILayout.LabelField(kAssetUpdatesLabel, EditorStyles.boldLabel);

            if(SubstanceExtensionsProjectSettings.DisableAutoUpdates)
            {
                EditorGUILayout.HelpBox("Auto updates have been disabled in project settings.", MessageType.Warning);
            }

            EditorGUI.BeginChangeCheck();
            logUpdateWarnings = EditorGUILayout.Toggle(kLogUpdateWarningsLabel, logUpdateWarnings);
            if(EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(kLogUpdateWarningsKey, logUpdateWarnings);
            }
        }*/



        private static void DrawColorsSection()
        {
            EditorGUILayout.LabelField(kColorsLabel, EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            highlightColor = EditorGUILayout.ColorField(kHighlightColorLabel, highlightColor);
            if(EditorGUI.EndChangeCheck())
            {
                highlightColorHtml = ColorUtility.ToHtmlStringRGB(highlightColor);
                EditorPrefs.SetString(kHighlightColorKey, highlightColorHtml);
            }
        }
    }
}