using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class RaceResultsViewController : MonoBehaviour
    {
        [SerializeField] private Text m_Place;
        [SerializeField] private Text m_TopSpeed;
        [SerializeField] private Text m_TotalTime;
        [SerializeField] private Text m_BestLapTime;
        [SerializeField] private RaceController m_RaceController;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(Bike.BikeStatistics stats)
        {
            gameObject.SetActive(true);
            
            m_Place.text = "Place: " + stats.RacePlace.ToString();
            m_TopSpeed.text = "Top speed: " + ((int)stats.TopSpeed).ToString() + " m/s";
            m_TotalTime.text = "Time: " + stats.TotalTime.ToString() + " seconds";
            m_BestLapTime.text = "Best time lap: " + stats.m_BestLapTime.ToString() + " seconds";
        }

        public void OnButtonQuit()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(PauseViewController.MainMenuScene);
        }
    }
}
