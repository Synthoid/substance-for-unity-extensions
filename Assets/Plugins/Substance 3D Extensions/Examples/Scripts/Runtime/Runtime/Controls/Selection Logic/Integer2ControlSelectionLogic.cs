using UnityEngine;
using Adobe.Substance.Input;

namespace SOS.SubstanceExtensions.Examples
{
    [CreateAssetMenu(fileName="Control Select - Integer2", menuName="SOS/Substance Extensions/Examples/Runtime/Control Select Logic/Integer2")]
    public class Integer2ControlSelectionLogic : InputControlSelectLogic
    {
        public override bool TryGetControl(ISubstanceInput input, RuntimeParameterControlData[] controls, out RuntimeParameterControl control)
        {
            if(input.Description.Identifier == SubstanceNativeGraphExtensions.kOutputSizeIdentifier)
            {
                for(int i=0; i < controls.Length; i++)
                {
                    if (controls[i].prefab is OutputSizeControl)
                    {
                        control = controls[i].prefab;
                        return true;
                    }
                }
            }

            control = null;

            return false;
        }
    }
}