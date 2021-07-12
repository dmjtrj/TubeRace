using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Race
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject m_Prefab;
        [SerializeField] private int m_NumObjects;
        [SerializeField] private RaceTrack m_Track;
        [SerializeField] private bool m_RandomizeRotation;
        [SerializeField] private bool m_SpawnByChance;
        // шанс того, что объект заспаунится
        [Range(1, 5)]
        [SerializeField] int instChance;

        // метод, создающий префабы
        public void spawnObj()
        {
            Instantiate(m_Prefab);
        }

        private void Start()
        {
            float distance = 0;

            int rand = m_NumObjects;

            for (int i = 0; i < m_NumObjects; i++)
            {
                if (distance == 0)
                {
                    distance += m_Track.GetTrackLength() / m_NumObjects;
                    continue;
                }
                else
                {
                    if (m_SpawnByChance == true)
                    {
                        if (Random.Range(1, rand) % instChance == 0)
                        {
                            spawnObj();
                            rand -= 1;
                        }
                    }
                    else
                        spawnObj();

                    m_Prefab.transform.position = m_Track.GetPosition(distance);
                    m_Prefab.transform.rotation = m_Track.GetRotation(distance);

                    if (m_RandomizeRotation)
                    {
                        m_Prefab.transform.Rotate(Vector3.forward, UnityEngine.Random.Range(0, 360), Space.Self);
                    }

                    distance += m_Track.GetTrackLength() / m_NumObjects;
                }
            }
        }
    }
}
