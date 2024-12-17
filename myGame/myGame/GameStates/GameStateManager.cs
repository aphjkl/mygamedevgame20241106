using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class GameStateManager
    {
        private Dictionary<string, BaseGameState> states;
        private BaseGameState currentState;
        private Game1 gameRef;

        public GameStateManager(Game1 game)
        {
            gameRef = game;
            states = new Dictionary<string, BaseGameState>();
        }

        public void AddState(string name, BaseGameState state)
        {
            states[name] = state;
        }

        public BaseGameState GetState(string name)
        {
            return states.ContainsKey(name) ? states[name] : null;
        }

        public void SwitchState(string stateName)
        {
            if (currentState != null)
                currentState.Exit();

            currentState = states[stateName];
            currentState.Enter();
        }

        public void SetState(GameState state)
        {
            string stateName = state.ToString();
            if (currentState != null)
                currentState.Exit();

            currentState = states[stateName];
            currentState.Enter();
        }

        public GameState CurrentState
        {
            get
            {
                foreach (var pair in states)
                {
                    if (pair.Value == currentState)
                    {
                        return (GameState)System.Enum.Parse(typeof(GameState), pair.Key);
                    }
                }
                return GameState.Menu;
            }
        }

        public void Update(GameTime gameTime)
        {
            currentState?.Update(gameTime);
        }

        public void Draw()
        {
            currentState?.Draw();
        }
    }
}