using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
#if UNITY_EDITOR
    // 
    [CustomEditor(typeof(RaceTrackCurved))]
    public class RaceTrackCurvedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            if(GUILayout.Button("Generate"))
            {
                (target as RaceTrackCurved).GenerateTrackData();
            }
        }
    }
#endif

    public class RaceTrackCurved : RaceTrack
    {
        // основные точки по которым строится трек
        [SerializeField] private CurvedTrackPoint[] m_TrackPoints;

        // количество точек на отрезке (между 2 основными точками) по которым из прямых будет строиться кривая
        [SerializeField] private int m_Division;
        
        // массив кватернионов на отрезке между 2 основными точками
        [SerializeField] private Quaternion[] m_TrackSampledRotations;
        // массив всех точек разбиения на треке
        [SerializeField] private Vector3[] m_TrackSampledPoints;
        // массив всех отрезков между точками разбиения
        [SerializeField] private float[] m_TrackSampledSegmentLengths;
        // длина всего трека
        [SerializeField] private float m_TrackSampledLength;

        [SerializeField] private bool m_DebugDrawBezier;
        [SerializeField] private bool m_DebugDrawSampledPoints;
        [SerializeField] private TrackDescription m_TrackDescription;

        public void Awake()
        {
            m_TrackDescription.SetTrackLength(m_TrackSampledLength);
        }

        private void OnDrawGizmos()
        {
            // визуальная отрисовка трека
            if(m_DebugDrawBezier)
                DrawBezierCurve();
            // отрисовка трека по точкам разбиения
            if(m_DebugDrawSampledPoints)    
                DrawSampledTrackPoints();
        }

        // генерация трека
        public void GenerateTrackData()
        {
            Debug.Log("Generating track data");

            if (m_TrackPoints.Length < 3)
                return;
            
            // массив в который сохраняются точки разбиения 
            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rotations = new List<Quaternion>();

            for (int i = 0; i < m_TrackPoints.Length - 1; i++)
            {
                var newPoints = GenerateBezierPoints(m_TrackPoints[i], m_TrackPoints[i + 1], m_Division);

                var newRotations = GenerateRotations(m_TrackPoints[i].transform, m_TrackPoints[i + 1].transform, newPoints);

                rotations.AddRange(newRotations);
                points.AddRange(newPoints);
            }

            var lastPoints = GenerateBezierPoints(m_TrackPoints[m_TrackPoints.Length - 1], m_TrackPoints[0], m_Division);
            var lastRotations = GenerateRotations(m_TrackPoints[m_TrackPoints.Length - 1].transform, m_TrackPoints[0].transform, lastPoints);

            // last points
            rotations.AddRange(lastRotations);
            points.AddRange(lastPoints);

            m_TrackSampledRotations = rotations.ToArray();
            m_TrackSampledPoints = points.ToArray();
            
            // precompute lengths
            {
                // массив отрезков между точками разбиения
                m_TrackSampledSegmentLengths = new float[m_TrackSampledPoints.Length - 1];

                m_TrackSampledLength = 0;
                for (int i = 0; i < m_TrackSampledPoints.Length - 1; i++)
                {
                    Vector3 a = m_TrackSampledPoints[i];
                    Vector3 b = m_TrackSampledPoints[i + 1];

                    float segmentLength = (b - a).magnitude;

                    m_TrackSampledSegmentLengths[i] = segmentLength;

                    m_TrackSampledLength += segmentLength; 
                }
            }
            m_TrackDescription.SetTrackLength(m_TrackSampledLength);
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private void DrawSampledTrackPoints()
        {
            Handles.DrawAAPolyLine(m_TrackSampledPoints);
        }

        // метод
        private Quaternion[] GenerateRotations(Transform a, Transform b, Vector3[] points)
        {
            List<Quaternion> rotations = new List<Quaternion>();
            
            float t = 0;
            
            for(int i = 0; i < points.Length - 1; i++)
            {
                Vector3 dir = (points[i + 1] - points[i]).normalized;

                Vector3 up = Vector3.Lerp(a.up, b.up, t);

                Quaternion rotation = Quaternion.LookRotation(dir, up);

                rotations.Add(rotation);

                t += 1.0f / (points.Length - 1);
            }

            rotations.Add(b.rotation);
            
            return rotations.ToArray();
        }

        // метод возвращающий массив точек разбиения трека (между 2 основными точками)
        private Vector3[] GenerateBezierPoints(CurvedTrackPoint a, CurvedTrackPoint b, int division)
        {
            return Handles.MakeBezierPoints(
                a.transform.position,
                b.transform.position,
                a.transform.position + a.transform.forward * a.GetLength(),
                b.transform.position - b.transform.forward * b.GetLength(),
                division);
        }

        // построение линии(!) всего участка трассы по кривым с помощью метода ниже
        private void DrawBezierCurve()
        {
            if (m_TrackPoints.Length < 3)
                return;
            
            for(int i = 0; i < m_TrackPoints.Length - 1; i++)
            {
                DrawTrackPartGizmo(m_TrackPoints[i], m_TrackPoints[i + 1]);
            }
            
            DrawTrackPartGizmo(m_TrackPoints[m_TrackPoints.Length - 1], m_TrackPoints[0]);
        }

        // построение линии(!) кривой между 2 соседними точками
        private void DrawTrackPartGizmo(CurvedTrackPoint a, CurvedTrackPoint b)
        {
            Handles.DrawBezier(
                a.transform.position,
                b.transform.position,
                a.transform.position + a.transform.forward * a.GetLength(),
                b.transform.position - b.transform.forward * b.GetLength(),
                Color.green,
                Texture2D.whiteTexture,
                1.0f
                );
        }

        public override Vector3 GetDirection(float distance)
        {
            distance = Mathf.Repeat(distance, m_TrackSampledLength);

            for (int i = 0; i < m_TrackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - m_TrackSampledSegmentLengths[i];

                if (diff < 0)
                {
                    return (m_TrackSampledPoints[i + 1] - m_TrackSampledPoints[i]).normalized;
                }
                else
                {
                    distance -= m_TrackSampledSegmentLengths[i];
                }
            }

            return Vector3.forward;
        }

        public override Vector3 GetPosition(float distance)
        {
            distance = Mathf.Repeat(distance, m_TrackSampledLength);

            for(int i = 0; i < m_TrackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - m_TrackSampledSegmentLengths[i];

                if(diff < 0)
                {
                    // return position
                    float t = distance / m_TrackSampledSegmentLengths[i];

                    return Vector3.Lerp(m_TrackSampledPoints[i], m_TrackSampledPoints[i + 1], t);
                }
                else
                {
                    distance -= m_TrackSampledSegmentLengths[i];
                }
            }
            return Vector3.zero;
        }

        public override Quaternion GetRotation(float distance)
        {
            distance = Mathf.Repeat(distance, m_TrackSampledLength);

            for (int i = 0; i < m_TrackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - m_TrackSampledSegmentLengths[i];

                if (diff < 0)
                {
                    // return position
                    float t = distance / m_TrackSampledSegmentLengths[i];

                    return Quaternion.Slerp(
                        m_TrackSampledRotations[i],
                        m_TrackSampledRotations[i + 1],
                        t
                        );
                }
                else
                {
                    distance -= m_TrackSampledSegmentLengths[i];
                }
            }

            return Quaternion.identity;
        }

        public override float GetTrackLength()
        {
            return m_TrackSampledLength;
        }
    }
}
