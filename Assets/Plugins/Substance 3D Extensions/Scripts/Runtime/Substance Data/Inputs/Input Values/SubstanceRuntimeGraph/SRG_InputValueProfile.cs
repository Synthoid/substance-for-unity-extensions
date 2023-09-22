using UnityEngine;
using Adobe.Substance.Runtime;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Profile of values that can set input values of a SubstanceRuntimeGraph component.
    /// </summary>
    [CreateAssetMenu(fileName="SRG Input Value Profile", menuName="SOS/Substance Extensions/Values/Profile (SubstanceRuntimeGraph)")]
    public class SRG_InputValueProfile : ScriptableObject
    {
        [SerializeField, SerializeReference, Tooltip("Values to set on a SubstanceRuntimeGraph.")]
        protected ISubstanceRuntimeGraphInputValue[] values = new ISubstanceRuntimeGraphInputValue[0];

        public ISubstanceRuntimeGraphInputValue[] Values
        {
            get { return values; }
            set { values = value; }
        }

        /// <summary>
        /// Set the input values of the given runtime graph using values from this profile.
        /// </summary>
        /// <param name="graph">Graph to set inputs on.</param>
        public virtual void SetValues(SubstanceRuntimeGraph graph)
        {
            for(int i=0; i < values.Length; i++)
            {
                values[i].SetInputValue(graph);
            }
        }
    }
}