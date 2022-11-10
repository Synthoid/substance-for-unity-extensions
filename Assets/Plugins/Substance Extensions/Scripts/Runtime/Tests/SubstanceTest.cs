using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    public class SubstanceTest : MonoBehaviour
    {
        [SerializeField]
        private SubstanceFileSO substanceGraph = null;
        [SerializeField]
        private SubstanceParameter parameter = new SubstanceParameter();
        [SerializeField]
        private SubstanceParameterValue paramValue = new SubstanceParameterValue();
        [SerializeField]
        private SubstanceOutput output = new SubstanceOutput();
        [SerializeField, TransformMatrix]
        private Vector4 transformMatrix = new Vector4(1, 0, 0, 1);


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