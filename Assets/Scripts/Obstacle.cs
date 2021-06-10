using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private RaceTrack m_Track;
        // ���������� ����������, ���������� �� �������� �������� �����������
        [Range(-150f, 150f)]
        [SerializeField] private float m_ObstacleRollSpeed;
        [Range(0.0f, 360f)]
        [SerializeField] private float m_RollAngle;
        [SerializeField] private float m_Distance;
        [Range(0.0f, 20f)]
        [SerializeField] private float m_RadiusModifier = 1;

        private void SetObstaclePosition()
        {
            Vector3 obstaclePos = m_Track.GetPosition(m_Distance);
            Vector3 obstacleDir = m_Track.GetDirection(m_Distance);
            
            // �������� ��������� �����, ���� ����� ��������, �� ����� �������������� ������ �������� � ����������� ����� ���������
            // ������ �������� ���������� ��� ����, ����� � ������ ��������� ����� ����� �� �������� ���� ����������� ������� ����������� � ������� ��������� m_RollAngle
            // � ��������� ������ �������� �� ����������� ������ ����� ��� ������� �������� ��� OnValidate()
            if (Application.isPlaying)
            {
                // ������ ���� �������� � ������� �������� ��������
                m_RollAngle += m_ObstacleRollSpeed * Time.deltaTime;
            }
            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (m_RadiusModifier * m_Track.Radius));

            transform.position = obstaclePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 centrelinePos = m_Track.GetPosition(m_Distance);
            Gizmos.DrawSphere(centrelinePos, m_Track.Radius);
        }

        private void OnValidate()
        {
            SetObstaclePosition();
        }

        private void Update()
        {
            SetObstaclePosition();
        }
    }
}