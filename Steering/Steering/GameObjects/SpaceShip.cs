//#define oudeManier

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Opdracht3_Steering
{
    internal class SpaceShip : SpriteGameObject
    {
        protected float mass;
        private Vector2 target;
        private float rotation;

        // add maxSteering, maxSpeed and arrivingRadius
        const float maxSteering = 5f;
        const float maxSpeed = 300;
        const float arrivingRadiusSqr = 250000f;


        public SpaceShip(string assetName, Vector2 position, float scale, float maxSteering, float maxSpeed, float arrivingRadius, float mass)
            : base(assetName, 1, "spaceship")
        {
            layer = 2;
            this.position = position;
            velocity = Vector2.Zero;
            this.mass = mass;
            rotation = 0;
            origin = new Vector2(Width / 2f, Height / 2f);
        }

        public override void Update(GameTime gameTime)
        {
            // get target
            target = GameWorld.Find("target").Position;

            Vector2 deltaVector = target - position;

            // calculate steering direction
            Vector2 steering = Truncate(deltaVector, maxSteering);
            steering = steering / mass;
            
            // arriving and stopping
            velocity = Truncate(velocity + steering, deltaVector.LengthSquared() >= arrivingRadiusSqr ? maxSpeed : (deltaVector.LengthSquared() / arrivingRadiusSqr) * maxSpeed);


            // apply rotation
            if (velocity != Vector2.Zero)
            {
                var angle = (float)Math.Atan2(velocity.Y, velocity.X);
                rotation = angle + (float)Math.PI / 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!visible || sprite == null)
                return;

            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White,
            rotation, origin, scale, SpriteEffects.None, 0.0f);
        }

        private Vector2 Truncate(Vector2 vector, float length)
        {
            if (!(vector.LengthSquared() > length * length)) return vector;
            vector.Normalize();
            vector *= length;
            return vector;
        }
    }
}