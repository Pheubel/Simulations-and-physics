//#define usingOldMich
//#define usingNewOldMich

#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Opdracht6_Transformations
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SimPhyGameWorld : Game
    {
        GraphicsDeviceManager graphDev;
        Color background = new Color(20, 0, 60);
        public static SimPhyGameWorld World;
        Vector3 cameraPosition = new Vector3(0, 30, 80);
        public Matrix View;
        public Matrix Projection;
        public static GraphicsDevice Graphics;

        #region solarsystem
        List<Sphere> spheres;

        Sphere sun;
        Sphere earth;
        Sphere moon;
        Sphere mars;
        Sphere jupiter;
        Sphere saturnus;
        Sphere uranus;

        float moonOrbitRot = 0;
        #endregion

        #region De Michelin Man

        # region Hoofd
        private Sphere neck;
        private Sphere headLower;
        private Sphere headUpper;

        private Sphere leftEye;
        private Sphere rightEye;

        //bones
        private Bone b_neck;
        private Bone b_headLower;
        private Bone b_headUpper;

        private Bone b_leftEye;
        private Bone b_rightEye;
        #endregion

        #region Lichaam
        private Sphere body1;
        private Sphere body2;
        private Sphere body3;
        private Sphere body4;

        //bones
        private Bone b_body1;
        private Bone b_body2;
        private Bone b_body3;
        private Bone b_body4;
        #endregion

        #region Armen
        private Matrix rotUpperLeftArm, rotUpperRightArm;
        private Vector3 endUpperLeftArm, endUpperRightArm;

        private Sphere armLeftUpper, armRightUpper;
        private Sphere armLeftElbow, armRightElbow;
        private Sphere armLeftLower, armRightLower;
        private Sphere leftHand, rightHand;

        //bones
        private Bone b_armLeftUpper, b_armRightUpper;
        private Bone b_armLeftElbow, b_armRightElbow;
        private Bone b_armLeftLower, b_armRightLower;
        private Bone b_leftHand, b_rightHand;
        #endregion

        #region //Benen
        private Matrix rotUpperLeftLeg, rotUpperRightLeg;
        private Vector3 endUpperLeftLeg, endUpperRightLeg;

        private Matrix rotLeftUpper, rotLeftFoot;
        private Sphere legLeftUpper;
        private Sphere legLeftKnee;
        private Sphere legLeftLower;
        private Sphere leftFoot;

        private Sphere legRightUpper;
        private Sphere legRightKnee;
        private Sphere legRightLower;
        private Sphere rightFoot;

        //bones
        private Bone b_legLeftUpper;
        private Bone b_legLeftKnee;
        private Bone b_legLeftLower;
        private Bone b_leftFoot;

        private Bone b_legRightUpper;
        private Bone b_legRightKnee;
        private Bone b_legRightLower;
        private Bone b_rightFoot;
        #endregion
        #endregion

        

        private MichelinMan mich;


        public SimPhyGameWorld()
            : base()
        {
            Content.RootDirectory = "Content";

            World = this;
            graphDev = new GraphicsDeviceManager(this);
        }
        protected override void Initialize()
        {
            Graphics = GraphicsDevice;

            graphDev.PreferredBackBufferWidth = 1280;
            graphDev.PreferredBackBufferHeight = 800;
            graphDev.IsFullScreen = false;
            graphDev.ApplyChanges();

            SetupCamera(true);

            Window.Title = "HvA - Simulation & Physics - Opdracht 5 - Transformations - press <> to rotate camera";

            #region solarSystem

            spheres = new List<Sphere>();

            // Step 7: Create the moon (radius, distance and color as given)     

            //initialize all solar bodies
            sun = new Sphere(Matrix.Identity * Matrix.CreateScale(2), Color.Yellow, 30, RandomHelper.FloatBetween(0.15f, 0.5f));
            earth = new Sphere(Matrix.Identity * Matrix.CreateTranslation(16, 0, 0), Color.Navy, 30, RandomHelper.FloatBetween(0.15f, 0.5f));
            moon = new Sphere(Matrix.Identity, Color.LightGray, 30, 1.5f);
            mars = new Sphere(Matrix.Identity * Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(21, 0, 0), Color.Red, 30, RandomHelper.FloatBetween(0.15f, 0.5f));
            jupiter = new Sphere(Matrix.Identity * Matrix.CreateScale(1.7f) * Matrix.CreateTranslation(27, 0, 0), Color.Orange, 30, RandomHelper.FloatBetween(0.15f, 0.5f));
            saturnus = new Sphere(Matrix.Identity * Matrix.CreateScale(1.6f) * Matrix.CreateTranslation(36, 0, 0), Color.Khaki, 30, RandomHelper.FloatBetween(0.15f, 0.5f));
            uranus = new Sphere(Matrix.Identity * Matrix.CreateScale(1.5f) * Matrix.CreateTranslation(43, 0, 0), Color.Cyan, 30, RandomHelper.FloatBetween(0.15f, 0.5f));


            // add the spheres to the sphere list.
            spheres.Add(sun);
            spheres.Add(earth);
            spheres.Add(moon);
            spheres.Add(mars);
            spheres.Add(jupiter);
            spheres.Add(saturnus);
            spheres.Add(uranus);
            #endregion



            mich = new MichelinMan();
            mich.Origin.SetLocalTranslation(Matrix.CreateTranslation(0, 15, 0));
            mich.LeftKnee.SetLocalRotation(Matrix.CreateRotationX(MathHelper.ToRadians(20)));
            mich.RightKnee.SetLocalRotation(Matrix.CreateRotationX(MathHelper.ToRadians(20)));
            mich.RightElbow.SetLocalRotation(Matrix.CreateRotationY(MathHelper.ToRadians(45)));
            mich.RightHandJoint.SetLocalRotation(Matrix.CreateRotationX(MathHelper.ToRadians(40)));

            //mich.Origin.SetLocalTranslation(Matrix.CreateTranslation(0, 19f, 0));


            base.Initialize();
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            IsMouseVisible = true;
        }

        private void SetupCamera(bool initialize = false)
        {
            View = Matrix.CreateLookAt(cameraPosition, new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            if (initialize) Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, SimPhyGameWorld.World.GraphicsDevice.Viewport.AspectRatio, 1.0f, 300.0f);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background);

            foreach (Sphere sphere in spheres)
            {
                sphere.Draw();
            }

            mich.Draw();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            //mich.Origin.ApplyLocalRotation(Matrix.CreateRotationX((float)gameTime.TotalGameTime.Seconds));
            //mich.Origin.SetLocalTranslation(Matrix.CreateTranslation(0,(float)gameTime.TotalGameTime.Seconds,0));
            Console.WriteLine("{" + mich.Skeleton[MichelinMan.Bones.LeftEye].LocalTransform.Translation + "}");
            //mich.LeftArm.ApplyLocalTranslation(Matrix.CreateTranslation((float)gameTime.ElapsedGameTime.TotalSeconds/10, 0, 0));

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // Step 10: Make the camera position rotate around the origin depending on gameTime.ElapsedGameTime.TotalSeconds
                cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY((float)gameTime.ElapsedGameTime.TotalSeconds * 1.3f));


                SetupCamera();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // Step 10: Make the camera position rotate around the origin depending on gameTime.ElapsedGameTime.TotalSeconds
                cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY((float)gameTime.ElapsedGameTime.TotalSeconds * -1.3f));
                SetupCamera();
            }

            // go through each sphere to apply rotation
            foreach (Sphere celestialBody in spheres)
            {
                if (celestialBody == moon)
                {
                    moonOrbitRot += moon.RotationSpeedAroundPivot * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    moonOrbitRot = MathHelper.WrapAngle(moonOrbitRot);

                    // Step 8: Change the orbit of the moon such that it is rotated 45 degrees toward the sun/origin(see example!)
                    moon.SetTransform( Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(2, 0, 0) *
                        Matrix.CreateRotationY(moonOrbitRot) *
                        Matrix.CreateRotationZ(0.78f) *
                        earth.Transform);
                }
                else
                    celestialBody.ApplyTransform( Matrix.CreateRotationY(MathHelper.WrapAngle(celestialBody.RotationSpeedAroundPivot * (float)gameTime.ElapsedGameTime.TotalSeconds)));

            }


            
            mich.RightArm.SetLocalRotation(Matrix.CreateFromYawPitchRoll(0, (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds), MathHelper.ToRadians(60)));
            mich.RightElbow.SetLocalRotation(Matrix.CreateFromYawPitchRoll((MathHelper.ToRadians(45) - (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) / 2), (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) / 4,0));

            mich.LeftLeg.SetLocalRotation(Matrix.CreateRotationX((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)));
            mich.RightLeg.SetLocalRotation(Matrix.CreateRotationX(-(float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)));

            //wave to the viewer :D
            mich.LeftArm.SetLocalRotation(Matrix.CreateRotationZ(MathHelper.ToRadians(10) + ((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) / 16));
            mich.LeftElbow.SetLocalRotation(Matrix.CreateRotationZ(MathHelper.ToRadians(40) + ((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds))/8));
            mich.LeftHand.SetLocalRotation(Matrix.CreateRotationZ(((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) / 2));

            //extra feet rotation
            mich.LeftAnkle.SetLocalRotation(Matrix.CreateRotationX((-(float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) / 8));
            mich.RightAnkle.SetLocalRotation(Matrix.CreateRotationX(((float)Math.Sin(gameTime.TotalGameTime.TotalSeconds)) / 8));


            //wiggle the body a bit
            mich.Origin.SetLocalRotation(Matrix.CreateRotationX(((float)Math.Sin((gameTime.TotalGameTime.TotalSeconds - Math.PI/2) *2)) / 16));
            mich.Origin.SetLocalRotation(Matrix.CreateRotationZ((-(float)Math.Sin((gameTime.TotalGameTime.TotalSeconds - Math.PI / 2))) / 16));


            base.Update(gameTime);
        }
    }
}
