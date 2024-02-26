using System.Collections.Generic;
using UnityEngine;  

namespace _Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Lógica de generacion de enemigos")]
        public float timeBetweenEnemyGenerations = 6;

        public float enemySpawnMinDistance = 5;
        public float enemySpawnMaxDistance = 12;
        public float enemyDespawnDistance = 30;
        public GameObject enemyPrefab;

        [Header("Lógica de dificultad del juego")]
        public int numLifePlayer = 3;

        public int receiveDam = 3;
        public int initialPoints = 0;
        public int pointsToKillEnemy;
        public int pointsToWinLife = 19900;
        public GameObject playerPrefab;
        public int pointsToLevelUp;
        protected float _lastEnemyGenerationTimeStamp;

        protected List<Transform> _enemyTransforms;
        protected List<Transform> _enemiesToRemove;
        protected Transform _playerTransform;
        protected int receiveDamCopy; 
        private int checkLifeCounter = 0;
        public SoundManager mySoundManager;
        private int counterPointsToLevelUp = 0;
        private int counterPointsToWinLife = 0;
        private int levelPlayer = 1;
        private float _lastHeight;
 
        void Awake()
        {
            startGame();
            _playerTransform = GameObject.Find("Cube [Player](Clone)").transform;
        }

        void Start()
        {
            StartCoroutine(CheckForNewLife());
            StartCoroutine(CheckForLevelUpp());
            StartCoroutine(CheckPlayerHeight()); 
        }
        void Update()
        {
            if (playerPrefab == null)
            {
                return;
            }
 
            // Controlar el tiempo que pasa entre generaciones
            if (Time.realtimeSinceStartup >= _lastEnemyGenerationTimeStamp + timeBetweenEnemyGenerations)
            {
                _lastEnemyGenerationTimeStamp = Time.realtimeSinceStartup;
                var distanceToPlayer = Random.Range(enemySpawnMinDistance, enemySpawnMaxDistance);
                var randomVector = Random.onUnitSphere;
                randomVector.y = 0;
                var spawnPosition = _playerTransform.position + randomVector.normalized * distanceToPlayer; 
                _enemyTransforms.Add(Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).transform);
            }

            foreach (var enemyTransform in _enemyTransforms)
            {
                if (enemyTransform == null || Vector3.Distance(enemyTransform.position, _playerTransform.position) >=
                    enemyDespawnDistance)
                {
                    _enemiesToRemove.Add(enemyTransform);
                }
            }

            foreach (var enemyToRemove in _enemiesToRemove)
            {
                // Destruyo el enemigo
                _enemyTransforms.Remove(enemyToRemove);
                if (enemyToRemove != null)
                {
                    Destroy(enemyToRemove.gameObject);
                }
            } 
            _enemiesToRemove.Clear();
        }

        public void RestarVida()
        {
            numLifePlayer -= 1;
            UI_Manager.instance.restOneHeart();
            receiveDam = receiveDamCopy;
            UI_Manager.instance.updateDamageSlide();
            SoundManager.instance.AudioLoseOneLive();
        }
        public void receiveDamage()
        {
            receiveDam -= 1;
            SoundManager.instance.AudioGetDamage();
            UI_Manager.instance.updateDamageSlide();
            if (receiveDam < 0)
            {
                RestarVida();
            } 
        }

        public void startGame()
        {
            /* Buscamos el padre de nuestro jugador para cuando lo instanciemos esté más ordenado */
            GameObject parentObject = GameObject.Find("Player");
            Instantiate(playerPrefab, new Vector3(0, 06f, 0), Quaternion.identity, parentObject.transform);
            playerPrefab.SetActive(true);
            playerPrefab.GetComponent<NumLives>().setLive(numLifePlayer);

            _enemyTransforms = new List<Transform>();
            _enemiesToRemove = new List<Transform>(); 
            
            _playerTransform = playerPrefab.transform;
            //Nos guardamos copia por si modificamos en el inspector el valor poder reinincializarlo
            receiveDamCopy = receiveDam;
        }
        
        public IEnumerator<object> CheckForNewLife()
        {
            while (true)
            { 
                if (getCounterPointsToWinLife() >= pointsToWinLife )
                {
                    UI_Manager.instance.addOneHeart(); 
                    resetCounterPointsToWinLife();
                    yield return new WaitForSeconds(5f);
                }
                yield return null;
            }
        }
        
        public IEnumerator<object> CheckForLevelUpp()
        {
            while (true)
            { 
                if (getCounterPointsToLevelUp() >= pointsToLevelUp)
                { 
                    UI_Manager.instance.makeLevelUp(); 
                    SoundManager.instance.AudioLevelUp();
                    resetCounterPointsToLevelUp();
                    setLevel(1);
                    yield return new WaitForSeconds(5f);
                } 
                yield return null;
            }
        }

        public IEnumerator<object> CheckPlayerHeight()
        {
            while (true)
            {
                float height = Player.instance.transform.position.y;
                if (_lastHeight - height > 20f)
                { 
                    UI_Manager.instance.restOneHeart();
                    Player.instance.cantMove();
                    Player.instance.transform.position = new Vector3(0, 6f, 0); // Posición inicial del jugador
                    Player.instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    MapGenerator.instance.ReiniciarMapa(height - 100);
                    Player.instance.setcanMove();
                    break;
                }
                _lastHeight = height;
                yield return new WaitForSeconds(1f); // Verificar la altura cada segundo
            }
        }
 
        public int GetReceiveDam() { return receiveDam; }
        public int getPoints() { return initialPoints; }
        public void setInitialpoints(int points) { initialPoints = points; }
        public int getPointsToKillEnemy() { return pointsToKillEnemy; }
        public int GetPointsToWinLife() { return pointsToWinLife; }
        public int GetNumLives() { return numLifePlayer; }
        public GameObject getPlayer() { return playerPrefab; }
        public int getPointsTolevelUp() { return pointsToLevelUp; }
        public int getCounterPointsToLevelUp() { return counterPointsToLevelUp; }
        public void resetCounterPointsToLevelUp() { counterPointsToLevelUp = 0; }
        public void addCounterPointsToLevelUp(int pointsToAdd) { counterPointsToLevelUp += pointsToAdd;}
        public int getCounterPointsToWinLife() { return counterPointsToWinLife; }
        public void addCounterPointsToWinLife(int pointsToAdd) { counterPointsToWinLife += pointsToAdd;}
        public void resetCounterPointsToWinLife() { counterPointsToWinLife = 0; }
        public int getLevel() { return levelPlayer; }
        public void setLevel(int newLevel) { levelPlayer += newLevel; }
    }
}


