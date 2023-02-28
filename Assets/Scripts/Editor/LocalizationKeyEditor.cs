using System.Linq;
using Clicker.Localization;
using UnityEditor;
using UnityEngine.UIElements;

namespace Clicker
{
    [CustomPropertyDrawer(typeof(LocalizationKey<string>))]
    public class LocalizationKeyEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var localizator = LocalizationBuilderEditorResolver.LabelsLocalizator;
            var keys = localizator.Keys.ToList();

            var defaultValue = property.stringValue;
            var valueChangedCallback = new EventCallback<ChangeEvent<string>>(args =>
            {
                property.stringValue = args.newValue;
            });
            
            if (!keys.Contains(defaultValue))
            {
                defaultValue = $"[Not localized] {defaultValue}";
                valueChangedCallback += (args) =>
                {
                    if (args.newValue == args.previousValue)
                        return;

                    keys.Remove(defaultValue);
                };
            }

            var popupField = new PopupField<string>(
                property.name,
                keys.ToList(),
                defaultValue
                );
            popupField.RegisterValueChangedCallback(valueChangedCallback);
            
            var container = new VisualElement();
            container.Add(popupField);
            return container;
        }
    }
}