using UnityEngine;

public class SwordBladeRotator : MonoBehaviour
{
    private const int CorrectiveAngle = -90;

    public Quaternion GetRotateTowardsMouse(Vector3 mousePosition)
    {
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + CorrectiveAngle);
        return targetRotation; 
    }
}