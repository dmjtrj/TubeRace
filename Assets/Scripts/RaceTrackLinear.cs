using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// 
    /// ����� ��������� �����
    /// 
    /// </summary>
    public class RaceTrackLinear : RaceTrack
    {
        [Header("Linear track properties")]
        [SerializeField] private Transform m_Start;
        [SerializeField] private Transform m_End;
        public override Vector3 GetDirection(float distance)
        {
            return (m_End.position - m_Start.position).normalized;
        }

        public override Vector3 GetPosition(float distance)
        {
            distance = Mathf.Clamp(distance, 0, GetTrackLength());

            Vector3 direction = m_End.position - m_Start.position;
            direction = direction.normalized;
            direction *= distance;

            return m_Start.position + direction;
        }

        public override float GetTrackLength()
        {
            Vector3 direction = m_End.position - m_Start.position;

            return direction.magnitude;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(m_Start.position, m_End.position);
        }

        #region Test

        [SerializeField] private float m_TestDistance;
        [SerializeField] private float m_Speed;
        //[SerializeField] private Transform m_TestObject;

        /// <summary>
        /// 
        /// ������� ��� ����, ����� ������� �� �������� �� ������� ��������� �������
        /// 
        /// </summary>
        private void OnValidate()
        {

        }

        /// <summary>
        /// 
        /// ����������� �������� �������
        /// 
        /// </summary>

        private void Update()
        {

        }

        #endregion
    }
}
