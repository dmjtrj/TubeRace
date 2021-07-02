using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class TrackEntryViewController : MonoBehaviour
    {
        [SerializeField] private TrackDescription m_TrackDescription;
        [SerializeField] private Text m_TrackName;
        [SerializeField] private Image m_PreviewImage;
        [SerializeField] private Text m_TrackLength;

        private TrackDescription m_ActiveDescription;

        private void Start()
        {
            if (m_TrackDescription != null)
            {
                SetViewValues(m_TrackDescription);
            }
        }

        public void SetViewValues(TrackDescription desc)
        {
            m_ActiveDescription = desc;
            
            m_TrackName.text = desc.TrackName;

            m_PreviewImage.sprite = desc.PreviewImage;

            m_TrackLength.text = desc.TrackLength.ToString();
        }

        public void OnButtonStartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_ActiveDescription.SceneNickname);
        }
    }
}
