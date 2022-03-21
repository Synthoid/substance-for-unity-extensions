using UnityEngine;
using SOS.SubstanceExtensions;
using Adobe.Substance;
using Adobe.Substance.Input;
using Adobe.Substance.Input.Description;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Contains relevant data for a substance input parameter to make editor tooling easier.
    /// </summary>
    public struct SubstanceParameterData
    {
        public string name;
        public int index;
        public SubstanceValueType type;
        public SubstanceWidgetType widget;
        public Vector4 rangeMin;
        public Vector4 rangeMax;
        public Vector4Int rangeIntMin;
        public Vector4Int rangeIntMax;

        public SubstanceParameterData(ISubstanceInput input)
        {
            this.name = input.Description.Identifier;
            this.index = input.Index;
            this.type = input.ValueType;
            this.widget = input.Description.WidgetType;

            if(input.IsNumeric)
            {
                switch(input.NumericalDescription)
                {
                    case SubstanceInputDescNumericalFloat floatDesc:
                        rangeMin = new Vector4(floatDesc.MinValue, 0f, 0f, 0f);
                        rangeMax = new Vector4(floatDesc.MaxValue, 0f, 0f, 0f);
                        rangeIntMin = Vector4Int.zero;
                        rangeIntMax = Vector4Int.zero;
                        break;
                    case SubstanceInputDescNumericalFloat2 float2Desc:
                        rangeMin = float2Desc.MinValue;
                        rangeMax = float2Desc.MaxValue;
                        rangeIntMin = Vector4Int.zero;
                        rangeIntMax = Vector4Int.zero;
                        break;
                    case SubstanceInputDescNumericalFloat3 float3Desc:
                        rangeMin = float3Desc.MinValue;
                        rangeMax = float3Desc.MaxValue;
                        rangeIntMin = Vector4Int.zero;
                        rangeIntMax = Vector4Int.zero;
                        break;
                    case SubstanceInputDescNumericalFloat4 float4Desc:
                        rangeMin = float4Desc.MinValue;
                        rangeMax = float4Desc.MaxValue;
                        rangeIntMin = Vector4Int.zero;
                        rangeIntMax = Vector4Int.zero;
                        break;
                    case SubstanceInputDescNumericalInt intDesc:
                        rangeMin = Vector4.zero;
                        rangeMax = Vector4.zero;
                        rangeIntMin = new Vector4Int(intDesc.MinValue, 0, 0, 0);
                        rangeIntMax = new Vector4Int(intDesc.MaxValue, 0, 0, 0);
                        break;
                    case SubstanceInputDescNumericalInt2 int2Desc:
                        rangeMin = Vector4.zero;
                        rangeMax = Vector4.zero;
                        rangeIntMin = new Vector4Int(int2Desc.MinValue.x, int2Desc.MinValue.y, 0, 0);
                        rangeIntMax = new Vector4Int(int2Desc.MaxValue.x, int2Desc.MaxValue.y, 0, 0);
                        break;
                    case SubstanceInputDescNumericalInt3 int3Desc:
                        rangeMin = Vector4.zero;
                        rangeMax = Vector4.zero;
                        rangeIntMin = new Vector4Int(int3Desc.MinValue.x, int3Desc.MinValue.y, int3Desc.MinValue.z, 0);
                        rangeIntMax = new Vector4Int(int3Desc.MaxValue.x, int3Desc.MaxValue.y, int3Desc.MaxValue.z, 0);
                        break;
                    case SubstanceInputDescNumericalInt4 int4Desc:
                        rangeMin = Vector4.zero;
                        rangeMax = Vector4.zero;
                        rangeIntMin = new Vector4Int(int4Desc.MinValue[0], int4Desc.MinValue[1], int4Desc.MinValue[2], int4Desc.MinValue[3]);
                        rangeIntMax = new Vector4Int(int4Desc.MaxValue[0], int4Desc.MaxValue[1], int4Desc.MaxValue[2], int4Desc.MaxValue[3]);

                        Debug.Log(rangeIntMin.ToString());
                        Debug.Log(rangeIntMax.ToString());
                        break;
                    default:
                        rangeMin = Vector4.zero;
                        rangeMax = Vector4.zero;
                        rangeIntMin = Vector4Int.zero;
                        rangeIntMax = Vector4Int.zero;
                        break;
                }
            }
            else
            {
                rangeMin = Vector4.zero;
                rangeMax = Vector4.zero;
                rangeIntMin = Vector4Int.zero;
                rangeIntMax = Vector4Int.zero;
            }
        }
    }
}