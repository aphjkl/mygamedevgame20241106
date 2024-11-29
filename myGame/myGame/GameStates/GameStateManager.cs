using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class GameStateManager
    {
        private Dictionary<string, GameState> states;
        private GameState currentGameState;

        public GameStateManager()
        {
            states = new Dictionary<string, GameState>();
        }

        public GameState CurrentState
        {
            get { return currentGameState; }
        }

        public void SetState(GameState newState)
        {
            currentGameState = newState;
        }
    }
}