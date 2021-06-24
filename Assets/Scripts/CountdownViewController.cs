using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class CountdownViewController : MonoBehaviour
    {
        [SerializeField] private RaceController m_RaceController;
        [SerializeField] private Text m_Label;

        // отсчет времени до начала гонки
        private void Update()
        {
            float t = m_RaceController.CountTimer;

            if (t >= 1)
                m_Label.text = ((int)t).ToString();
            else if (t > 0 && t < 1)
                m_Label.text = "Go!";
            else
                gameObject.SetActive(false);
        }
    }
}
