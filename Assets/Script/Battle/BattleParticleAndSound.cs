using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticlesAndSounds", menuName = "Scriptable Object/ParticlesAndSounds", order = int.MaxValue)]
public class BattleParticleAndSound : ScriptableObject
{
    AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();

    public List<GameObject> paticles = new List<GameObject>();

    public void PlaySound(AudioSource audioSource,int soundIndex)
    {
        audioSource.PlayOneShot(audioClips[soundIndex]);
    }
    public void PlayParticle(int particleIndex,Vector3 position)
    {
        Destroy(Instantiate(paticles[particleIndex], position, Quaternion.identity),5);  
    }
}
