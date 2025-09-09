using UnityEngine;

public class LayerChanger
{
    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj == null) 
            return;

        obj.layer = layer;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Transform child = obj.transform.GetChild(i);
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}