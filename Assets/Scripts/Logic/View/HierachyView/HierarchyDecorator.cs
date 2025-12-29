#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyDecorator
{
    static HierarchyDecorator()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj != null)
        {
            var highlighter = obj.GetComponent<HierarchyColor>();

            if (highlighter != null)
            {
                // 1. Создаем область для заливки фона. 
                // Сдвигаем её чуть-чуть вправо (x + 18), чтобы не перекрывать иконку "глазика" или стрелочку вложенности
                Rect fullRect = new Rect(selectionRect.x + 18, selectionRect.y, selectionRect.width, selectionRect.height);

                // 2. Рисуем ПЛОТНЫЙ фон, который закроет стандартный текст. 
                // Важно: Alpha у BackgroundColor в инспекторе должна быть 1 (или близко к этому), 
                // чтобы старый текст не просвечивал.
                Color bgColor = highlighter.BackgroundColor;
                // Если вы хотите, чтобы фон был прозрачным, но старый текст исчез — 
                // это сложно, поэтому лучше использовать непрозрачный фон.
                EditorGUI.DrawRect(fullRect, bgColor);

                // 3. Настраиваем стиль текста
                GUIStyle style = new GUIStyle();
                style.normal.textColor = highlighter.TextColor;
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.MiddleLeft;
                style.padding.left = 2; // Небольшой отступ от края фона

                // 4. Рисуем имя объекта
                EditorGUI.LabelField(fullRect, obj.name, style);
            }
        }
    }
}
#endif