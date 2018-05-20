#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace Opdracht3_Steering {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SimPhyGameWorld : GameEnvironment {
        public SimPhyGameWorld() {
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            base.LoadContent();

            screen = new Point(1280, 800);
            SetFullScreen(false);
            gameStateManager.AddGameState("playState", new PlayState());

            gameStateManager.SwitchTo("playState");
            IsMouseVisible = true;
        }
    }
}
