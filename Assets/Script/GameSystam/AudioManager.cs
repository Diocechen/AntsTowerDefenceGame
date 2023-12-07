using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance; //singleton

    [SerializeField] private AudioClip bgmBattle;
    [SerializeField] private AudioClip seShoot;
    [SerializeField] private AudioClip seProjectileHit;
    [SerializeField] private AudioClip seAntHit;

    private List<AudioSource> audios = new();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 4;i++)
        {
            var audio = this.gameObject.AddComponent<AudioSource>(); //加入通道
            audios.Add(audio); //將audio source加入list
        }
    }

    private void Start()
    {
        PlayAudio(0, "bgmBattle", true);
    }

    public void PlayAudio(int index, string name, bool looping)
    {
        var clip = GetAudioClip(name);
        if (clip)
        {
            var audio = audios[index];
            audio.clip = clip;
            audio.loop = looping;
            audio.volume = 0.5f;
            audio.Play();  
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        switch (name)
        {
            case "bgmBattle":
                return bgmBattle;
            case "seShoot":
                return seShoot;
            case "seProjectileHit":
                return seProjectileHit;
            case "seAntHit":
                return seAntHit;
        }
        return null;
    }
}
