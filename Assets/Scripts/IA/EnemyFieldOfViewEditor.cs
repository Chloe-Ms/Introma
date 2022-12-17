using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFieldOfView))]
public class EnemyFieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFieldOfView fov = (EnemyFieldOfView)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.RadiusFront);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.RadiusBack);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.AngleFront / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.AngleFront / 2);
        Vector3 viewAngle03 = DirectionFromAngle(fov.transform.eulerAngles.y , -fov.AngleBack / 2);
        Vector3 viewAngle04 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.AngleBack / 2);

        Handles.color = Color.red;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.RadiusFront);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.RadiusFront);
        Handles.DrawLine(fov.transform.position, fov.transform.position - viewAngle03 * fov.RadiusBack);
        Handles.DrawLine(fov.transform.position, fov.transform.position - viewAngle04 * fov.RadiusBack);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            //Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
