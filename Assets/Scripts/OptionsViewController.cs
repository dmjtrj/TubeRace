using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class OptionsViewController : MonoBehaviour
    {
        [SerializeField] private MainMenuViewController m_MainMenuViewController;
        [SerializeField] private ScreenResolutionViewController m_ScreenResolutionViewController;
        [SerializeField] private MenuSFXController m_MenuSFXController;
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_MenuSFXController.Click();

                m_MainMenuViewController.gameObject.SetActive(true);

                gameObject.SetActive(false);
            }
        }

        public void OnButtonBackToMenu()
        {
            m_MainMenuViewController.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnButtonScreenResulotion()
        {
            m_ScreenResolutionViewController.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
