using UnityEngine;
using Text = UnityEngine.UI.Text;
using RoundData = Loop.Data.RoundData;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
using Singleton = Pixelplacement.Singleton<Loop.Managers.GameManager>;

namespace Loop.Managers
{
    public enum GameState 
    {
        Start, Running, End
    }

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

        [Header("UI Stuff")]

        [SerializeField] private Text _playerScoreText;
        [SerializeField] private Text _AIScoreText;

        [Header("Debug")]

        [SerializeField] private GameState _gameState = GameState.Start;

        
        private float _playerPoints;
        private float _AIPoints;
        private LoopManager _AI;
        private LoopManager _player;
        private LoopManager _leader;
        private float _timer;

        private void Start()
        {
            _timer = 0f;
            SpawnAllLoops();
            _gameState = GameState.Start;
        }

        private void Update() 
        {
            switch(_gameState)    
            {
                case GameState.Start : GameStart(); break;
                case GameState.Running : GameUpdate(); break;
                case GameState.End : GameEnd(); break;
            }
        }

        private void GameStart()
        {
            Debug.Log("start");
            DisableControls();
            _leader = null;
            _crown.gameObject.SetActive(false);

            _timer += Time.deltaTime;
            if (_timer > _startDelay) 
            {
                _gameState = GameState.Running;
            }
        }

        private void GameUpdate()
        {
            EnableControls();
            SetCrownPosition();

            if (Won() || Lost())
            {
                _timer = 0f;
                _gameState = GameState.End;
            }
        }

        private void GameEnd()
        {
            DisableControls();
            Debug.Log("end");
            var text = "";
            if (Won())
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

            _timer += Time.deltaTime;
            if (_timer > _endDelay)
            {
                SceneManager.LoadScene(1);
            }
        }

        private void SetText()
        {
            _playerScoreText.text = _playerPoints.ToString();
            _AIScoreText.text = _AIPoints.ToString();
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

            SetText();
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
