using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.EaseFunctions.Editor
{
    [CustomPropertyDrawer(typeof(EaseFunction))]
    public class EaseFunctionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentRect = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            GUIContent[] labels = new[] { new GUIContent(string.Empty), new GUIContent(string.Empty) };
            SerializedProperty[] properties = new[] { property.FindPropertyRelative("easingType"), property.FindPropertyRelative("functionType") };
            DrawMultiplePropertyFields(contentRect, labels, properties);

            EditorGUI.EndProperty();
        }

        private const float SubLabelSpacing = 4;
        public static void DrawMultiplePropertyFields(Rect pos, GUIContent[] subLabels, SerializedProperty[] props)
        {
            // backup gui settings
            int indent = EditorGUI.indentLevel;
            float labelWidth = EditorGUIUtility.labelWidth;

            // draw properties
            int propsCount = props.Length;
            float width = (pos.width - ((propsCount - 1) * SubLabelSpacing)) / propsCount;
            Rect contentPos = new Rect(pos.x, pos.y, width, pos.height);
            EditorGUI.indentLevel = 0;
            for (int i = 0; i < propsCount; i++)
            {
                EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(subLabels[i]).x + 2;
                EditorGUI.PropertyField(contentPos, props[i], subLabels[i]);
                contentPos.x += width + SubLabelSpacing;
            }

            // restore gui settings
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.indentLevel = indent;
        }
        //public override VisualElement CreatePropertyGUI(SerializedProperty property)
        //{
        //    VisualElement preContainer = new VisualElement();
        //    VisualElement container = new VisualElement();

        //    Label label = new Label(property.displayName);
        //    label.style.width = new StyleLength(110);
        //    preContainer.style.flexDirection = FlexDirection.Row;
        //    container.style.flexDirection = FlexDirection.Row;

        //    functionTypeProp = property.FindPropertyRelative("functionType");
        //    PropertyField functionField = new PropertyField(functionTypeProp)
        //    {
        //        label = string.Empty
        //    };
        //    functionField.style.width = new StyleLength(80);

        //    functionField.BindProperty(functionTypeProp);
        //    functionField.RegisterCallback<SerializedPropertyChangeEvent>(FunctionChanged, TrickleDown.TrickleDown);
        //    //functionField.RegisterValueChangeCallback(FunctionChanged);
        //    container.Add(functionField);

        //    SerializedProperty easingProp = property.FindPropertyRelative("easingType");
        //    easingField = new PropertyField(easingProp)
        //    {
        //        label = string.Empty
        //    };
        //    easingField.style.width = new StyleLength(90);
        //    easingField.BindProperty(easingProp);
        //    container.Add(easingField);

        //    preContainer.Add(label);
        //    preContainer.Add(container);

        //    return preContainer;
        //}

        //private void FunctionChanged(SerializedPropertyChangeEvent change)
        //{
        //    if ((FunctionType)functionTypeProp.enumValueIndex != FunctionType.Linear)
        //    {
        //        easingField.SetEnabled(true);
        //    }
        //    else easingField.SetEnabled(false);
        //}
    }
}