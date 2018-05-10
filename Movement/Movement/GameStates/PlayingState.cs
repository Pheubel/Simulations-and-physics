using System;
using Microsoft.Xna.Framework;

namespace Movement {
    class PlayingState : GameObjectList{

        private PhysicsObject _target;

        public PlayingState() {
            // The target for the spaceship to go after.
            _target = new PhysicsObject("spr_ball_green", GameEnvironment.Screen.ToVector2() / 2, Vector2.Zero, 30, Vector2.Zero)
            {
                Visible = false
            };

            // The spaceship
            SpaceShip spaceShip = new SpaceShip("spr_spaceship", GameEnvironment.Screen.ToVector2() / 2, _target);

            // The shield
            Shield shield = new Shield("GreenSoftColorBall", spaceShip,Vector2.Zero);

            Add(shield);
            Add(_target);
            Add(spaceShip);
        }

        public override void HandleInput(InputHelper inputHelper) {

            // Sets the target to the mouse's position when M1 is pressed.
            if (inputHelper.MouseLeftButtonPressed())
            {
                _target.Position = inputHelper.MousePosition; 
                _target.Visible = true;
            }

            base.HandleInput(inputHelper);
        }
    }
}
