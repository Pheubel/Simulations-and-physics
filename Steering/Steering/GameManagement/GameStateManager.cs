using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameStateManager : IGameLoopObject {
    private readonly Dictionary<string, IGameLoopObject> gameStates;

    public GameStateManager() {
        gameStates = new Dictionary<string, IGameLoopObject>();
        CurrentGameState = null;
    }

    public void AddGameState(string name, IGameLoopObject state) {
        gameStates[name] = state;
    }

    public IGameLoopObject GetGameState(string name) {
        return gameStates[name];
    }

    public void SwitchTo(string name) {
        if (gameStates.ContainsKey(name))
            CurrentGameState = gameStates[name];
        else
            throw new KeyNotFoundException("Could not find game state: " + name);
    }

    public IGameLoopObject CurrentGameState { get; private set; }

    public void HandleInput(InputHelper inputHelper) {
        if (CurrentGameState != null)
            CurrentGameState.HandleInput(inputHelper);
    }

    public void Update(GameTime gameTime) {
        if (CurrentGameState != null)
            CurrentGameState.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        if (CurrentGameState != null)
            CurrentGameState.Draw(gameTime, spriteBatch);
    }

    public void Reset() {
        if (CurrentGameState != null)
            CurrentGameState.Reset();
    }
}