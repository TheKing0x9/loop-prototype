using UnityEngine;
using System.Collections.Generic;
using Button = UnityEngine.UI.Button;
using Tween = Pixelplacement.Tween;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Loop.UI
{
    [System.Serializable]
    struct Animatable
    {
        public RectTransform transform;
        public Vector3 initPosition;
        public Vector3 finalPosition;
    }
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<Animatable> _animatables;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private void Start() 
        {
            _playButton.enabled = false;
            _exitButton.enabled = false;
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
                Tween.AnchoredPosition(a.transform, a.finalPosition, 1, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, OnAnimationDone);
            }
        }

        private void Play()
        {
            SceneManager.LoadScene(1);
        }

        private void AnimateOut()
        {
            foreach (Animatable a in _animatables)
            {
                a.transform.anchoredPosition = a.finalPosition;
                Tween.AnchoredPosition(a.transform, a.initPosition, 1, 0, Tween.EaseInOutBack, Tween.LoopType.None, null, Play);
            }
        }

        private void Exit()
        {
            #if !UNITY_EDITOR
                Application.Quit();
            #endif
        }
    }   
}