//#define usingSquared

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;

namespace CollisionBalls
{
    class Ball : SpriteGameObject
    {
        private float radius;
        private Vector2 acceleration;
        private Vector2 gravity;
        private float inelasticFriction;
        float bounceVector;
        float inverseMass;
        float totalInverseMass;


        public Ball(string assetName, Vector2 position, Vector2 velocity, Vector2 acceleration, Vector2 gravity, float radius, float inelastic = 1f, float bounceVector = 0.8f)
            : base(assetName, 0, "ball")
        {
            PerPixelCollisionDetection = false;
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Gravity = gravity;
            Origin = Center;
            Radius = radius;
            scale = radius * 2 / Width;
            InelasticFriction = inelastic;
            inverseMass = 1 / (radius * radius);
            this.bounceVector = bounceVector;

        }

        public override void Update(GameTime gameTime)
        {
            Bounce();
            base.Update(gameTime);
        }

        public void ResolveCollisionWith(Ball other)
        {
            //Step 1: calculate the vector from the position of this ball to the other ball
            Vector2 distanceBallDelta = other.position - this.position;

            //step 2: calculate the distance between the two balls
            float distance = distanceBallDelta.Length() - (other.radius + this.radius);

            //Step 3: if there is a collision
            if (distance < 0)
            {
                //Step 4: calculate the collision normal
                Vector2 collisionNormal = Vector2.Normalize(distanceBallDelta);

                //Step 5: Resolve interpenetration
                Vector2 resetVector = collisionNormal;
                resetVector *= distance;
                this.position += resetVector / 2;
                other.Position -= resetVector / 2;

                //Step 6: calculate the velocity component parallel to normal
                //Step 7: calculate the changeVelocity
                Vector2 ChangingVelocity = (1 + bounceVector) * Vector2.Dot(this.velocity - other.velocity, collisionNormal) * collisionNormal;

                //Step 8: change the velocities (assume equal mass)
                totalInverseMass = this.inverseMass + other.inverseMass;

                ChangingVelocity /= totalInverseMass;

                this.velocity -= ChangingVelocity * this.inverseMass;
                other.velocity += ChangingVelocity * other.inverseMass;
            }

        }

        public float InelasticFriction
        {
            get { return inelasticFriction; }
            set { inelasticFriction = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public Vector2 Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        private void Bounce()
        {
            if (position.X < radius)
            {
                position.X = radius;
                velocity.X *= -inelasticFriction;
            }
            if (position.X > GameEnvironment.Screen.X - radius)
            {
                position.X = GameEnvironment.Screen.X - radius;
                velocity.X *= -inelasticFriction;
            }
            if (position.Y < radius)
            {
                position.Y = radius;
                velocity.Y *= -inelasticFriction;
            }
            if (position.Y > GameEnvironment.Screen.Y - radius)
            {
                position.Y = GameEnvironment.Screen.Y - radius;
                velocity.Y *= -inelasticFriction;
            }

            Velocity += Gravity;
        }
    }
}