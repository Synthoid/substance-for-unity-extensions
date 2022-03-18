using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    public class SubstanceTest : MonoBehaviour
    {
        [SerializeField]
        private SubstanceMaterialInstanceSO substanceGraph = null;
        [SerializeField]
        private SubstanceParameter parameter = new SubstanceParameter();


        private void UpdateSubstance()
        {
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