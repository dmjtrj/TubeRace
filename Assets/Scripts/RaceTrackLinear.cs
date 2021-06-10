using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// 
    /// Класс линейного трека
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
        /// Условия для того, чтобы капсула не выходила за пределы заданного отрезка
        /// 
        /// </summary>
        private void OnValidate()
        {

        }

        /// <summary>
        /// 
        /// Регулировка скорости капсулы
        /// 
        /// </summary>

        private void Update()
        {

        }

        #endregion
    }
}
