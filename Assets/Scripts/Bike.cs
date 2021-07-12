using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// 
    /// Model
    /// ћодель данных
    /// 
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)]
        public float mass;

        [Range(0.0f, 100.0f)]
        public float thrust;

        public float afterburnerThrust;

        [Range(0.0f, 1000.0f)]
        public float agility;
        public float maxSpeed;

        public float afterburnerMaxSpeedBonus;

        public float afterburnerCoolSpeed;
        public float afterburnerHeatGeneration; // per second
        public float afterburnerMaxHeat;

        //максимальна€ скорость вращени€
        public float maxSpeedRoll;

        [Range(0.0f, 1.0f)]
        public float linearDrag;

        [Range(0.0f, 1.0f)]
        public float collisionBounceFactor;

        public bool afterburner;

        public GameObject m_EngineModelId;
        public GameObject m_HullModel;
    }

    /// <summary>
    /// 
    ///  онтроллер (—ущность)
    /// Controller
    /// 
    /// </summary>
    public class Bike : MonoBehaviour
    {
        [SerializeField] private AnimationCurve m_CollisionVolumeCurve;
        [SerializeField] private AudioSource m_CollisionSFX;

        [SerializeField] private bool m_IsPlayerBike;
        public bool IsPlayerBike => m_IsPlayerBike;

        [SerializeField] private BikeParameters m_BikeParametersInitial;

        /// <summary>
        /// 
        /// Viev
        /// ѕредставление
        /// 
        /// </summary>

        [SerializeField] private BikeViewController m_VisualController;
        

        /// <summary>
        ///  ”правление газом байка. Ќормализованное. ќт -1 до +1.
        /// </summary>
        private float m_ForwardThrustAxis;

        /// <summary>
        /// ”становка значени€ педали газа.
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val) => m_ForwardThrustAxis = val;

        /// <summary>
        ///  ”правление отклонением влево и вправо. Ќормализованное. ќт -1 до +1.
        /// </summary>
        private float m_HorizontalThrustAxis;
        /// <summary>
        /// ”становка значени€ отклонени€.
        /// </summary>
      
        /// <summary>
        /// ¬кл/выкл доп ускорител€
        /// </summary>
        public bool EnableAfterburner { get; set; }

        public void SetHorizontalThrustAxis(float val) => m_HorizontalThrustAxis = val;

        [SerializeField] private RaceTrack m_Track;
        
        public RaceTrack GetTrack => m_Track;

        public bool IsMovementControlsActive { get; set; }

        private float m_Distance;
        private float m_Velocity;
        [Range(0.0f, 360f)]
        [SerializeField] private float m_RollAngle;

        public float Distance => m_Distance;
        public float Velocity => m_Velocity;
        public float RollAngle => m_RollAngle;
        public float GetDistance()
        {
            return m_Distance;
        }

        //текуща€ скорость вращени€
        private float m_RollSpeed;

        public void Update()
        {
            UpdateAfterburnerHeat();
            BikePhysics();
            BestLapTime();
        }

        private float m_AfterburnerHeat;
        
        public float GetNormalizedHeat()
        {
            if (m_BikeParametersInitial.afterburnerMaxHeat > 0)
                return m_AfterburnerHeat / m_BikeParametersInitial.afterburnerMaxHeat;

            return 0;
        }

        public void CoolAfterburner()
        {
            m_AfterburnerHeat = 0;
        }

        private void UpdateAfterburnerHeat()
        {
            // calc heat dissipation;
            m_AfterburnerHeat -= m_BikeParametersInitial.afterburnerCoolSpeed * Time.deltaTime;
            
            if (m_AfterburnerHeat < 0)
                m_AfterburnerHeat = 0;

            // Chech max heat
            // ***
        }

        public float GetNormalizedSpeed()
        {
            return Mathf.Clamp01(m_Velocity / m_BikeParametersInitial.maxSpeed);
        }

        private void BikePhysics()
        {
            // S=v * dt
            // F=m * a
            // V=v0 + a * dt
            float dt = Time.deltaTime;
            //float dv = dt * m_ForwardThrustAxis * m_BikeParametersInitial.thrust;

            float F_thrustMax = m_BikeParametersInitial.thrust;
            float Vmax = m_BikeParametersInitial.maxSpeed;
            float F = m_ForwardThrustAxis * m_BikeParametersInitial.thrust;

            if (EnableAfterburner && ConsumeFuelForAfterburner(1.0f * Time.deltaTime))
            {
                m_AfterburnerHeat += m_BikeParametersInitial.afterburnerHeatGeneration * Time.deltaTime;

                F += m_BikeParametersInitial.afterburnerThrust;

                Vmax += m_BikeParametersInitial.afterburnerMaxSpeedBonus;
                F_thrustMax += m_BikeParametersInitial.afterburnerThrust;
            }

            // drag
            F += -m_Velocity * (F_thrustMax / Vmax);
            
            float dv = dt * F;


            // F=ma
            // F_thrust
            // F_drag
            // F = F_thrust - F_drag
            // F_drag = -V * K_drag

            // F = F_thrust - V * K_drag
            // V * K_drag = F_thrust
            // K_drag = f_thrust / Vmax


            m_Velocity += dv;

            if (m_BikeStatistics.TopSpeed < Mathf.Abs(m_Velocity))
                m_BikeStatistics.TopSpeed = Mathf.Abs(m_Velocity);

            //m_Velocity = Mathf.Clamp(m_Velocity, -m_BikeParametersInitial.maxSpeed, m_BikeParametersInitial.maxSpeed);

            float dS = m_Velocity * dt;

            // collision 
            if (Physics.Raycast(transform.position, transform.forward, dS))
            {
                m_CollisionSFX.volume = m_CollisionVolumeCurve.Evaluate(GetNormalizedSpeed());
                m_CollisionSFX.Play();

                m_Velocity = -m_Velocity * m_BikeParametersInitial.collisionBounceFactor;
                dS = m_Velocity * dt;
                // при столкновении с преп€тствием у байка будет перегрев
                m_AfterburnerHeat = m_BikeParametersInitial.afterburnerMaxHeat;
            }

            m_PrevDistance = m_Distance;
            m_Distance += dS;

            //m_Velocity += -m_Velocity * m_BikeParametersInitial.linearDrag * dt;

            // ограничение дистанции длиной в один круг 
            //m_Distance = Mathf.Clamp(m_Distance, 0, m_Track.GetTrackLength());

            // если байк достигнет конечной точки трассы, то скорость приравниваетс€ к 0
            //if (m_Distance == m_Track.GetTrackLength() || m_Distance == 0)
            //{
            //    m_Velocity = 0;
            //}

            // изминение скорости вращени€ с учетом поворачиваемости байка
            float dv_roll = dt * m_HorizontalThrustAxis * m_BikeParametersInitial.agility;
            // изминение текущей скорости вращени€
            m_RollSpeed += dv_roll;
            // ограничение текущей скорости в соответствии с максимально возможной
            m_RollSpeed = Mathf.Clamp(m_RollSpeed, -m_BikeParametersInitial.maxSpeedRoll, m_BikeParametersInitial.maxSpeedRoll);
            // корректировка скорости вращени€ в соответствии с трением
            m_RollSpeed += -m_RollSpeed * m_BikeParametersInitial.linearDrag * dt;
            // расчет угла вращени€ с учетом полученной текущей скорости
            m_RollAngle += m_RollSpeed * dt;
            if (m_RollAngle > 360)
            {
                m_RollAngle = 0;
            }
            else if (m_RollAngle < 0)
            {
                m_RollAngle = 360;
            }

            Vector3 bikePos = m_Track.GetPosition(m_Distance);
            Vector3 bikeDir = m_Track.GetDirection(m_Distance);

            Quaternion q = Quaternion.AngleAxis(m_RollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * m_Track.Radius);

            //transform.position = bikePos - trackOffset;
            //transform.rotation = Quaternion.LookRotation(bikeDir, trackOffset);

            transform.position = bikePos;
            transform.rotation = m_Track.GetRotation(m_Distance);
            transform.Rotate(Vector3.forward, m_RollAngle, Space.Self);
            transform.Translate(-Vector3.up * m_Track.Radius, Space.Self);
        }

        private float m_PrevDistance;
        public float GetPrevDistance()
        {
            return m_PrevDistance;
        }
        // 0 - 100
        private float m_Fuel;

        public float GetFuel()
        {
            return m_Fuel;
        }

        public void AddFuel(float amount)
        {
            m_Fuel += amount;

            m_Fuel = Mathf.Clamp(m_Fuel, 0, 100);
        }

        public void Braking()
        {
            m_Velocity = 0;
        }

        public bool ConsumeFuelForAfterburner(float amount)
        {
            if (m_Fuel <= amount)
                return false;

            m_Fuel -= amount;

            return true;
        }

        public static readonly string Tag = "Bike";


        public class BikeStatistics
        {
            public float TopSpeed;
            public float TotalTime;
            public int RacePlace;
            public float m_BestLapTime;
        }

        private BikeStatistics m_BikeStatistics;
        public BikeStatistics Statistics => m_BikeStatistics;

        [SerializeField] private RaceController m_RaceController;

        private void Awake()
        {
            m_BikeStatistics = new BikeStatistics();
            m_Times = new List<float>();
        }

        private float m_RaceStartTime;
        public void OnRaceStart()
        {
            m_RaceStartTime = Time.time;
        }

        public void OnRaceEnd()
        {
            m_BikeStatistics.TotalTime = Time.time - m_RaceStartTime;
        }


        private int i = 1;
        // CompletedLaps - номер текущего круга 
        private int CompletedLaps ;
        // TimeLastLap -  врем€ предыдущего круга
        float TimeLastLap = 0;
        public List<float> m_Times;
        // метод рассчитывающий лучшее врем€ круга
        public void BestLapTime()
        {
            CompletedLaps = (int)(Distance / m_Track.GetTrackLength());
            if (CompletedLaps == i)
            {
                float time2 = Time.time;
                float t = time2 - m_RaceStartTime;
                m_Times.Add(t);
                if (CompletedLaps == 1)
                {
                    Statistics.m_BestLapTime = m_Times[0];
                }
                else
                {
                    TimeLastLap = m_Times[CompletedLaps - 1] - m_Times[CompletedLaps - 2];
                    if (Statistics.m_BestLapTime <= TimeLastLap)
                    {
                        i++;
                        return;
                    }
                    else
                        Statistics.m_BestLapTime = TimeLastLap;
                }
                i++;
            }
        }
    }
}

