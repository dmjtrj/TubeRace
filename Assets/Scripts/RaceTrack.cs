using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race

{ 
    /// <summary>
    /// 
    /// Ѕазовый класс который определ€ет нашу трубу дл€ гонок.
    /// 
    /// </summary>
    public abstract class RaceTrack : MonoBehaviour
    {
        /// <summary>
        /// 
        /// –адиус трубы.
        /// 
        /// </summary>
        [Header("Base track properties")]
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        /// <summary>
        /// 
        /// ћетод возвращает длину трека.
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract float GetTrackLength();
        
        /// <summary>
        /// 
        /// ћетод возвращает позицию в 3д кривой центр-линии трубы.
        /// 
        /// </summary>
        /// <param name="distance">дистанци€ от начала трубы до ее GetTrackLength</param>
        /// <returns></returns>
        public abstract Vector3 GetPosition(float distance);

        /// <summary>
        /// 
        /// ћетод возвращает направление в 3д кривой центр-линии трубы.
        ///  асательна€ к кривой в точке.
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public abstract Vector3 GetDirection(float distance);

        public virtual Quaternion GetRotation(float distance)
        {
            return Quaternion.identity;
        }
    }
}
