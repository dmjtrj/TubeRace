using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
#if UNITY_EDITOR

    [CustomEditor(typeof(SplineMeshProxy2))]
    public class SplineMeshProxyEditor2 : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                (target as SplineMeshProxy2).UpdatePoints2();
            }
        }
    }
#endif


    [RequireComponent(typeof(SplineMesh.Spline))]
    public class SplineMeshProxy2 : MonoBehaviour
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
        [SerializeField] private CurvedTrackPoint m_Point9;
        [SerializeField] private CurvedTrackPoint m_Point10;
        [SerializeField] private CurvedTrackPoint m_Point11;
        [SerializeField] private CurvedTrackPoint m_Point12;
        [SerializeField] private CurvedTrackPoint m_Point13;
        [SerializeField] private CurvedTrackPoint m_Point14;
        [SerializeField] private CurvedTrackPoint m_Point15;
        [SerializeField] private CurvedTrackPoint m_Point16;
        [SerializeField] private CurvedTrackPoint m_Point17;

        public void UpdatePoints2()
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
            n8.Position = m_Point9.transform.position;
            n8.Direction = m_Point9.transform.position + m_Point9.transform.forward * m_Point9.GetLength();

            var n9 = spline.nodes[9];
            n9.Position = m_Point10.transform.position;
            n9.Direction = m_Point10.transform.position + m_Point10.transform.forward * m_Point10.GetLength();

            var n10 = spline.nodes[10];
            n10.Position = m_Point11.transform.position;
            n10.Direction = m_Point11.transform.position + m_Point11.transform.forward * m_Point11.GetLength();

            var n11 = spline.nodes[11];
            n11.Position = m_Point12.transform.position;
            n11.Direction = m_Point12.transform.position + m_Point12.transform.forward * m_Point12.GetLength();

            var n12 = spline.nodes[12];
            n12.Position = m_Point13.transform.position;
            n12.Direction = m_Point13.transform.position + m_Point13.transform.forward * m_Point13.GetLength();

            var n13 = spline.nodes[13];
            n13.Position = m_Point14.transform.position;
            n13.Direction = m_Point14.transform.position + m_Point14.transform.forward * m_Point14.GetLength();

            var n14 = spline.nodes[14];
            n14.Position = m_Point15.transform.position;
            n14.Direction = m_Point15.transform.position + m_Point15.transform.forward * m_Point15.GetLength();

            var n15 = spline.nodes[15];
            n15.Position = m_Point16.transform.position;
            n15.Direction = m_Point16.transform.position + m_Point16.transform.forward * m_Point16.GetLength();

            var n16 = spline.nodes[16];
            n16.Position = m_Point17.transform.position;
            n16.Direction = m_Point17.transform.position + m_Point17.transform.forward * m_Point17.GetLength();

            var n17 = spline.nodes[17];
            n17.Position = m_Point1.transform.position;
            n17.Direction = m_Point1.transform.position + m_Point1.transform.forward * m_Point1.GetLength();
        }
    }
}
