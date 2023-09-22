using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Collections.Generic;
using System.Reflection;
using SOS.SubstanceExtensions;

namespace SOS.SubstanceExtensionsEditor
{
    [CustomEditor(typeof(SRG_InputValueProfile))]
    public class SRG_InputValueProfileEditor : Editor
    {
        protected readonly GUIContent kValuesLabel = new GUIContent("Values", "Values to set on a SubstanceRuntimeGraph.");

        protected SerializedProperty m_Script = null;
        protected SerializedProperty m_Values = null;

        protected ReorderableList valuesList = null;
        protected List<AddMenuData> addMenuData = null;

        protected virtual void OnEnable()
        {
            m_Script = serializedObject.FindProperty("m_Script");
            m_Values = serializedObject.FindProperty("values");

            valuesList = new ReorderableList(serializedObject, m_Values)
            {
                drawHeaderCallback = DrawValuesHeader,
                drawElementCallback = DrawValueElement,
                elementHeightCallback = GetValueHeight,
                onAddDropdownCallback = OnAddDropdownClicked
            };

            addMenuData = SubstanceExtensionsEditorUtility.GetAddMenuData(typeof(ISubstanceRuntimeGraphInputValue));
        }


        public override void OnInspectorGUI()
        {
            using(new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.PropertyField(m_Script);
            }

            serializedObject.Update();

            valuesList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }


        protected void DrawValuesHeader(Rect position)
        {
            EditorGUI.LabelField(position, kValuesLabel);
        }


        protected void DrawValueElement(Rect position, int index, bool isActive, bool isFocused)
        {
            EditorGUI.PropertyField(position, m_Values.GetArrayElementAtIndex(index), true);
        }


        protected float GetValueHeight(int index)
        {
            return EditorGUI.GetPropertyHeight(m_Values.GetArrayElementAtIndex(index), true);
        }


        protected void OnAddDropdownClicked(Rect position, ReorderableList list)
        {
            GenericMenu menu = new GenericMenu();

            for(int i=0; i < addMenuData.Count; i++)
            {
                int index = i;

                menu.AddItem(addMenuData[i].PathLabel, false, () => { AddValue(index); });
            }

            menu.DropDown(position);
        }


        protected void AddValue(int index)
        {
            Type valueType = addMenuData[index].Type;

            m_Values.InsertArrayElementAtIndex(m_Values.arraySize);

            SerializedProperty newValue = m_Values.GetArrayElementAtIndex(m_Values.arraySize - 1);

            ConstructorInfo constructorInfo = valueType.GetConstructor(new Type[0]);

            newValue.managedReferenceValue = (ISubstanceRuntimeGraphInputValue)constructorInfo.Invoke(new object[0]);

            serializedObject.ApplyModifiedProperties();
        }
    }
}