using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class ScreenResolutionViewController : MonoBehaviour
    {
        [SerializeField] private OptionsViewController m_OptionsViewController;
        [SerializeField] private Dropdown m_Dropdown;
        [SerializeField] private Text m_TextDone;
        [SerializeField] private MenuSFXController m_MenuSFXController;
        // текущее значение разрешения, необходимо для того чтобы при выходе из данного пункта настроек и при повторном заходе
        // в dropdown было выбранное значение, подтвержденное кнопкой apply changes
        private int currentValue;

        private void Awake()
        {
            gameObject.SetActive(false);

            m_Dropdown.value = 2;
            currentValue = m_Dropdown.value;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_MenuSFXController.Click();

                OnButtonBackToOptions();
            }
        }
        public void OnButtonBackToOptions()
        {
            m_OptionsViewController.gameObject.SetActive(true);

            gameObject.SetActive(false);

            m_Dropdown.value = currentValue;

            m_TextDone.text = "";
        }

        public void OnButtonApplyChanges()
        {
            if (m_Dropdown.value == 0)
            {
                Screen.SetResolution(1024, 768, true);
            }

            if (m_Dropdown.value == 1)
            {
                Screen.SetResolution(1280, 1024, true);
            }

            if (m_Dropdown.value == 2)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            currentValue = m_Dropdown.value;

            m_TextDone.text = "Done";
        }
    }
}
