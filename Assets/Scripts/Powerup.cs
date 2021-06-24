using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public abstract class Powerup : MonoBehaviour
    {
        [SerializeField] private RaceTrack m_Track;
        [SerializeField] private float m_Distance;
        [Range(0.0f, 360f)]
        [SerializeField] private float m_RollAngle;
        // угол в диапазоне которого будут активироваться паверапы
        [Range(20.0f, 100f)]
        [SerializeField] private float offsetAngle;
        

        private void OnValidate()
        {
            SetPowerPosition();
        }

        private void OnDrawGizmos()
        {
            Vector3 a1 = new Vector3(0, 0, transform.position.z);
            Gizmos.DrawLine(a1, transform.position);
        }

        private void SetPowerPosition()
        {
            Vector3 PowerupPos = m_Track.GetPosition(m_Distance);
            Vector3 PowerupDir = m_Track.GetDirection(m_Distance);

            
            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (m_Track.Radius));

            transform.position = PowerupPos - trackOffset;
            transform.rotation = Quaternion.LookRotation(PowerupDir, trackOffset);
        }

        private void Update()
        {
            UpdateBikes();
            DrawOffsetAngle();
        }

        private void UpdateBikes()
        {
            foreach (var bikeObject in GameObject.FindGameObjectsWithTag(Bike.Tag))
            {
                Bike bike = bikeObject.GetComponent<Bike>();

                float prev = bike.GetPrevDistance();
                float curr = bike.GetDistance();

                
                if (prev < m_Distance && curr > m_Distance)
                {
                    if (m_RollAngle <= (bike.RollAngle + offsetAngle/2) && m_RollAngle >= (bike.RollAngle - offsetAngle/2))
                        // bike picks powerup
                        OnPickedByBike(bike);
                }
            }
        }
        private void DrawOffsetAngle()
        {
            
        }
        public abstract void OnPickedByBike(Bike bike);

    }
}
