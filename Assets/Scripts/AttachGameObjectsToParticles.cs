using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AttachGameObjectsToParticles : MonoBehaviour {
    public GameObject m_Prefab;

    private ParticleSystem m_ParticleSystem;
    private List<GameObject> m_Instances = new();
    private ParticleSystem.Particle[] m_Particles;
    
    private void Start() {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
    }

    private void LateUpdate() {
        var count = m_ParticleSystem.GetParticles(m_Particles);

        while (m_Instances.Count < count) {
            m_Instances.Add(Instantiate(m_Prefab, m_ParticleSystem.transform));
        }

        var worldSpace = m_ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World;
        for (var i = 0; i < m_Instances.Count; i++) {
            if (i < count) {
                if (worldSpace)
                    m_Instances[i].transform.position = m_Particles[i].position;
                else
                    m_Instances[i].transform.localPosition = m_Particles[i].position;
                m_Instances[i].SetActive(true);
            } else {
                m_Instances[i].SetActive(false);
            }
        }
    }
}
