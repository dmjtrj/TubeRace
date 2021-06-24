using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class CircleTrackPoint : MonoBehaviour
    {
        [SerializeField] private float m_Length = 1.0f;

        public float GetLength()
        {
            return m_Length;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(transform.position, 5.0f);
        }
    }
}