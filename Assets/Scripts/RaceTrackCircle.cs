using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Race
{
    public class RaceTrackCircle : RaceTrack
    {
        [SerializeField] private CircleTrackPoint[] m_CircleTrackPoints;

        private void OnDrawGizmos()
        {
            DrawBezierCircle();
        }

        private void DrawBezierCircle()
        {
            for(int i = 0; i < m_CircleTrackPoints.Length - 1; i++)
            {

            }
        }

        public override Vector3 GetDirection(float distance)
        {
            return Vector3.zero;
        }

        public override Vector3 GetPosition(float distance)
        {
            return Vector3.zero;
        }

        public override float GetTrackLength()
        {
            return 1.0f;
        }
    }
}
