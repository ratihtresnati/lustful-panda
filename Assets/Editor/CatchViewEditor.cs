using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(CatchSensor), true)]

public class CacthViewEditor : Editor
{

    private void OnSceneGUI()
    {
        CatchSensor fov = (CatchSensor)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radiusCatch);

        Vector3 viewAngle01 = DirectionFromAngleCatch(fov.transform.eulerAngles.y, -fov.angleCatch / 2);
        Vector3 viewAngle02 = DirectionFromAngleCatch(fov.transform.eulerAngles.y, fov.angleCatch / 2);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radiusCatch);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radiusCatch);

        if (fov.catchPlayer)
        {
            Handles.color = Color.blue;
            Handles.DrawLine(fov.transform.position, fov.playerSouldCatch.transform.position);
            Debug.Log("Player Catch");
        }
    }

    private Vector3 DirectionFromAngleCatch(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
