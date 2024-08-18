using UnityEngine;

public static class CameraExtensions
{
    public static bool IsObjectVisible(Camera camera, Vector2 objectPosition, float margin = 0f)
    {
        //Get the camera bounds
        Vector3 viewPos = camera.WorldToViewportPoint(objectPosition);

        //Check if within camera's view bounds AABB .
        Debug.Log($"ViewPos: {viewPos.x}, {viewPos.y}, {viewPos.z}");
        return viewPos.x > 0f - margin && viewPos.x < 1f + margin &&
               viewPos.y > 0f - margin && viewPos.y < 1f + margin;
    }
}