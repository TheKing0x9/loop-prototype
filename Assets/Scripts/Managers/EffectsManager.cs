using UnityEngine;
using System.Collections.Generic;
using Text = UnityEngine.UI.Text;
using Tween = Pixelplacement.Tween;
using Animatable = Loop.UI.Animatable;
using IEnumerator = System.Collections.IEnumerator;
using Singleton = Pixelplacement.Singleton<Loop.Managers.EffectsManager>;

namespace Loop.Managers
{
    public class EffectsManager : Singleton
    {
        [SerializeField] private List<Animatable> _startAnimatables;
        [SerializeField] private List<Animatable> _endAnimatables;
        [SerializeField] private Text _timerText;
        private string _exitText = "";
        [SerializeField] private ParticleSystem _confetti;

        private void Start()
        {
            _timerText.text = "";
            _confetti = GetComponentInChildren<ParticleSystem>();
        }


        private void ShowEntryTimer()
        {
            StartCoroutine(EntryTime());
        }

        private IEnumerator EntryTime()
        {
            _timerText.text = "3";
            yield return new WaitForSeconds(1);

            _timerText.text = "2";
            yield return new WaitForSeconds(1);

            _timerText.text = "1";
            yield return new WaitForSeconds(1);

            _timerText.text = "GO!";
            yield return new WaitForSeconds(1);

            _timerText.text = "";
        }

        private void OnDestroy() {
            StopAllCoroutines();    
        }

        public void StartAnim()
        {
            Animatable a;
            for (int i = 0; i < _startAnimatables.Count - 1; i++)
            {
                a = _startAnimatables[i];
                a.transform.anchoredPosition = a.initPosition;
                Tween.AnchoredPosition(a.transform, a.finalPosition, 2, 0, Tween.EaseOutBack);
            }

            a = _startAnimatables[_startAnimatables.Count - 1];
            a.transform.anchoredPosition = a.initPosition;
            Tween.AnchoredPosition(a.transform, a.finalPosition, 2, 0, Tween.EaseOutBack, Tween.LoopType.None, null, ShowEntryTimer);
        }

        private void ShowExitText()
        {
            _timerText.text = _exitText;
            _exitText = "";
        }

        public void EndAnim(string text)
        {
            _exitText = text;
            Animatable a;
            for (int i = 0; i < _endAnimatables.Count - 1; i++)
            {
                a = _endAnimatables[i];
                a.transform.anchoredPosition = a.finalPosition;
                Tween.AnchoredPosition(a.transform, a.initPosition, 2, 0, Tween.EaseOutBack);
            }

            a = _endAnimatables[_endAnimatables.Count - 1];
            a.transform.anchoredPosition = a.finalPosition;
            Tween.AnchoredPosition(a.transform, a.initPosition, 2, 0, Tween.EaseOutBack, Tween.LoopType.None, null, ShowExitText);
        }

        public void Confetti(Vector3 position)
        {
            _confetti.transform.position = position;
            _confetti.Play();
        }
    }
}