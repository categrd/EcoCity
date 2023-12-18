
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS
{

    public class AudioPlayer : MonoBehaviour
    {
        public AudioClip placementSound;
        public AudioClip destroySound;
        public AudioClip heatwaveSound;
        public AudioClip acidRainSound;
        public AudioClip smogSound;
        public AudioSource audioSource;

        public static AudioPlayer instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(this.gameObject);

        }

        public void PlayPlacementSound()
        {
            if(placementSound != null)
            {
                audioSource.PlayOneShot(placementSound);
            }
        }
        // play sound when structure is destroyed
        public void PlayDestroySound()
        {
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }
        }
        // play sound when heatwave starts
        public void PlayHeatwaveSound()
        {
            if (heatwaveSound != null)
            {
                audioSource.PlayOneShot(heatwaveSound);
            }
        }
        // play sound when acid rain starts
        public void PlayAcidRainSound()
        {
            if (acidRainSound != null)
            {
                audioSource.PlayOneShot(acidRainSound);
            }
        }
        // play sound when smog starts
        public void PlaySmogSound()
        {
            if (smogSound != null)
            {
                audioSource.PlayOneShot(smogSound);
            }
        }
        
        
    }
}