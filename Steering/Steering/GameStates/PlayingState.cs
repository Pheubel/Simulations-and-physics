using Microsoft.Xna.Framework;

namespace Opdracht3_Steering {
    internal class PlayState : GameObjectList {
        public PlayState() {
            var background = new SpriteGameObject("blue_stars");
            Add(background);

            var ball = new Target("PurpleSoftColorBall", new Vector2(600, 400), 0.5f);
            Add(ball);

            var ship = new SpaceShip("blueship3", new Vector2(600, 400), 1f, 0.2f, 7f, 150f, 1f);
            Add(ship);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}