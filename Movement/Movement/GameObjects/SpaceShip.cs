using Microsoft.Xna.Framework;
using System;

namespace Movement {
    class SpaceShip : PhysicsObject{

        Vector2 _direction;//_direction = (targetObject.Position - position);

        //Step 2.3: Declare a variable for keeping track of the rotation of the ship

        public SpaceShip(string assetName, Vector2 position, PhysicsObject target)
            : base(assetName, position, Vector2.Zero, 50f, Vector2.Zero) {
            layer = 2;
            targetObject = target;
        }

        //Step 2.5: Set the target position of the ship to the mouse position when the left button is pressed.
        //Tip: override HandleInput

        public override void Update(GameTime gameTime) {

            // Checks if the spaceship is not near it's target
            if ((targetObject.Position - this.Position).LengthSquared() > 1)
            {
                this.Velocity = (targetObject.Position - this.Position) * 0.5f;
                LookAt(targetObject,-90);
            }
            else
            {
                velocity = Vector2.Zero;
            }

            base.Update(gameTime);
        }
    }
}
