using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Handles initializing and drawing UI for substance extension project settings.
    /// </summary>
    public static class SubstanceExtensionsProjectSettings
    {
        private const string kGitHubUrl = "https://github.com/Synthoid/substance-for-unity-extensions";
        private const string kDocumetationUrl = "https://github.com/Synthoid/substance-for-unity-extensions/blob/main/docs/index.md";

        //Links
        private static readonly GUIContent kLinksLabel = new GUIContent("Links", "Useful links for Substance for Unity Extensions.");
        private static readonly GUIContent kGitHubLabel = new GUIContent("GitHub", "Click to open the GitHub repository for Substance for Unity Extensions.");
        private static readonly GUIContent kDocumentationLabel = new GUIContent("Documentation", "Click to open the online documentation for Substance for Unity Extensions.");
        //Settings
        private static readonly GUIContent kEditorSettingsLabel = new GUIContent("Editor Settings", "Settings that affect editor functionality.");

        private static SubstanceExtensionsProjectSettingsAsset settingsAsset = null;
        private static Editor settingsEditor = null;

        public static bool DisableAutoUpdates
        {
            get { return SubstanceExtensionsProjectSettingsAsset.Instance.disableAutoUpdates; }
        }

        [SettingsProvider]
        public static SettingsProvider CreateExtensionsProjectSettingsProvider()
        {
            return new SettingsProvider("Project/Adobe Substance 3D Extensions", SettingsScope.Project)
            {
                keywords = new string[] { "Adobe", "Substance", "3D", "Extensions", "sbsar" },
                guiHandler = OnProjectSettingsGUI,
                activateHandler = OnActivate,
                deactivateHandler = OnDeactivate
            };
        }


        private static void OnProjectSettingsGUI(string searchString)
        {
            float labelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = SubstanceExtensionsEditorUtility.kSettingsLabelWidth;

            DrawLinks(searchString);

            EditorGUILayout.Space();

            DrawSettingsAssetInspector(searchString);

            EditorGUIUtility.labelWidth = labelWidth;
        }


        private static void DrawLinks(string searchString)
        {
            EditorGUILayout.LabelField(kLinksLabel, EditorStyles.boldLabel);

            if(EditorGUILayout.LinkButton(kGitHubLabel))
            {
                Application.OpenURL(kGitHubUrl);
            }

            if(EditorGUILayout.LinkButton(kDocumentationLabel))
            {
                Application.OpenURL(kDocumetationUrl);
            }
        }


        private static void DrawSettingsAssetInspector(string searchString)
        {
            EditorGUILayout.LabelField(kEditorSettingsLabel, EditorStyles.boldLabel);

            settingsEditor.OnInspectorGUI();
        }


        private static void OnActivate(string search, VisualElement element)
        {
            settingsAsset = SubstanceExtensionsProjectSettingsAsset.Instance;
            settingsEditor = Editor.CreateEditor(settingsAsset);
        }


        private static void OnDeactivate()
        {
            if(settingsEditor == null) return;

            Object.DestroyImmediate(settingsEditor);

            settingsAsset = null;
            settingsEditor = null;
        }
    }
}