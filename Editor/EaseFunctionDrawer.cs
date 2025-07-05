using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace HexTecGames.EaseFunctions.Editor
{
    [CustomPropertyDrawer(typeof(EaseFunction))]
    public class EaseFunctionDrawer : PropertyDrawer
    {
        private PropertyField easingField;
        private SerializedProperty functionTypeProp;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement preContainer = new VisualElement();
            VisualElement container = new VisualElement();

            Label label = new Label(property.displayName);
            label.style.width = new StyleLength(110);
            preContainer.style.flexDirection = FlexDirection.Row;
            container.style.flexDirection = FlexDirection.Row;

            functionTypeProp = property.FindPropertyRelative("functionType");
            PropertyField functionField = new PropertyField(functionTypeProp)
            {
                label = string.Empty
            };
            functionField.style.width = new StyleLength(80);

            functionField.BindProperty(functionTypeProp);
            functionField.RegisterCallback<SerializedPropertyChangeEvent>(FunctionChanged, TrickleDown.TrickleDown);
            //functionField.RegisterValueChangeCallback(FunctionChanged);
            container.Add(functionField);

            SerializedProperty easingProp = property.FindPropertyRelative("easingType");
            easingField = new PropertyField(easingProp)
            {
                label = string.Empty
            };
            easingField.style.width = new StyleLength(90);
            easingField.BindProperty(easingProp);
            container.Add(easingField);

            preContainer.Add(label);
            preContainer.Add(container);

            return preContainer;
        }

        private void FunctionChanged(SerializedPropertyChangeEvent change)
        {
            if ((FunctionType)functionTypeProp.enumValueIndex != FunctionType.Linear)
            {
                easingField.SetEnabled(true);
            }
            else easingField.SetEnabled(false);
        }
    }
}