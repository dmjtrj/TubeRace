using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class MenuSFXController : MonoBehaviour
    {
        [SerializeField] private AudioSource m_Click;
        [SerializeField] private AudioSource m_Success;
        [SerializeField] private AudioSource m_Logout;

        public void Click()
        {
            m_Click.Play();
        }

        public void Success()
        {
            m_Success.Play();
        }

        public void Logout()
        {
            m_Logout.Play();
        }
    }
}
