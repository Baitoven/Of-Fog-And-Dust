using OfFogAndDust.Dialogue.Parsing;
using OfFogAndDust.Dialogue.Types;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfFogAndDust.Game
{
    internal class GameManager : MonoBehaviour
    {
        internal enum GameState
        {
            Town,
            Expedition,
            Combat
        }

        public static GameManager Instance;

        internal GameState currentGameState;
        internal Dictionary<int, TDialogue> dialogueBank = new Dictionary<int, TDialogue>();
        internal Dictionary<int, TChoice> choiceBank = new Dictionary<int, TChoice>();

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            ContentParser parser = new ContentParser();
            parser.ParseDialogue();
            parser.ParseChoice();
        }

        private void LoadLevel(string name)
        {
            // prob need async loading here
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }

        public void LaunchExpedition()
        {
            if (currentGameState == GameState.Town)
            {
                currentGameState = GameState.Expedition;

                LoadLevel("Map");
            }
        }

        public void LaunchCombat()
        {
            if (currentGameState == GameState.Expedition)
            {
                currentGameState = GameState.Combat;

                LoadLevel("Combat");
            }
        }

        public void ReturnToTown()
        {
            if (currentGameState == GameState.Expedition)
            {
                currentGameState = GameState.Town;

                LoadLevel("Town");
            }
        }
    }
}
