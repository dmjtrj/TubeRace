using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class ComplexEngineSFXController : MonoBehaviour
    {
        [SerializeField] private Bike m_Bike;

        [SerializeField] private AudioSource m_SfxLow;
        [SerializeField] private AudioSource m_SfxHigh;
        [SerializeField] private AudioSource m_SfxLoud;
        [SerializeField] private AudioSource m_SfxSonicSpeed;

        [SerializeField] private AnimationCurve m_CurveLow; // 0
        [SerializeField] private AnimationCurve m_CurveHigh; // 0.5
        [SerializeField] private AnimationCurve m_CurveLoud; // 1
        [SerializeField] private AnimationCurve m_CurveSonicSpeed;

        [SerializeField] private AudioSource m_SfxSonicBoom;

        [SerializeField] public float PitchFactor = 4;

        [SerializeField] private float m_SuperSonicSpeed;
        [SerializeField] private AnimationCurve m_SonicCurve;

        public bool IsSuperSonic { get; private set; }

        public void SetSuperSonic(bool flag)
        {
            if(!IsSuperSonic && flag)
            {
                m_SfxSonicBoom.Play();
                m_SfxSonicSpeed.Play();
            }

            IsSuperSonic = flag;
        }

        private void Update()
        {
            SetSuperSonic(Mathf.Abs(m_Bike.Velocity) > m_SuperSonicSpeed);

            if(m_SfxSonicBoom.isPlaying)
            {
                var t = Mathf.Clamp01(m_SfxSonicBoom.time / m_SfxSonicBoom.clip.length);

                m_SfxSonicBoom.volume = m_SonicCurve.Evaluate(t);
            }

            UpdateDynamicEngineSound();
        }

        private void UpdateDynamicEngineSound()
        {
            if(IsSuperSonic)
            {
                m_SfxSonicSpeed.volume = m_CurveSonicSpeed.Evaluate(m_SfxSonicSpeed.time / m_SfxSonicSpeed.clip.length);
                m_SfxLow.volume = 0;
                m_SfxHigh.volume = 0;
                m_SfxLoud.volume = 0;
                return;
            }

            //var t = m_Bike.GetNormalizedSpeed();

            var t = Mathf.Clamp01(m_Bike.Velocity / m_SuperSonicSpeed);

            m_SfxLow.volume = m_CurveLow.Evaluate(t);

            m_SfxLow.pitch = 1.0f + PitchFactor * t;

            m_SfxHigh.volume = m_CurveHigh.Evaluate(t);

            m_SfxHigh.pitch = 1.0f + PitchFactor * t;

            m_SfxLoud.volume = m_CurveLoud.Evaluate(t);

            m_SfxSonicSpeed.volume = 0;
        }
    }
}