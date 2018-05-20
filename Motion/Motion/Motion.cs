using Microsoft.Xna.Framework;

namespace Motion {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Motion : GameEnvironment {
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            base.LoadContent();
            Screen = new Point(1000, 552);
            ApplyResolutionSettings();

            GameStateManager.AddGameState("PlayingState", new PlayingState());
            GameStateManager.SwitchTo("PlayingState");
        }

    }
}
