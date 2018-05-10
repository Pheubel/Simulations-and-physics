using Microsoft.Xna.Framework;
using System;

namespace Movement {
    class Shield : PhysicsObject{

        const float SPRINGCONST = 5f;

        public Shield(string assetName, SpaceShip ship, Vector2 velocity)
            : base(assetName, ship.Position + new Vector2(30, 30), velocity, 15f, Vector2.Zero) {
            targetObject = ship;
        }

        public override void Update(GameTime gameTime) {
            Force = (targetObject.Position - this.Position) * SPRINGCONST;
            
            base.Update(gameTime);
        }
    }
}
