using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [Header("Asteroid Data:")]
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private AsteroidScriptableObject[]  _asteroidData;


        [Header("Spawn information:")]
        [Range(0.0f, 10.0f)]
        public float _minSpawnTime;
        [Range(0f, 10.0f)]
        public float _maxSpawnTime;
        [Range(0, 10)]
        [SerializeField] private int _minAmount;
        [Range(0, 10)]
        [SerializeField] private int _maxAmount;



        [HideInInspector] public  Color asteroidColor ;
        
        private float _timer;
        private float _nextSpawnTime;
        private Camera _camera;

        private enum SpawnLocation
        {
            Top,
            Bottom,
            Left,
            Right
        }

        private void Start()
        {
            if (_minSpawnTime > _maxSpawnTime)
            {
                  float temp = _maxSpawnTime;
                _maxSpawnTime = _minSpawnTime;
                _minSpawnTime = _maxSpawnTime;

            }

            if (_minAmount > _maxAmount)
            {
                int temp = _maxAmount;
                _maxAmount = _minAmount;
                _maxAmount = _minAmount;

            }

            _camera = Camera.main;
            Spawn();
            UpdateNextSpawnTime();
        }

        private void Update()
        {
            UpdateTimer();

            if (!ShouldSpawn())
                return;

            Spawn();
            UpdateNextSpawnTime();
            _timer = 0f;
        }

        private void UpdateNextSpawnTime()
        {
            _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        }

        private void UpdateTimer()
        {
            _timer += Time.deltaTime;
        }

        private bool ShouldSpawn()
        {
            return _timer >= _nextSpawnTime;
        }

        private void Spawn()
        {
            var amount = Random.Range(_minAmount, _maxAmount + 1);
            
            for (var i = 0; i < amount; i++)
            {
                var location = GetSpawnLocation();
                var position = GetStartPosition(location);
                GameObject _tempAsteroid = Instantiate(_asteroidPrefab, position, Quaternion.identity);
                _tempAsteroid.GetComponent<Asteroid>().asteroidData = _asteroidData[Random.Range(0, _asteroidData.Length)];
                _tempAsteroid.GetComponent<Asteroid>().sprite.color = asteroidColor;
            }

        }

        private static SpawnLocation GetSpawnLocation()
        {
            var roll = Random.Range(0, 4);

            return roll switch
            {
                1 => SpawnLocation.Bottom,
                2 => SpawnLocation.Left,
                3 => SpawnLocation.Right,
                _ => SpawnLocation.Top
            };
        }

        private Vector3 GetStartPosition(SpawnLocation spawnLocation)
        {
            var pos = new Vector3 { z = Mathf.Abs(_camera.transform.position.z) };
            
            const float padding = 5f;
            switch (spawnLocation)
            {
                case SpawnLocation.Top:
                    pos.x = Random.Range(0f, Screen.width);
                    pos.y = Screen.height + padding;
                    break;
                case SpawnLocation.Bottom:
                    pos.x = Random.Range(0f, Screen.width);
                    pos.y = 0f - padding;
                    break;
                case SpawnLocation.Left:
                    pos.x = 0f - padding;
                    pos.y = Random.Range(0f, Screen.height);
                    break;
                case SpawnLocation.Right:
                    pos.x = Screen.width - padding;
                    pos.y = Random.Range(0f, Screen.height);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spawnLocation), spawnLocation, null);
            }
            
            return _camera.ScreenToWorldPoint(pos);
        }
    }
}
