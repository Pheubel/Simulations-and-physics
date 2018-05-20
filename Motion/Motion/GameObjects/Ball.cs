using System;
using Microsoft.Xna.Framework;

namespace Motion
{
    class Ball : SpriteGameObject
    {
        private float radius;

        private Vector2 _acceleration;
        private float _gravity;
        private float _inelasticity;

        public Ball(string assetName, Vector2 position, Vector2 velocity, float radius, Vector2? acceleration = null, float gravity = 0, float inelasticity = 1)
            : base(assetName, 0, "ball")
        {
            PerPixelCollisionDetection = false;
            Position = position;
            Velocity = velocity;
            _acceleration = acceleration ?? Vector2.Zero;
            _gravity = gravity;
            _inelasticity = MathHelper.Clamp( inelasticity,0,1);
            Origin = Center;
            this.radius = radius;
            scale = radius * 2 / Width;
        }

        public override void Update(GameTime gameTime)
        {
            // acceleration of the ball
            if (velocity.X < 0)
                velocity.X -= _acceleration.X;
            else
                velocity.X += _acceleration.X;

            if (velocity.Y < 0)
                velocity.Y -= _acceleration.Y;
            else
                velocity.Y += _acceleration.Y;


            velocity.Y += _gravity;

            KeepInScreen();

            base.Update(gameTime);
        }

        private void KeepInScreen()
        {
            // set the fixed values to the old vectors
            float fixedPositionX = position.X;
            float fixedPositionY = position.Y;
            float fixedVelocityX = velocity.X;
            float fixedVelocityY = velocity.Y;

            // the ball tries to leave on the left side of the screen.
            if (position.X - radius < 0 && velocity.X < 0)  
            {
                fixedVelocityX *= -1 * _inelasticity;
                fixedPositionX = radius;
            }

            // the ball tries to leave on the right side
            else if (position.X + radius > GameEnvironment.Screen.X && velocity.X > 0) 
            {
                fixedVelocityX *= -1 * _inelasticity;
                fixedPositionX = GameEnvironment.Screen.X - radius;
            }

            // the ball tries to leave on the top side of the screen.
            if (position.Y - radius < 0 && velocity.Y < 0)
            {
                fixedVelocityY *= -1 * _inelasticity;
                fixedPositionY = radius;
            }

            // the ball tries to leave on the bottom side
            else if (position.Y + radius > GameEnvironment.Screen.Y && velocity.Y > 0)
            {
                fixedVelocityY *= -1 * _inelasticity;
                fixedPositionY = GameEnvironment.Screen.Y - radius;
            }

            // set the velocity and position to the fixed values
            velocity = new Vector2(fixedVelocityX, fixedVelocityY);
            position = new Vector2(fixedPositionX, fixedPositionY);
        }
    }
}
