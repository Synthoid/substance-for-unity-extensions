using UnityEngine;
using UnityEditor;
using Adobe.Substance;

namespace SOS.SubstanceExtensions.Tests
{
    /// <summary>
    /// Asset containing values for <see cref="SubstanceGraphSO"/> runtime unit tests.
    /// </summary>
    [CreateAssetMenu(fileName="Substance Graph Runtime Test Asset", menuName="SOS/Substance/Tests/Substance Graph (Runtime)")]
    public class SubstanceGraphRuntimeTestAsset : UnitTestAsset
    {
        [SerializeField, Tooltip("Scene loaded for runtime tests.")]
        private SceneAsset testScene = null;
        [SerializeField, Tooltip("Texture shown when waiting for a render test's output texture.")]
        private Texture2D placeholderRenderTexture = null;
        [Header("Tests")]
        [SerializeField, Tooltip("Data for a unit test of a render focused on a string parameter.")]
        private SubstanceGraphRuntimeTestGroup stringTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a bool parameter.")]
        private SubstanceGraphRuntimeTestGroup boolTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on an enum parameter.")]
        private SubstanceGraphRuntimeTestGroup enumTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on an int parameter.")]
        private SubstanceGraphRuntimeTestGroup intTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on an int2 parameter.")]
        private SubstanceGraphRuntimeTestGroup int2Test = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on an int3 parameter.")]
        private SubstanceGraphRuntimeTestGroup int3Test = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on an int4 parameter.")]
        private SubstanceGraphRuntimeTestGroup int4Test = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a float parameter.")]
        private SubstanceGraphRuntimeTestGroup floatTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a float2 parameter.")]
        private SubstanceGraphRuntimeTestGroup float2Test = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a float3 parameter.")]
        private SubstanceGraphRuntimeTestGroup float3Test = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a color float4 parameter.")]
        private SubstanceGraphRuntimeTestGroup float4ColorTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a matrix float4 parameter.")]
        private SubstanceGraphRuntimeTestGroup float4MatrixTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a CPU texture parameter.")]
        private SubstanceGraphRuntimeTestGroup textureCPUTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a GPU texture parameter.")]
        private SubstanceGraphRuntimeTestGroup textureGPUTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on an output size parameter.")]
        private SubstanceGraphRuntimeTestGroup outputSizeTest = new SubstanceGraphRuntimeTestGroup();
        [SerializeField, Tooltip("Data for a unit test of a render focused on a random seed parameter.")]
        private SubstanceGraphRuntimeTestGroup randomSeedTest = new SubstanceGraphRuntimeTestGroup();
        //TODO: $time test?

        private static SubstanceGraphRuntimeTestAsset instance = null;

        public static SubstanceGraphRuntimeTestAsset Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = GetTestAsset<SubstanceGraphRuntimeTestAsset>();
                }

