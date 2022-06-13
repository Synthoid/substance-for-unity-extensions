using UnityEngine;
using SOS.SubstanceExtensions;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Tests
{
    [CreateAssetMenu(fileName = "Substance File Test Asset", menuName = "SOS/Substance/Tests/Substance File")]
    public class SubstanceFileTestAsset : UnitTestAsset
    {
        [SerializeField, Tooltip("Substance used for testing.")]
        private SubstanceFileSO substance = null;
        [SerializeField, Tooltip("Expected value for the target string input on the test substance.")]
        private SubstanceParameterValue stringValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target bool input on the test substance.")]
        private SubstanceParameterValue boolValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target enum input on the test substance.")]
        private SubstanceParameterValue enumValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int input on the test substance.")]
        private SubstanceParameterValue intValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int2 input on the test substance.")]
        private SubstanceParameterValue int2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int3 input on the test substance.")]
        private SubstanceParameterValue int3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target int4 input on the test substance.")]
        private SubstanceParameterValue int4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float input on the test substance.")]
        private SubstanceParameterValue floatValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float2 input on the test substance.")]
        private SubstanceParameterValue float2Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float3 input on the test substance.")]
        private SubstanceParameterValue float3Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target float4 input on the test substance.")]
        private SubstanceParameterValue float4Value = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target output size input on the test substance.")]
        private SubstanceParameterValue outputSizeValue = new SubstanceParameterValue();
        [SerializeField, Tooltip("Expected value for the target random seed input on the test substance.")]
        private SubstanceParameterValue randomSeedValue = new SubstanceParameterValue();

        private static SubstanceFileTestAsset instance = null;

        private static SubstanceFileTestAsset Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetTestAsset<SubstanceFileTestAsset>();
                }

                return instance;
            }
        }


        public static SubstanceFileSO Substance
        {
            get { return Instance != null ? Instance.substance : null; }
        }


        public static SubstanceParameterValue StringValue
        {
            get { return Instance != null ? Instance.stringValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue BoolValue
        {
            get { return Instance != null ? Instance.boolValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue EnumValue
        {
            get { return Instance != null ? Instance.enumValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue IntValue
        {
            get { return Instance != null ? Instance.intValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue Int2Value
        {
            get { return Instance != null ? Instance.int2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue Int3Value
        {
            get { return Instance != null ? Instance.int3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue Int4Value
        {
            get { return Instance != null ? Instance.int4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue FloatValue
        {
            get { return Instance != null ? Instance.floatValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue Float2Value
        {
            get { return Instance != null ? Instance.float2Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue Float3Value
        {
            get { return Instance != null ? Instance.float3Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue Float4Value
        {
            get { return Instance != null ? Instance.float4Value : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue OutputSizeValue
        {
            get { return Instance != null ? Instance.outputSizeValue : default(SubstanceParameterValue); }
        }


        public static SubstanceParameterValue RandomSeedValue
        {
            get { return Instance != null ? Instance.randomSeedValue : default(SubstanceParameterValue); }
        }
    }
}