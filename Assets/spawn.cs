using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class spawn : MonoBehaviour
{
    public GameObject m_DecalProjectorPrefab;

    void Start()
    {
        GameObject m_DecalProjectorObject = Instantiate(m_DecalProjectorPrefab,  transform);
        DecalProjector m_DecalProjectorComponent = m_DecalProjectorObject.GetComponent<DecalProjector>();

        // Creates a new material instance for the DecalProjector.
        m_DecalProjectorComponent.material = new Material(m_DecalProjectorComponent.material);

    }
}
