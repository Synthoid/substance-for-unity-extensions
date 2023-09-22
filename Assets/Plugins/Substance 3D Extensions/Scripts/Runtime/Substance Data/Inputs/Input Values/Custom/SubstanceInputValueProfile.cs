using UnityEngine;
using Adobe.Substance;

namespace SOS.SubstanceExtensions
{
    /// <summary>
    /// Profile of values that can be used to set input values of a SubstanceNativeGraph object.
    /// </summary>
    [CreateAssetMenu(fileName="Input Value Profile", menuName="SOS/Substance Extensions/Values/Profile")]
    public class SubstanceInputValueProfile : ScriptableObject
    {
        [SerializeField, SerializeReference, Tooltip("Values to set on a SubstanceNativeGraph.")]
        protected ISubstanceInputValue[] values = new ISubstanceInputValue[0];

        public ISubstanceInputValue[] Values
        {
            get { return values; }
            set { values = value; }
        }

        public virtual void SetValues(SubstanceNativeGraph graph)
        {
            for(int i = 0; i < values.Length; i++)
            {
                values[i].SetInputValue(graph);
            }
        }
    }
}