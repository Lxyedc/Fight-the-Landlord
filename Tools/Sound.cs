using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景音 音效
/// </summary>
public class Sound : MonoSingleton<Sound> {

    AudioSource m_Bg;
    AudioSource m_Effect;
    public string ResourceDir = "";

    protected override void Awake()
    {
        base.Awake();
        m_Bg = gameObject.AddComponent<AudioSource>();
        m_Bg.loop = true;
        m_Bg.playOnAwake = false;
        m_Bg.volume = 0.5f;

        m_Effect = gameObject.AddComponent<AudioSource>();

    }

    /// <summary>
    /// 背景音
    /// </summary>
    /// <param name="name"></param>
    public void PlayBG(string  name)
    {
        string oldName;
        if (m_Bg.clip == null)
            oldName = "";
        else
            oldName = m_Bg.clip.name;

        if(oldName != name)
        {
            AudioClip clip = Resources.Load<AudioClip>(ResourceDir + "/" + name);

            if (clip != null)
            {
                m_Bg.clip = clip;
                m_Bg.Play();

            }
        }
    }

    /// <summary>
    /// 音效
    /// </summary>
    /// <param name="name"></param>
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(ResourceDir + "/" + name);
        m_Effect.PlayOneShot(clip);
    }
}
