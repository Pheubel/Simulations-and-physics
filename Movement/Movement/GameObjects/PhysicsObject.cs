using Microsoft.Xna.Framework;
using System;

namespace Movement {
    class PhysicsObject : RotatingSpriteGameObject{
        private readonly float radius;
        private float mass;
        private Vector2 force;
        private Vector2 acceleration;

        public PhysicsObject(string assetName, Vector2 position, Vector2 velocity, float radius, Vector2 acceleration, float mass = 1f)
            : base(assetName) {
            this.mass = mass;
            PerPixelCollisionDetection = false;
            Position = position;
            Velocity = velocity;
            Origin = Center;
            this.radius = radius;
            scale = radius * 2 / Width;
            this.acceleration = acceleration;
            force = mass * acceleration;
        }

        public override void Update(GameTime gameTime) {
            if (position.X < radius) {
                position.X = radius;
                velocity.X *= -1f;
            }
            if (position.X > GameEnvironment.Screen.X - radius) {
                position.X = GameEnvironment.Screen.X - radius;
                velocity.X *= -1f;
            }
            if (position.Y < radius) {
                position.Y = radius;
                velocity.Y *= -1f;
            }
            if (position.Y > GameEnvironment.Screen.Y - radius) {
                position.Y = GameEnvironment.Screen.Y - radius;
                velocity.Y *= -1f;
            }

            Velocity += acceleration;
            base.Update(gameTime);
        }

        /// <summary> Sets the force to the given Vector2 and adjusts the acceleration to the new force.</summary>
        public virtual Vector2 Force {
            get { return force; }
            set { force = value; acceleration = force / mass; }
        }

        /// <summary> Sets the acceleration to the given Vector2 and adjusts the force to the new accelertion.</summary>
        public virtual Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value;  force = acceleration * mass; }
        }

        /// <summary> Sets the mass to the given float value and adjusts the force to the new mass.</summary>
        public virtual float Mass
        {
            get { return mass; }
            set { mass = value; force = acceleration * mass; }
        }
    }
}
