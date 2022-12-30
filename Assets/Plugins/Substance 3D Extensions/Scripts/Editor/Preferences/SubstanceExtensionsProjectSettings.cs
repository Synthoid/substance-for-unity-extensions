using UnityEngine;
using UnityEditor;

namespace SOS.SubstanceExtensionsEditor
{
    public static class SubstanceExtensionsProjectSettings
    {
        private const string kGitHubUrl = "https://github.com/Synthoid/substance-for-unity-extensions";
        private const string kDocumetationUrl = "https://github.com/Synthoid/substance-for-unity-extensions/blob/main/docs/index.md";

        private static readonly GUIContent kLinksLabel = new GUIContent("Links", "Useful links for Substance for Unity Extensions.");
        private static readonly GUIContent kGitHubLabel = new GUIContent("GitHub", "Click to open the GitHub repository for Substance for Unity Extensions.");
        private static readonly GUIContent kDocumentationLabel = new GUIContent("Documentation", "Click to open the online documentation for Substance for Unity Extensions.");

        [SettingsProvider]
        public static SettingsProvider CreateExtensionsProjectSettingsProvider()
        {
            return new SettingsProvider("Project/Adobe Substance 3D Extensions", SettingsScope.Project)
            {
                keywords = new string[4] { "Adobe", "Substance", "3D", "Extensions" },
                guiHandler = OnProjectSettingsGUI
            };
        }


        private static void OnProjectSettingsGUI(string searchString)
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
    }
}