                return instance;
            }
        }


        public SceneAsset TestScene
        {
            get { return testScene; }
        }


        public Texture2D PlaceholderRenderTexture
        {
            get { return placeholderRenderTexture; }
        }


        public SubstanceGraphRuntimeTestGroup[] TestGroups
        {
            get
            {
                return new SubstanceGraphRuntimeTestGroup[]
                {
                    stringTest, boolTest, enumTest,
                    intTest, int2Test, int3Test, Int4Test,
                    floatTest, float2Test, float3Test, float4ColorTest,
                    textureCPUTest, outputSizeTest, randomSeedTest
                };
            }
        }


        /*public SubstanceGraphSO Substance
        {
            get { return substance; }
        }*/

        public SubstanceGraphRuntimeTestGroup StringTest
        {
            get { return stringTest; }
        }


        public SubstanceGraphRuntimeTestGroup BoolTest
        {
            get { return boolTest; }
        }


        public SubstanceGraphRuntimeTestGroup EnumTest
        {
            get { return enumTest; }
        }


        public SubstanceGraphRuntimeTestGroup IntTest
        {
            get { return intTest; }
        }


        public SubstanceGraphRuntimeTestGroup Int2Test
        {
            get { return int2Test; }
        }


        public SubstanceGraphRuntimeTestGroup Int3Test
        {
            get { return int3Test; }
        }


        public SubstanceGraphRuntimeTestGroup Int4Test
        {
            get { return int4Test; }
        }


        public SubstanceGraphRuntimeTestGroup FloatTest
        {
            get { return floatTest; }
        }


        public SubstanceGraphRuntimeTestGroup Float2Test
        {
            get { return float2Test; }
        }


        public SubstanceGraphRuntimeTestGroup Float3Test
        {
            get { return float3Test; }
        }


        public SubstanceGraphRuntimeTestGroup Float4ColorTest
        {
            get { return float4ColorTest; }
        }


        public SubstanceGraphRuntimeTestGroup Float4MatrixTest
        {
            get { return float4MatrixTest; }
        }


        public SubstanceGraphRuntimeTestGroup TextureCPUTest
        {
            get { return textureCPUTest; }
        }


        public SubstanceGraphRuntimeTestGroup TextureGPUTest
        {
            get { return textureGPUTest; }
        }


        public SubstanceGraphRuntimeTestGroup OutputSizeTest
        {
            get { return outputSizeTest; }
        }


        public SubstanceGraphRuntimeTestGroup RandomSeedTest
        {
            get { return randomSeedTest; }
        }


        /*public SubstanceParameterValue DefaultStringValue
        {
            get { return defaultStringValue; }
        }


        public SubstanceParameterValue DefaultBoolValue
        {
            get { return defaultBoolValue; }
        }


        public SubstanceParameterValue DefaultEnumValue
        {
            get { return defaultEnumValue; }
        }


        public TestInputEnum DefaultEnumCastValue
        {
            get { return defaultEnumCastValue; }
        }


        public SubstanceParameterValue DefaultIntValue
        {
            get { return defaultIntValue; }
        }


        public SubstanceParameterValue DefaultInt2Value
        {
            get { return defaultInt2Value; }
        }


        public SubstanceParameterValue DefaultInt3Value
        {
            get { return defaultInt3Value; }
        }


        public SubstanceParameterValue DefaultInt4Value
        {
            get { return defaultInt4Value; }
        }


        public SubstanceParameterValue DefaultFloatValue
        {
            get { return defaultFloatValue; }
        }


        public SubstanceParameterValue DefaultFloat2Value
        {
            get { return defaultFloat2Value; }
        }


        public SubstanceParameterValue DefaultFloat3Value
        {
            get { return defaultFloat3Value; }
        }


        public SubstanceParameterValue DefaultFloat4Value
        {
            get { return defaultFloat4Value; }
        }


        public SubstanceParameterValue DefaultTextureValue
        {
            get { return defaultTextureValue; }
        }


        public SubstanceParameterValue DefaultTextureNullValue
        {
            get { return defaultTextureNullValue; }
        }


        public SubstanceParameterValue DefaultOutputSizeValue
        {
            get { return defaultOutputSizeValue; }
        }


        public SubstanceParameterValue DefaultRandomSeedValue
        {
            get { return defaultRandomSeedValue; }
        }


        public SubstanceParameterValue SetStringValue
        {
            get { return setStringValue; }
        }


        public SubstanceParameterValue SetBoolValue
        {
            get { return setBoolValue; }
        }


        public SubstanceParameterValue SetEnumValue
        {
            get { return setEnumValue; }
        }


        public TestInputEnum SetEnumCastValue
        {
            get { return setEnumCastValue; }
        }


        public SubstanceParameterValue SetIntValue
        {
            get { return setIntValue; }
        }


        public SubstanceParameterValue SetInt2Value
        {
            get { return setInt2Value; }
        }


        public SubstanceParameterValue SetInt3Value
        {
            get { return setInt3Value; }
        }


        public SubstanceParameterValue SetInt4Value
        {
            get { return setInt4Value; }
        }


        public SubstanceParameterValue SetFloatValue
        {
            get { return setFloatValue; }
        }


        public SubstanceParameterValue SetFloat2Value
        {
            get { return setFloat2Value; }
        }


        public SubstanceParameterValue SetFloat3Value
        {
            get { return setFloat3Value; }
        }


        public SubstanceParameterValue SetFloat4Value
        {
            get { return setFloat4Value; }
        }


        public SubstanceParameterValue SetTextureValue
        {
            get { return setTextureValue; }
        }


        public SubstanceParameterValue SetTextureNullValue
        {
            get { return setTextureNullValue; }
        }


        public SubstanceParameterValue SetOutputSizeValue
        {
            get { return setOutputSizeValue; }
        }


        public SubstanceParameterValue SetRandomSeedValue
        {
            get { return setRandomSeedValue; }
        }*/
    }
}