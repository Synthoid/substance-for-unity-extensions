using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Tests
{
    //TODO: Write tests for TryGet methods...
    public class SubstanceFileTests
    {
        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetString()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.StringValue;

            string inputValue = substance.GetString(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetBool()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.BoolValue;

            bool inputValue = substance.GetBool(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetEnum()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.EnumValue;

            int inputValue = substance.GetInt(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.IntValue;

            int inputValue = substance.GetInt(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt2()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.Int2Value;

            Vector2Int inputValue = substance.GetInt2(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt3()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.Int3Value;

            Vector3Int inputValue = substance.GetInt3(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetInt4()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.Int4Value;

            Vector4Int inputValue = substance.GetInt4(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.FloatValue;

            float inputValue = substance.GetFloat(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat2()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.Float2Value;

            Vector2 inputValue = substance.GetFloat2(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat3()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.Float3Value;

            Vector3 inputValue = substance.GetFloat3(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetFloat4()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.Float4Value;

            Vector4 inputValue = substance.GetFloat4(parameterValue.Index, parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if an $outputsize input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetOutputSize()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.OutputSizeValue;

            Vector2Int inputValue = substance.GetOutputSize(parameterValue.GraphId);

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceFileExtensions)), Author("Chris Ingerson"), Description("Tests if a $randomseed input value can properly be referenced on a target SubstanceFileSO asset.")]
        public void GetRandomSeed()
        {
            SubstanceFileSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.RandomSeedValue;

            int inputValue = substance.GetRandomSeed(parameterValue.GraphId);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        /*[UnityTest]
        public IEnumerator SubstanceFileTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }*/
    }
}