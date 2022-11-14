using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    public class SubstanceTest : MonoBehaviour
    {
        [SerializeField]
        public SubstanceFileSO substanceGraph = null;
        [SerializeField]
        public SubstanceParameter parameter = new SubstanceParameter();
        [SerializeField]
        public SubstanceParameterValue paramValue = new SubstanceParameterValue();
        [SerializeField]
        public SubstanceOutput output = new SubstanceOutput();
        [SerializeField, TransformMatrix]
        public Vector4 transformMatrix = new Vector4(1, 0, 0, 1);


        private void UpdateSubstance()
        {
            Debug.Log(substanceGraph.name);
            //UnityEditor.AssetDatabase.GetAssetPath()
            //substanceGraph.AssetPath
            //substanceGraph.Graphs[0].Input[0].Description.Identifier
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                UpdateSubstance();
            }
        }
    }
}