using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Adobe.Substance;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensions.Tests
{
    public class SubstanceFileTests
    {
        [SetUp, OneTimeTearDown]
        public void SetupAndTearDown()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            SubstanceParameterValue defaultStringValue = SubstanceFileTestAsset.DefaultStringValue;
            SubstanceParameterValue defaultBoolValue = SubstanceFileTestAsset.DefaultBoolValue;
            SubstanceParameterValue defaultEnumValue = SubstanceFileTestAsset.DefaultEnumValue;
            SubstanceParameterValue defaultIntValue = SubstanceFileTestAsset.DefaultIntValue;
            SubstanceParameterValue defaultInt2Value = SubstanceFileTestAsset.DefaultInt2Value;
            SubstanceParameterValue defaultInt3Value = SubstanceFileTestAsset.DefaultInt3Value;
            SubstanceParameterValue defaultInt4Value = SubstanceFileTestAsset.DefaultInt4Value;
            SubstanceParameterValue defaultFloatValue = SubstanceFileTestAsset.DefaultFloatValue;
            SubstanceParameterValue defaultFloat2Value = SubstanceFileTestAsset.DefaultFloat2Value;
            SubstanceParameterValue defaultFloat3Value = SubstanceFileTestAsset.DefaultFloat3Value;
            SubstanceParameterValue defaultFloat4Value = SubstanceFileTestAsset.DefaultFloat4Value;
            SubstanceParameterValue defaultTextureValue = SubstanceFileTestAsset.DefaultTextureValue;
            SubstanceParameterValue defaultNullTextureValue = SubstanceFileTestAsset.DefaultTextureNullValue;

            substance.SetString(defaultStringValue.StringValue, defaultStringValue.Parameter);
            substance.SetBool(defaultBoolValue.BoolValue, defaultBoolValue.Parameter);
            substance.SetInt(defaultEnumValue.IntValue, defaultEnumValue.Parameter);
            substance.SetInt(defaultIntValue.IntValue, defaultIntValue.Parameter);
            substance.SetInt2(defaultInt2Value.Int2Value, defaultInt2Value.Parameter);
            substance.SetInt3(defaultInt3Value.Int3Value, defaultInt3Value.Parameter);
            substance.SetInt4(defaultInt4Value.Int4Value, defaultInt4Value.Parameter);
            substance.SetFloat(defaultFloatValue.IntValue, defaultFloatValue.Parameter);
            substance.SetFloat2(defaultFloat2Value.Float2Value, defaultFloat2Value.Parameter);
            substance.SetFloat3(defaultFloat3Value.Float3Value, defaultFloat3Value.Parameter);
            substance.SetFloat4(defaultFloat4Value.Float4Value, defaultFloat4Value.Parameter);
            substance.SetTexture(defaultTextureValue.TextureValue, defaultTextureValue.Parameter);
            substance.SetTexture(defaultNullTextureValue.TextureValue, defaultNullTextureValue.Parameter);
        }

        /*#region Utility

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a graph index on a target SubstanceGraphSO asset can properly be returned by referencing a graph guid.")]
        public void GetGraphIndex()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.SetStringValue;

            int index = substance.GetGraphIndex(parameterValue.Parameter.GraphGuid);

            Assert.AreEqual(1, index);
        }

        #endregion*/

        #region String

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetString()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetString()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultStringValue;

            bool success = substance.TryGetString(out string inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default string value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetStringFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetString(out string inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual("", inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetString()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetStringValue;

            substance.SetString(setValue.StringValue, setValue.Parameter);

            inputValue = substance.GetString(setValue.Parameter);

            Assert.AreEqual(setValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a string input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetString()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetStringValue;

            bool success = substance.TrySetString(setValue.StringValue, setValue.Parameter);

            inputValue = substance.GetString(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.StringValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant string input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetStringFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultStringValue;

            string inputValue = substance.GetString(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.StringValue, inputValue);

            bool success = substance.TrySetString("Master Betty, what do we do?", "I am a great magician.");

            substance.TryGetString(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual("", inputValue);
        }

        #endregion

        #region Int

        #region Bool

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetBool()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetBool()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultBoolValue;

            bool success = substance.TryGetBool(out bool inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default bool value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetBoolFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetBool(out bool inputValue, "Your clothes are black.");

            Assert.IsFalse(success);
            Assert.AreEqual(false, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetBool()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.BoolValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetBoolValue;

            substance.SetBool(setValue.BoolValue, setValue.Parameter);

            inputValue = substance.GetBool(setValue.Parameter);

            Assert.AreEqual(setValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a bool input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetBool()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.BoolValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetBoolValue;

            bool success = substance.TrySetBool(setValue.BoolValue, setValue.Parameter);

            inputValue = substance.GetBool(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.BoolValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant bool input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetBoolFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultBoolValue;

            bool inputValue = substance.GetBool(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.BoolValue, inputValue);

            bool success = substance.TrySetBool(true, "I am a great magician.");

            substance.TryGetBool(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.IsFalse(inputValue);
        }

        #endregion

        #region Enum

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetEnum()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultEnumValue;

            int inputValue = substance.GetInt(parameterValue.Index);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceGraphSO asset and cast to a specific enum type.")]
        public void GetEnumCast()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultEnumValue;
            TestInputEnum enumValue = SubstanceFileTestAsset.DefaultEnumCastValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(parameterValue.Parameter);

            Assert.AreEqual(enumValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum int input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetEnumCast()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultEnumValue;
            TestInputEnum enumValue = SubstanceFileTestAsset.DefaultEnumCastValue;

            bool success = substance.TryGetEnum<TestInputEnum>(out TestInputEnum inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(enumValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default enum value can properly be returned when referencing a non-existant enum input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetEnumCastFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            TestInputEnum defaultValue = TestInputEnum.Blue;

            bool success = substance.TryGetEnum<TestInputEnum>(out TestInputEnum inputValue, "Red clothes!", defaultValue);

            Assert.IsFalse(success);
            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetEnum()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultEnumValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(defaultValue.Parameter);

            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetEnumValue;
            TestInputEnum enumValue = SubstanceFileTestAsset.SetEnumCastValue;

            substance.SetEnum(enumValue, setValue.Parameter);

            inputValue = substance.GetEnum<TestInputEnum>(setValue.Parameter);

            Assert.AreEqual((TestInputEnum)setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an enum input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetEnum()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultEnumValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(defaultValue.Parameter);

            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetEnumValue;

            bool success = substance.TrySetEnum<TestInputEnum>((TestInputEnum)setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetEnum<TestInputEnum>(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual((TestInputEnum)setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant enum input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetEnumFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultEnumValue;

            TestInputEnum inputValue = substance.GetEnum<TestInputEnum>(defaultValue.Parameter);

            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);

            bool success = substance.TrySetEnum<TestInputEnum>(TestInputEnum.Black, "I am a great magician.");

            substance.TryGetEnum<TestInputEnum>(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual((TestInputEnum)defaultValue.IntValue, inputValue);
        }

        #endregion

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultIntValue;

            bool success = substance.TryGetInt(out int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetIntFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt(out int inputValue, "Red clothes!");

            Assert.IsFalse(success);
            Assert.AreEqual(0, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetIntValue;

            substance.SetInt(setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetInt(setValue.Parameter);

            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetIntValue;

            bool success = substance.TrySetInt(setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetInt(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetIntFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            bool success = substance.TrySetInt(1, "I am a great magician.");

            substance.TryGetInt(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(0, inputValue);
        }

        #endregion

        #region Int2

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt2()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt2Value;

            Vector2Int inputValue = substance.GetInt2(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt2()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt2Value;

            bool success = substance.TryGetInt2(out Vector2Int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int2 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt2Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt2(out Vector2Int inputValue, "That's a lot of nuts!");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2Int.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt2()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultIntValue;

            int inputValue = substance.GetInt(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.IntValue, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetIntValue;

            substance.SetInt(setValue.IntValue, setValue.Parameter);

            inputValue = substance.GetInt(setValue.Parameter);

            Assert.AreEqual(setValue.IntValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int2 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt2()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt2Value;

            Vector2Int inputValue = substance.GetInt2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int2Value, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetInt2Value;

            bool success = substance.TrySetInt2(setValue.Int2Value, setValue.Parameter);

            inputValue = substance.GetInt2(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Int2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int2 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt2Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt2Value;

            Vector2Int inputValue = substance.GetInt2(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int2Value, inputValue);

            bool success = substance.TrySetInt2(Vector2Int.one, "I am a great magician.");

            substance.TryGetInt2(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2Int.zero, inputValue);
        }

        #endregion

        #region Int3

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt3()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt3()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt3Value;

            bool success = substance.TryGetInt3(out Vector3Int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int3 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt3Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt3(out Vector3Int inputValue, "Ventriloquists, huh?");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3Int.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt3()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int3Value, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetInt3Value;

            substance.SetInt3(setValue.Int3Value, setValue.Parameter);

            inputValue = substance.GetInt3(setValue.Parameter);

            Assert.AreEqual(setValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int3 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt3()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int3Value, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetInt3Value;

            bool success = substance.TrySetInt3(setValue.Int3Value, setValue.Parameter);

            inputValue = substance.GetInt3(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Int3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int3 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt3Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt3Value;

            Vector3Int inputValue = substance.GetInt3(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int3Value, inputValue);

            bool success = substance.TrySetInt3(Vector3Int.one, "I am a great magician.");

            substance.TryGetInt3(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3Int.zero, inputValue);
        }

        #endregion

        #region Int4

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt4()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt4Value;

            int[] defaultValue = new int[4] { parameterValue.Int4Value.x, parameterValue.Int4Value.y, parameterValue.Int4Value.z, parameterValue.Int4Value.w };
            int[] inputValue = substance.GetInt4(parameterValue.Parameter);

            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt4Value;
            int[] defaultValue = new int[4] { parameterValue.Int4Value.x, parameterValue.Int4Value.y, parameterValue.Int4Value.z, parameterValue.Int4Value.w };

            bool success = substance.TryGetInt4(out int[] inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int4 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            int[] defaultValue = new int[4] { 0, 0, 0, 0 };

            bool success = substance.TryGetInt4(out int[] inputValue, "Face to foot style!");

            Assert.IsFalse(success);
            Assert.AreEqual(defaultValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetInt4Vector()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4Vector()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultInt4Value;

            bool success = substance.TryGetInt4Vector(out Vector4Int inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default int4 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetInt4VectorFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetInt4Vector(out Vector4Int inputValue, "Face to foot style!");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4Int.zero, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be set on a target SubstanceGraphSO asset.")]
        public void SetInt4()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int4Value, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetInt4Value;

            substance.SetInt4(setValue.Int4Value, setValue.Parameter);

            inputValue = substance.GetInt4Vector(setValue.Parameter);

            Assert.AreEqual(setValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an int4 input value can properly be set on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt4()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int4Value, inputValue);

            SubstanceParameterValue setValue = SubstanceFileTestAsset.SetInt4Value;

            bool success = substance.TrySetInt4(setValue.Int4Value, setValue.Parameter);

            inputValue = substance.GetInt4Vector(setValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(setValue.Int4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if attempting to set a non-existant int4 input value properly returns false when setting its value on a target SubstanceGraphSO asset using the try set method.")]
        public void TrySetInt4Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue defaultValue = SubstanceFileTestAsset.DefaultInt4Value;

            Vector4Int inputValue = substance.GetInt4Vector(defaultValue.Parameter);

            Assert.AreEqual(defaultValue.Int4Value, inputValue);

            bool success = substance.TrySetInt4(Vector4Int.one, "I am a great magician.");

            substance.TryGetInt4Vector(out inputValue, "I am a great magician.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4Int.zero, inputValue);
        }

        #endregion

        #region Float

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloatValue;

            float inputValue = substance.GetFloat(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloatValue;

            bool success = substance.TryGetFloat(out float inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.FloatValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloatFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat(out float inputValue, "And so, he walked.");

            Assert.IsFalse(success);
            Assert.AreEqual(0f, inputValue);
        }

        #endregion

        #region Float2

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat2()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloat2Value;

            Vector2 inputValue = substance.GetFloat2(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float2 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat2()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloat2Value;

            bool success = substance.TryGetFloat2(out Vector2 inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float2Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float2 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat2Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat2(out Vector2 inputValue, "And sometimes, drove.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector2.zero, inputValue);
        }

        #endregion

        #region Float3

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat3()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloat3Value;

            Vector3 inputValue = substance.GetFloat3(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float3 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat3()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloat3Value;

            bool success = substance.TryGetFloat3(out Vector3 inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float3Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float3 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat3Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat3(out Vector3 inputValue, "I mean, I'm no doctor.");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector3.zero, inputValue);
        }

        #endregion

        #region Float4

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetFloat4()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloat4Value;

            Vector4 inputValue = substance.GetFloat4(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a float4 input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat4()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultFloat4Value;

            bool success = substance.TryGetFloat4(out Vector4 inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.Float4Value, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default float4 value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetFloat4Fail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetFloat4(out Vector4 inputValue, "It was like, one clean chunk!");

            Assert.IsFalse(success);
            Assert.AreEqual(Vector4.zero, inputValue);
        }

        #endregion

        #region Texture

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a texture input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetTexture()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultTextureValue;

            Texture2D inputValue = substance.GetTexture(parameterValue.Parameter);

            Assert.AreEqual(parameterValue.TextureValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a null texture input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetNullTexture()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultTextureNullValue;

            Texture2D inputValue = substance.GetTexture(parameterValue.Parameter);

            Assert.IsNull(inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a texture input value can properly be referenced on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetTexture()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultTextureValue;

            bool success = substance.TryGetTexture(out Texture2D inputValue, parameterValue.Parameter);

            Assert.IsTrue(success);
            Assert.AreEqual(parameterValue.TextureValue, inputValue);
        }

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a default texture value can properly be returned when referencing a non-existant input on a target SubstanceGraphSO asset using the try get method.")]
        public void TryGetTextureFail()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;

            bool success = substance.TryGetTexture(out Texture2D inputValue, "I mean, I'm no doctor.");

            Assert.IsFalse(success);
            Assert.IsNull(inputValue);
        }

        #endregion

        #region Output Size

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if an $outputsize input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetOutputSize()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultOutputSizeValue;

            Vector2Int inputValue = substance.GetOutputSize();

            Assert.AreEqual(parameterValue.Int2Value, inputValue);
        }

        #endregion

        #region Random Seed

        [Test, TestOf(typeof(SubstanceGraphExtensions)), Author("Chris Ingerson"), Description("Tests if a $randomseed input value can properly be referenced on a target SubstanceGraphSO asset.")]
        public void GetRandomSeed()
        {
            SubstanceGraphSO substance = SubstanceFileTestAsset.Substance;
            SubstanceParameterValue parameterValue = SubstanceFileTestAsset.DefaultRandomSeedValue;

            int inputValue = substance.GetRandomSeed();

            Assert.AreEqual(parameterValue.IntValue, inputValue);
        }

        #endregion

        /*[UnityTest]
        public IEnumerator SubstanceFileTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }*/
    }
}