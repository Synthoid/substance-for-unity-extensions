using UnityEngine;
using Adobe.Substance;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    [CreateAssetMenu(fileName="Runtime Control Data", menuName="SOS/Substance Extensions/Examples/Runtime/Control Data Asset")]
    public class RuntimeParameterControlsAsset : ScriptableObject
    {
        [Tooltip("Prefab returned when not valid control prefab is found.")]
        public InvalidParameterControl invalidControlPrefab = null;
        public RuntimeParameterControlGroupData[] controlGroups = new RuntimeParameterControlGroupData[0];


        public RuntimeParameterControlGroupData GetGroupData(SubstanceValueType valueType)
        {
            for(int i=0; i < controlGroups.Length; i++)
            {
                if(controlGroups[i].valueType == valueType) return controlGroups[i];
            }

            return null;
        }


        public RuntimeParameterControl GetControlPrefab(ISubstanceInput input)
        {
            RuntimeParameterControlGroupData controlGroup = GetGroupData(input.ValueType);

            if(controlGroup == null) return invalidControlPrefab;

            RuntimeParameterControl controlPrefab = controlGroup.GetControlPrefab(input);

            return controlPrefab != null ? controlPrefab : invalidControlPrefab;
        }
    }
}