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
        

        private void OnValidate()
        {
            SetPowerPosition();
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
                    // Здесь я указал диапазон захвата паверапа в 40 градусов, но как мне кажется правильнее бы было построить
                    // 2 вектора идущих из центра трека которые будут касаться обеих сторон паверапа по локальной оси X,
                    // найти угол между этими векторами и значение от Roll_Angle - данный угол/2 до Roll_Angle + - данный угол/2 будет нужным.
                    // но пока что я не додумался как это сделать :(
                    if (m_RollAngle <= (bike.RollAngle + 20) && m_RollAngle >= (bike.RollAngle - 20))
                        // bike picks powerup
                        OnPickedByBike(bike);
                }
            }
        }

        public abstract void OnPickedByBike(Bike bike);

    }
}
