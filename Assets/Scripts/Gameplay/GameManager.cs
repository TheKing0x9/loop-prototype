using UnityEngine;
using Singleton = Pixelplacement.Singleton<Loop.Gameplay.GameManager>;
using RoundData = Loop.Data.RoundData;
using IEnumerator = System.Collections.IEnumerator;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Loop.Gameplay
{
    public class GameManager : Singleton
    {
        [SerializeField] private RoundData _roundData;
        [SerializeField] private Transform _crown;
        [SerializeField] private Vector3 _crownOffset;
        [SerializeField] private int _pointsToWin = 5;
        [SerializeField] private float _startDelay = 3f;
        [SerializeField] private float _endDelay = 3f;

        [Header("Instantiation Details")]
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _AISpawnPoint;
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private GameObject _AIGO;

        
        private float _playerPoints;
        private float _AIPoints;
        private WaitForSeconds _startWait;
        private WaitForSeconds _endWait;
        private LoopManager _AI;
        private LoopManager _player;
        private LoopManager _leader;

        private void Start()
        {
            _startWait = new WaitForSeconds(_startDelay);
            _endWait = new WaitForSeconds(_endDelay);

            SpawnAllLoops();

            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(GameStart());

            yield return StartCoroutine(GameUpdate());

            yield return StartCoroutine(GameEnd());

            if (!(Won() || Lost()))
            {
                StartCoroutine(GameLoop());
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }

        private IEnumerator GameStart()
        {
            Debug.Log("start");
            DisableControls();
            _leader = null;
            _crown.gameObject.SetActive(false);

            yield return _startWait;
        }

        private IEnumerator GameEnd()
        {
            Debug.Log("end");
            var text = "";
            if(Won())
            {
                _roundData.CurrentRound++;
                if (_roundData.CurrentRound > _roundData.RoundsPerLevel)
                {
                    _roundData.CurrentLevel++;
                    _roundData.CurrentRound = 1;
                    text = "You completed the level";
                }
                else
                {
                    text = "You Won the Round";
                }

                if (_roundData.CurrentLevel > _roundData.MaxLevels)
                {
                    text = "You Completed the Game";
                }
            }
            else
            {
                text = "You Lost the Round";
                _roundData.CurrentLevel = 1;
            }

            yield return _endWait;
        }

        private IEnumerator GameUpdate()
        {
            EnableControls();
            SetCrownPosition();

            if(!(Won() || Lost()))
            {
                yield return null;
            }
        }

        private void SetCrownPosition()
        {
            if (_leader != null)
            {
                _crown.gameObject.SetActive(true);
                Vector3 postion = _leader.transform.position + _crownOffset;
                _crown.position = postion;
            }
            else
            {
                _crown.gameObject.SetActive(false);
            }
        }

        private void SetLeader()
        {
            if (_playerPoints > _AIPoints)
                _leader = _player;
            else
                _leader = _AI;
        }

        private void SpawnAllLoops()
        {
            _player = Instantiate (_playerGO, _playerSpawnPoint.position, Quaternion.identity, null).GetComponent<LoopManager>();
            _AI = Instantiate (_AIGO, _AISpawnPoint.position, Quaternion.identity, null).GetComponent<LoopManager>();
        }

        private void EnableControls()
        {
            _player.EnableControls();
            _AI.EnableControls();
        }

        private void DisableControls()
        {
            _player.DisableControls();
            _AI.DisableControls();
        }

        public void AddScore(bool _isPlayer)
        {
            if (_isPlayer)
                _playerPoints ++;
            else
                _AIPoints++;

            SetLeader();
        }


        private bool Won()
        {
            return (_playerPoints > _pointsToWin);
        }

        private bool Lost()
        {
            return (_AIPoints > _pointsToWin);
        }
    }
}
