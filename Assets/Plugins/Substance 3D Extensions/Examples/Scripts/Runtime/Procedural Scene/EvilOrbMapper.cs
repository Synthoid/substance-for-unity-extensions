using UnityEngine;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Examples
{
    /// <summary>
    /// Example class that handles retrieving output textures from a substance and mapping them to external material instances.
    /// </summary>
    public class EvilOrbMapper : MonoBehaviour
    {
        private const string kNormalMap = "_BumpMap";
        private const string kMetallicRoughMap = "_MetallicGlossMap";

        [SerializeField, Tooltip("Target the kanji will face.")]
        private Transform target = null;
        [SerializeField, Tooltip("Transform that will be oriented to face the target transform.")]
        private Transform kanjiRoot = null;
        [SerializeField, Tooltip("Substance to map textures from.")]
        private SubstanceGraphSO evilOrbSubstance = null;
        [SerializeField, Tooltip("Material for the evil orb's sphere.")]
        private Renderer sphereRenderer = null;
        [SerializeField, Tooltip("Material for the evil orb's kanji.")]
        private Renderer kanjiRenderer = null;
        [SerializeField, Tooltip("Output texture for the substance's basecolor.")]
        private SubstanceOutput colorOutput = new SubstanceOutput();
        [SerializeField, Tooltip("Output texture for the substance's normal.")]
        private SubstanceOutput normalOutput = new SubstanceOutput();
        [SerializeField, Tooltip("Output texture for the substance's metallic-roughness map.")]
        private SubstanceOutput metallicRoughOutput = new SubstanceOutput();

        /// <summary>
        /// Refresh the referenced textures on evil orb materials. Note: If the target substance has not been rendered, textures will appear blank.
        /// </summary>
        [ContextMenu("Refresh Materials (Runtime Only)", false)]
        public void RefreshMaterials()
        {
            Texture2D colorMap = evilOrbSubstance.GetOutputTexture(colorOutput.Name);
            Texture2D normalMap = evilOrbSubstance.GetOutputTexture(normalOutput.Name);
            Texture2D metallicRoughnessMap = evilOrbSubstance.GetOutputTexture(metallicRoughOutput.Name);

            sphereRenderer.material.mainTexture = colorMap;
            sphereRenderer.material.SetTexture(kNormalMap, normalMap);
            sphereRenderer.material.SetTexture(kMetallicRoughMap, metallicRoughnessMap);

            kanjiRenderer.material.mainTexture = colorMap;
            kanjiRenderer.material.SetTexture(kNormalMap, normalMap);
            kanjiRenderer.material.SetTexture(kMetallicRoughMap, metallicRoughnessMap);
        }

        [ContextMenu("Refresh Materials (Runtime Only)", true)]
        private bool RefreshMaterialsValidation()
        {
            return Application.isPlaying;
        }


        private void Start()
        {
            if(target == null) target = Camera.main.transform;
            if(kanjiRoot == null) kanjiRoot = transform;
        }


        private void Update()
        {
            if(target == null) return;

            kanjiRoot.LookAt(target);
        }
    }
}