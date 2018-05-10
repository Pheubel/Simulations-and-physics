using Microsoft.Xna.Framework;

namespace Movement {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Movement : GameEnvironment {
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            base.LoadContent();
            Screen = new Point(1200, 1000);
            ApplyResolutionSettings();

            GameStateManager.AddGameState("PlayingState", new PlayingState());
            GameStateManager.SwitchTo("PlayingState");

            IsMouseVisible = true;
        }
    }
}
