﻿#region Using Statements
using System;
using System.Collections.Generic;
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

        List<Sphere> spheres;

        Sphere sun;
        Sphere earth;
        Sphere moon;
        Sphere mars;
        Sphere jupiter;
        Sphere saturnus;
        Sphere uranus;

        float moonOrbitRot = 0;


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

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
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
                    moon.Transform = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(2, 0, 0) *
                        
                        Matrix.CreateRotationY(moonOrbitRot) *
                        Matrix.CreateRotationZ(0.78f) *
                        earth.Transform;
                }
                else
                    celestialBody.Transform *= Matrix.CreateRotationY(MathHelper.WrapAngle(celestialBody.RotationSpeedAroundPivot * (float)gameTime.ElapsedGameTime.TotalSeconds));

            }


            // Step 7: Make the moon rotate around the earth, speed 1.5
            // Step 8: Change the orbit of the moon such that it is rotated 45 degrees toward the sun/origin(see example!)

            base.Update(gameTime);
        }
    }
}