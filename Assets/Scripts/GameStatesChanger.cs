using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameStateObjects
    {
        [SerializeField] private GameObject[] _objects;

        public IEnumerable<GameObject> Objects => _objects;
    }

    public class GameStatesChanger : MonoBehaviour
    {
        [SerializeField] private GameStateObjects _quizGame;
        [SerializeField] private GameStateObjects _lettersGame;
        [SerializeField] private GameStateObjects _objectsGame;
        [SerializeField] private GameStateObjects _menuObjects;

        private GameStateObjects _currentGameStateObjects;

        private void OffCurrentGameStateObjects()
        {
            if (_currentGameStateObjects != null)
            {
                foreach (var gameStateObject in _currentGameStateObjects.Objects)
                    gameStateObject.SetActive(false);
            }
        }

        private void OnCurrentGameStateObjects()
        {
            if (_currentGameStateObjects != null)
            {
                foreach (var gameStateObject in _currentGameStateObjects.Objects)
                    gameStateObject.SetActive(true);
            }
        }

        private void SetGameState(GameStateObjects gameStateObjects)
        {
            OffCurrentGameStateObjects();
            _currentGameStateObjects = gameStateObjects;
            OnCurrentGameStateObjects();
        }

        public void LoadQuizGame()
        {
            SetGameState(_quizGame);
        }

        public void LoadLettesGame()
        {
            SetGameState(_lettersGame);
        }

        public void LoadObjectsGame()
        {
            SetGameState(_objectsGame);
        }

        public void LoadMenuGame()
        {
            SetGameState(_menuObjects);
        }

    }
}