using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace myGame.GameStates
{
    public class GameStateManager
    {
        private Dictionary<string, BaseGameState> states;
        private BaseGameState currentState;
        private GameState currentGameState;

        public GameStateManager()
        {
            states = new Dictionary<string, BaseGameState>();
        }

        public GameState CurrentState => currentGameState;

        public void AddState(string name, BaseGameState state)
        {
            states[name] = state;
        }

        public void SwitchState(string stateName)
        {
            if (currentState != null)
                currentState.Exit();

            if (states.TryGetValue(stateName, out BaseGameState newState))
            {
                currentState = newState;
                currentState.Enter();
                
                switch (stateName.ToLower())
                {
                    case "menu":
                        currentGameState = GameState.StartScreen;
                        break;
                    case "playing":
                        currentGameState = GameState.Playing;
                        break;
                    case "gameover":
                        currentGameState = GameState.GameOver;
                        break;
                }
            }
        }

        public void SetState(GameState newState)
        {
            string stateName = newState switch
            {
                GameState.StartScreen => "Menu",
                GameState.Playing => "Playing",
                GameState.GameOver => "GameOver",
                _ => throw new ArgumentException("Invalid state")
            };
            SwitchState(stateName);
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