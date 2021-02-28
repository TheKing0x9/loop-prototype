using UnityEngine;
using System.Collections.Generic;
using Button = UnityEngine.UI.Button;
using Tween = Pixelplacement.Tween;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Loop.UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<Animatable> _animatables;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private float _duration;

        private void Start() 
        {
            _playButton.enabled = false;
            _exitButton.enabled = false;

            AnimateIn();
        }

        private void OnAnimationDone()
        {
            _playButton.enabled = true;
            _exitButton.enabled = true;
        }

        private void AnimateIn()
        {
            foreach(Animatable a in _animatables)
            {
                a.transform.anchoredPosition = a.initPosition;
                Tween.AnchoredPosition(a.transform, a.finalPosition, _duration, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, OnAnimationDone);
            }
        }

        private void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void AnimateOut()
        {
            foreach (Animatable a in _animatables)
            {
                a.transform.anchoredPosition = a.finalPosition;
                Tween.AnchoredPosition(a.transform, a.initPosition, _duration, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, Play);
            }
        }

        public void Exit()
        {
            #if !UNITY_EDITOR
                Application.Quit();
            #endif
        }
    }   
}