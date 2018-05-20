using Microsoft.Xna.Framework;
using System;

namespace Motion {
    class PlayingState : GameObjectList {
        public PlayingState() {

            // The red ball moves horizontally from left to right
            Ball redBall = new Ball(
                "RedBallX", 
                new Vector2(0,GameEnvironment.Screen.Y/2),
                Vector2.Zero, 
                30,
                new Vector2(1,0)
            );

            // The pink ball moves diagonally from the lower left to the upper right
            Ball pinkBall = new Ball(
                "PinkSoftColorBall",
                new Vector2(0, GameEnvironment.Screen.Y),
                GetVelocityToPoint(
                    new Vector2(0, GameEnvironment.Screen.Y), 
                    new Vector2(GameEnvironment.Screen.X, 0),
                    100),
                30,
                null,
                0.3f
            );

            // The purple ball moves from the top to the bottom.
            Ball purpleBall = new Ball(
                "PurpleSoftColorBall",
                new Vector2(GameEnvironment.Screen.X / 2, 0),
                new Vector2(0,100),
                30,
                null,
                0.3f,
                0.7f
            );


            // add the references to the playing state
            Add(redBall);
            Add(pinkBall);
            Add(purpleBall);
        }

        /// <summary> Gets a 2-dimensional vector that will point towards the end position with a length of the given magnitude.</summary>
        /// <param name="startPoint"> The staring point of the velocity vector.</param>
        /// <param name="endPoint"> The point the velocity vector will aim at.</param>
        /// <param name="magnitude"> The length of the velocity vector.</param>
        /// <returns> Vector pointing to the end position with the length of the given magnitude</returns>
        private Vector2 GetVelocityToPoint(Vector2 startPoint, Vector2 endPoint, float magnitude)
        {
            Vector2 deltaVector = endPoint - startPoint;
            deltaVector.Normalize();
            return deltaVector * magnitude;
        }
    }
}
