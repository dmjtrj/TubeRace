using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race.UI
{

    public class BikeHudViewController : MonoBehaviour
    {
        [SerializeField] private Text m_LabelSpeed;
        [SerializeField] private Text m_LabelDistance;
        [SerializeField] private Text m_LabelRoll;
        [SerializeField] private Text m_LabelLapNumber;

        [SerializeField] private Text m_LabelHeat;
        [SerializeField] private Text m_LabelFuel;
        [SerializeField] private Bike m_Bike;

        private void Update()
        {
            m_LabelSpeed.text = $"Speed: {(int)m_Bike.Velocity} m/s"; // Speed: 100 m/s

            m_LabelDistance.text = $"Distance: {(int)m_Bike.Distance} m"; // Distance: 20 m

            m_LabelRoll.text = $"Angle: {(int)(m_Bike.RollAngle)} deg"; // Angle: 120 deg

            int laps = (int)(m_Bike.Distance / m_Bike.GetTrack.GetTrackLength());
            m_LabelLapNumber.text = $"Lap: {laps + 1} "; // Lap: 3

            int heat = (int)(m_Bike.GetNormalizedHeat() * 100.0f);
            m_LabelHeat.text = $"Heat: {heat}";

            m_LabelFuel.text = $"Fuel: {(int)(m_Bike.GetFuel())}";
        }
    }
}
