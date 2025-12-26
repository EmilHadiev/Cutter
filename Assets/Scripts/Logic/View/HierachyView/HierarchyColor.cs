using UnityEngine;
using UnityEngine.UI;

public class HierarchyColor : MonoBehaviour
{
    #if UNITY_EDITOR
        public Color BackgroundColor = new Color(1, 0.8431373f, 0f, 0f);
        public Color TextColor = new Color(1, 0.8431373f, 0f, 1);

        private const string ObjName = "ARFitter_";

    [ContextMenu(nameof(SetOptions))]
    private void SetOptions()
    {
        var child = gameObject.GetComponentInChildren<AspectRatioFitter>();
        child.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        gameObject.name = $"{ObjName}{child.name}";
    }
    #endif
}