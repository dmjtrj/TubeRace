using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
#if UNITY_EDITOR

    [CustomEditor(typeof(SplineMeshProxy))]
    public class SplineMeshProxyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                (target as SplineMeshProxy).UpdatePoints();
            }
        }
    }
#endif


    [RequireComponent(typeof(SplineMesh.Spline))]
    public class SplineMeshProxy : MonoBehaviour
    {
        [SerializeField] private RaceTrackCurved m_CurvedTrack;

        [SerializeField] private CurvedTrackPoint m_Point1;
        [SerializeField] private CurvedTrackPoint m_Point2;
        [SerializeField] private CurvedTrackPoint m_Point3;
        [SerializeField] private CurvedTrackPoint m_Point4;
        [SerializeField] private CurvedTrackPoint m_Point5;
        [SerializeField] private CurvedTrackPoint m_Point6;
        [SerializeField] private CurvedTrackPoint m_Point7;
        [SerializeField] private CurvedTrackPoint m_Point8;


        public void UpdatePoints()
        {
            var spline = GetComponent<SplineMesh.Spline>();

            var n0 = spline.nodes[0];
            n0.Position = m_Point1.transform.position;
            n0.Direction = m_Point1.transform.position + m_Point1.transform.forward * m_Point1.GetLength();

            var n1 = spline.nodes[1];
            n1.Position = m_Point2.transform.position;
            n1.Direction = m_Point2.transform.position + m_Point2.transform.forward * m_Point2.GetLength();

            var n2 = spline.nodes[2];
            n2.Position = m_Point3.transform.position;
            n2.Direction = m_Point3.transform.position + m_Point3.transform.forward * m_Point3.GetLength();

            var n3 = spline.nodes[3];
            n3.Position = m_Point4.transform.position;
            n3.Direction = m_Point4.transform.position + m_Point4.transform.forward * m_Point4.GetLength();

            var n4 = spline.nodes[4];
            n4.Position = m_Point5.transform.position;
            n4.Direction = m_Point5.transform.position + m_Point5.transform.forward * m_Point5.GetLength();

            var n5 = spline.nodes[5];
            n5.Position = m_Point6.transform.position;
            n5.Direction = m_Point6.transform.position + m_Point6.transform.forward * m_Point6.GetLength();

            var n6 = spline.nodes[6];
            n6.Position = m_Point7.transform.position;
            n6.Direction = m_Point7.transform.position + m_Point7.transform.forward * m_Point7.GetLength();

            var n7 = spline.nodes[7];
            n7.Position = m_Point8.transform.position;
            n7.Direction = m_Point8.transform.position + m_Point8.transform.forward * m_Point8.GetLength();

            var n8 = spline.nodes[8];
            n8.Position = m_Point1.transform.position;
            n8.Direction = m_Point1.transform.position + m_Point1.transform.forward * m_Point1.GetLength();

        }
    }
}
