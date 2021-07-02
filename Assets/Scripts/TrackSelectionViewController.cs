using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    


    public class TrackSelectionViewController : MonoBehaviour
    {
        [SerializeField] private MainMenuViewController m_MainMenuViewController;

        public void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_MainMenuViewController.gameObject.SetActive(true);

                gameObject.SetActive(false);
            }
        }
    }
}
