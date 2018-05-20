using Microsoft.Xna.Framework;

namespace Opdracht3_Steering {
    internal class Target : SpriteGameObject {

        public Target(string assetName, Vector2 position, float scale)
            : base(assetName, 1, "target") {
            this.position = position;
            origin = new Vector2(Width / 2f, Height / 2f);
            this.scale = scale;
        }

        public override void HandleInput(InputHelper inputHelper) {
            if (inputHelper.MouseLeftButtonPressed()) {
                position = inputHelper.MousePosition;
            }

            base.HandleInput(inputHelper);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}