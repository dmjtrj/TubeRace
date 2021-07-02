using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private string m_TrackName;
        public string TrackName => m_TrackName;

        [SerializeField] private string m_SceneNickname;
        public string SceneNickname => m_SceneNickname;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        private float m_TrackLength { get; set; }
        public float TrackLength => m_TrackLength;
        public void SetTrackLength(float l)
        {
            m_TrackLength = l;
        }

    }
}
