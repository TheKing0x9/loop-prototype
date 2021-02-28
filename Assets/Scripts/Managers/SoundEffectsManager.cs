using UnityEngine;
using Singleton = Pixelplacement.Singleton<Loop.Managers.SoundEffectsManager>;

namespace Loop.Managers
{    
    public class SoundEffectsManager : Singleton
    {
        private AudioSource _source;

        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _loseClip;
        [SerializeField] private AudioClip _applauseClip;

        private void Awake() {
            _source = GetComponent<AudioSource>();
        }

        public void Win()
        {
            _source.clip = _winClip;
            _source.Play();
        }

        public void Lose()
        {
            _source.clip = _loseClip;
            _source.Play();
        }

        public void Applause()
        {
            _source.clip = _applauseClip;
            _source.Play();
        }
    }
}