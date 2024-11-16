using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip m_clip;
    private AudioSource m_source;

    // Start is called before the first frame update
    void Start()
    {
        m_source = GetComponent<AudioSource>();
        m_source.clip = m_clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!m_source.isPlaying)
        {
            m_source.Play();
        }
    }
}
