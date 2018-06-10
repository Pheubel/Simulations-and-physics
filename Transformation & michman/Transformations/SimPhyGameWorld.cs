//#define usingOldMich

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

#if usingOldMich
            #region  De Michelin Man

            // Nek en het hoofd
            spheres.Add(neck = new Sphere(Matrix.CreateScale(1.4f, 1.2f, 1.3f) * Matrix.CreateTranslation(0f, 24f, 0f), Color.White, 30));
            spheres.Add(headLower = new Sphere(Matrix.CreateScale(2f, 1.2f, 1.7f) * Matrix.CreateTranslation(0f, 25f, 0f), Color.White, 30));
            spheres.Add(headUpper = new Sphere(Matrix.CreateScale(1.4f, 1.4f, 1.4f) * Matrix.CreateTranslation(0f, 25.8f, 0f), Color.White, 30));

            // Ogen
            spheres.Add(leftEye = new Sphere(Matrix.CreateScale(0.3f, 0.3f, 0.5f) * Matrix.CreateTranslation(-0.5f, 26f, 1.3f), Color.Black, 30));
            spheres.Add(rightEye = new Sphere(Matrix.CreateScale(0.3f, 0.3f, 0.5f) * Matrix.CreateTranslation(0.5f, 26f, 1.3f), Color.Black, 30));

            // Het lichaam
            spheres.Add(body1 = new Sphere(Matrix.CreateScale(2.9f, 1.3f, 2.5f) * Matrix.CreateTranslation(0f, 18.7f, 0f), Color.White, 30));
            spheres.Add(body2 = new Sphere(Matrix.CreateScale(3.1f, 1.5f, 2.7f) * Matrix.CreateTranslation(0f, 20f, 0f), Color.White, 30));
            spheres.Add(body3 = new Sphere(Matrix.CreateScale(3.0f, 1.5f, 2.6f) * Matrix.CreateTranslation(0f, 21.5f, 0f), Color.White, 30));
            spheres.Add(body4 = new Sphere(Matrix.CreateScale(2.7f, 1.3f, 2.4f) * Matrix.CreateTranslation(0f, 22.8f, 0f), Color.White, 30));

            // De armen

            // Rechter bovenarm
            rotUpperRightArm = Matrix.CreateRotationZ(0.8f);
            spheres.Add(armRightUpper = new Sphere(Matrix.CreateScale(1.6f, 1f, 1f) * rotUpperRightArm * Matrix.CreateTranslation(-3.3f, 22f, 0f)
                , Color.White, 30));
            endUpperRightArm = Vector3.Transform(new Vector3(-1f, 0f, 0f), armRightUpper.Transform);

            // Linker bovenarm
            rotUpperLeftArm = Matrix.CreateRotationZ(-0.3f);
            spheres.Add(armLeftUpper = new Sphere(Matrix.CreateScale(1.6f, 1f, 1f) * rotUpperLeftArm * Matrix.CreateTranslation(3.3f, 22.8f, 0f)
                , Color.White, 30));
            endUpperLeftArm = Vector3.Transform(new Vector3(1f, 0f, 0f), armLeftUpper.Transform);

            // Rechter elleboog
            spheres.Add(armRightElbow = new Sphere(Matrix.CreateScale(1f, 0.9f, 0.9f) * rotUpperRightArm * Matrix.CreateTranslation(endUpperRightArm),
                Color.White, 30));

            // Linker elleboog
            spheres.Add(armLeftElbow = new Sphere(Matrix.CreateScale(1f, 0.9f, 0.9f) * rotUpperLeftArm * Matrix.CreateTranslation(endUpperLeftArm),
                Color.White, 30));

            // Rechter onderarm
            Matrix rotLowerRightArm = rotUpperLeftArm * Matrix.CreateRotationZ(-1.6f);
            spheres.Add(armRightLower = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerRightArm * Matrix.CreateTranslation(endUpperRightArm),
                Color.White, 30));
            Vector3 endLowerRightArm = Vector3.Transform(new Vector3(1f, 0f, 0f), armRightLower.Transform);

            // Linker onderarm
            Matrix rotLowerLeftArm = rotUpperLeftArm * Matrix.CreateRotationZ(1.6f);
            spheres.Add(armLeftLower = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerLeftArm * Matrix.CreateTranslation(endUpperLeftArm),
                Color.White, 30));
            Vector3 endLowerLeftArm = Vector3.Transform(new Vector3(1f, 0f, 0f), armLeftLower.Transform);

            // Rechter hand
            Matrix rotRightHand = rotLowerRightArm * Matrix.CreateRotationZ(0.1f);
            spheres.Add(rightHand = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * rotRightHand * Matrix.CreateTranslation(endLowerRightArm),
                Color.White, 30));

            // Linker hand
            Matrix rotLeftHand = rotLowerLeftArm * Matrix.CreateRotationZ(0.1f);
            spheres.Add(leftHand = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * rotLeftHand * Matrix.CreateTranslation(endLowerLeftArm),
                Color.White, 30));

            // De benen

            // Rechter been
            Matrix rotUpperRightLeg = Matrix.CreateRotationY(-0.7f) * Matrix.CreateRotationZ(-1.5f) *
                                     Matrix.CreateRotationZ(-0.2f);
            spheres.Add(legRightUpper = new Sphere(Matrix.CreateScale(1.8f, 1.5f, 1.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotUpperRightLeg * Matrix.CreateTranslation(-1.4f, 17.5f, 0f),
                Color.White, 30));
            Vector3 endUpperRightLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legRightUpper.Transform);

            // Linker been
            Matrix rotUpperLeftLeg = Matrix.CreateRotationY(-0.7f) * Matrix.CreateRotationZ(-1.5f) *
                                     Matrix.CreateRotationZ(-0.2f);
            spheres.Add(legLeftUpper = new Sphere(Matrix.CreateScale(2.0f, 1.7f, 1.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotUpperLeftLeg * Matrix.CreateTranslation(1.4f, 17.5f, 0f),
                Color.White, 30));
            Vector3 endUpperLeftLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legLeftUpper.Transform);

            // Rechter knie
            spheres.Add(legRightKnee = new Sphere(Matrix.CreateScale(1.3f, 1.3f, 1.3f) * rotUpperRightLeg * Matrix.CreateTranslation(endUpperRightLeg),
                Color.White, 30));

            // Linker knie
            spheres.Add(legLeftKnee = new Sphere(Matrix.CreateScale(1.3f, 1.3f, 1.3f) * rotUpperLeftLeg * Matrix.CreateTranslation(endUpperLeftLeg),
                Color.White, 30));

            // Rechter onderbeen
            Matrix rotLowerRightLeg = rotUpperRightLeg * Matrix.CreateRotationX(1.4f);
            spheres.Add(legRightLower = new Sphere(Matrix.CreateScale(2.0f, 1.5f, 1.5f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerRightLeg * Matrix.CreateTranslation(endUpperRightLeg),
                Color.White, 30));
            Vector3 endLowerRightLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legRightLower.Transform);

            // Linker onderbeen
            Matrix rotLowerLeftLeg = rotUpperLeftLeg * Matrix.CreateRotationX(1.4f);
            spheres.Add(legLeftLower = new Sphere(Matrix.CreateScale(2.0f, 1.5f, 1.5f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerLeftLeg * Matrix.CreateTranslation(endUpperLeftLeg),
                Color.White, 30));
            Vector3 endLowerLeftLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legLeftLower.Transform);

            // Rechter voet
            Matrix rotRightFoot = rotLowerRightLeg * Matrix.CreateRotationX(-1.4f);
            spheres.Add(rightFoot = new Sphere(Matrix.CreateScale(1.8f, 1f, 0.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotRightFoot * Matrix.CreateTranslation(endLowerRightLeg),
                Color.White, 30));

            // Linker voet
            Matrix rotLeftFoot = rotLowerLeftLeg * Matrix.CreateRotationX(-1.4f);
            spheres.Add(leftFoot = new Sphere(Matrix.CreateScale(1.8f, 1f, 0.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLeftFoot * Matrix.CreateTranslation(endLowerLeftLeg),
                Color.White, 30));

            #endregion
#else
            #region  De Michelin Man
            #region Het lichaam
            body1 = new Sphere(Matrix.Identity, Color.White, 30);
            body2 = new Sphere(Matrix.Identity, Color.White, 30);
            body3 = new Sphere(Matrix.Identity, Color.White, 30);

            //top dog
            b_body1 = new Bone(body1, Matrix.CreateTranslation(0f, 5f, 0),null, Matrix.CreateScale(1.4f, 1.2f, 1.3f));
            b_body1.AddNewBone(b_body2 = new Bone(body2, Matrix.CreateTranslation(0f, 3f, 0f),null,Matrix.CreateScale(2f, 1.2f, 1.7f)));
            b_body2.AddNewBone(b_body3 = new Bone(body3));
            #endregion

            // Nek en het hoofd
            spheres.Add(neck = new Sphere(Matrix.CreateScale(1.4f, 1.2f, 1.3f) * Matrix.CreateTranslation(0f, 24f, 0f), Color.White, 30));
            spheres.Add(headLower = new Sphere(Matrix.CreateScale(2f, 1.2f, 1.7f) * Matrix.CreateTranslation(0f, 25f, 0f), Color.White, 30));
            spheres.Add(headUpper = new Sphere(Matrix.CreateScale(1.4f, 1.4f, 1.4f) * Matrix.CreateTranslation(0f, 25.8f, 0f), Color.White, 30));

            // Ogen
            spheres.Add(leftEye = new Sphere(Matrix.CreateScale(0.3f, 0.3f, 0.5f) * Matrix.CreateTranslation(-0.5f, 26f, 1.3f), Color.Black, 30));
            spheres.Add(rightEye = new Sphere(Matrix.CreateScale(0.3f, 0.3f, 0.5f) * Matrix.CreateTranslation(0.5f, 26f, 1.3f), Color.Black, 30));

            // Het lichaam
            spheres.Add(body1);
            spheres.Add(body2);
            spheres.Add(body3);
            spheres.Add(body4 = new Sphere(Matrix.CreateScale(2.7f, 1.3f, 2.4f) * Matrix.CreateTranslation(0f, 22.8f, 0f), Color.White, 30));

            // De armen

            // Rechter bovenarm
            rotUpperRightArm = Matrix.CreateRotationZ(0.8f);
            spheres.Add(armRightUpper = new Sphere(Matrix.CreateScale(1.6f, 1f, 1f) * rotUpperRightArm * Matrix.CreateTranslation(-3.3f, 22f, 0f)
                , Color.White, 30));
            endUpperRightArm = Vector3.Transform(new Vector3(-1f, 0f, 0f), armRightUpper.Transform);

            // Linker bovenarm
            rotUpperLeftArm = Matrix.CreateRotationZ(-0.3f);
            spheres.Add(armLeftUpper = new Sphere(Matrix.CreateScale(1.6f, 1f, 1f) * rotUpperLeftArm * Matrix.CreateTranslation(3.3f, 22.8f, 0f)
                , Color.White, 30));
            endUpperLeftArm = Vector3.Transform(new Vector3(1f, 0f, 0f), armLeftUpper.Transform);

            // Rechter elleboog
            spheres.Add(armRightElbow = new Sphere(Matrix.CreateScale(1f, 0.9f, 0.9f) * rotUpperRightArm * Matrix.CreateTranslation(endUpperRightArm),
                Color.White, 30));

            // Linker elleboog
            spheres.Add(armLeftElbow = new Sphere(Matrix.CreateScale(1f, 0.9f, 0.9f) * rotUpperLeftArm * Matrix.CreateTranslation(endUpperLeftArm),
                Color.White, 30));

            // Rechter onderarm
            Matrix rotLowerRightArm = rotUpperLeftArm * Matrix.CreateRotationZ(-1.6f);
            spheres.Add(armRightLower = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerRightArm * Matrix.CreateTranslation(endUpperRightArm),
                Color.White, 30));
            Vector3 endLowerRightArm = Vector3.Transform(new Vector3(1f, 0f, 0f), armRightLower.Transform);

            // Linker onderarm
            Matrix rotLowerLeftArm = rotUpperLeftArm * Matrix.CreateRotationZ(1.6f);
            spheres.Add(armLeftLower = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerLeftArm * Matrix.CreateTranslation(endUpperLeftArm),
                Color.White, 30));
            Vector3 endLowerLeftArm = Vector3.Transform(new Vector3(1f, 0f, 0f), armLeftLower.Transform);

            // Rechter hand
            Matrix rotRightHand = rotLowerRightArm * Matrix.CreateRotationZ(0.1f);
            spheres.Add(rightHand = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * rotRightHand * Matrix.CreateTranslation(endLowerRightArm),
                Color.White, 30));

            // Linker hand
            Matrix rotLeftHand = rotLowerLeftArm * Matrix.CreateRotationZ(0.1f);
            spheres.Add(leftHand = new Sphere(Matrix.CreateScale(1.4f, 0.9f, 0.9f) * rotLeftHand * Matrix.CreateTranslation(endLowerLeftArm),
                Color.White, 30));

            // De benen

            // Rechter been
            Matrix rotUpperRightLeg = Matrix.CreateRotationY(-0.7f) * Matrix.CreateRotationZ(-1.5f) *
                                     Matrix.CreateRotationZ(-0.2f);
            spheres.Add(legRightUpper = new Sphere(Matrix.CreateScale(1.8f, 1.5f, 1.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotUpperRightLeg * Matrix.CreateTranslation(-1.4f, 17.5f, 0f),
                Color.White, 30));
            Vector3 endUpperRightLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legRightUpper.Transform);

            // Linker been
            Matrix rotUpperLeftLeg = Matrix.CreateRotationY(-0.7f) * Matrix.CreateRotationZ(-1.5f) *
                                     Matrix.CreateRotationZ(-0.2f);
            spheres.Add(legLeftUpper = new Sphere(Matrix.CreateScale(2.0f, 1.7f, 1.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotUpperLeftLeg * Matrix.CreateTranslation(1.4f, 17.5f, 0f),
                Color.White, 30));
            Vector3 endUpperLeftLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legLeftUpper.Transform);

            // Rechter knie
            spheres.Add(legRightKnee = new Sphere(Matrix.CreateScale(1.3f, 1.3f, 1.3f) * rotUpperRightLeg * Matrix.CreateTranslation(endUpperRightLeg),
                Color.White, 30));

            // Linker knie
            spheres.Add(legLeftKnee = new Sphere(Matrix.CreateScale(1.3f, 1.3f, 1.3f) * rotUpperLeftLeg * Matrix.CreateTranslation(endUpperLeftLeg),
                Color.White, 30));

            // Rechter onderbeen
            Matrix rotLowerRightLeg = rotUpperRightLeg * Matrix.CreateRotationX(1.4f);
            spheres.Add(legRightLower = new Sphere(Matrix.CreateScale(2.0f, 1.5f, 1.5f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerRightLeg * Matrix.CreateTranslation(endUpperRightLeg),
                Color.White, 30));
            Vector3 endLowerRightLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legRightLower.Transform);

            // Linker onderbeen
            Matrix rotLowerLeftLeg = rotUpperLeftLeg * Matrix.CreateRotationX(1.4f);
            spheres.Add(legLeftLower = new Sphere(Matrix.CreateScale(2.0f, 1.5f, 1.5f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLowerLeftLeg * Matrix.CreateTranslation(endUpperLeftLeg),
                Color.White, 30));
            Vector3 endLowerLeftLeg = Vector3.Transform(new Vector3(1f, 0f, 0f), legLeftLower.Transform);

            // Rechter voet
            Matrix rotRightFoot = rotLowerRightLeg * Matrix.CreateRotationX(-1.4f);
            spheres.Add(rightFoot = new Sphere(Matrix.CreateScale(1.8f, 1f, 0.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotRightFoot * Matrix.CreateTranslation(endLowerRightLeg),
                Color.White, 30));

            // Linker voet
            Matrix rotLeftFoot = rotLowerLeftLeg * Matrix.CreateRotationX(-1.4f);
            spheres.Add(leftFoot = new Sphere(Matrix.CreateScale(1.8f, 1f, 0.7f) * Matrix.CreateTranslation(1f, 0f, 0f) *
                rotLeftFoot * Matrix.CreateTranslation(endLowerLeftLeg),
                Color.White, 30));

            #endregion
#endif

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

        float i = 0;

        protected override void Update(GameTime gameTime)
        {
            b_body1.SetLocalTranslation(Matrix.CreateTranslation(0, i-- * (float)gameTime.ElapsedGameTime.TotalSeconds, 0));

            Debug.WriteLine($"b_body1 local pos: \n{{{b_body1.LocalTranslation.Translation}}}\n\nworld pos:\n{{{b_body1.WorldTransform.Translation}}}\n\n");
            Debug.WriteLine($"b_body2 local pos: \n{{{b_body2.LocalTranslation.Translation}}}\n\nworld pos:\n{{{b_body2.WorldTransform.Translation}}}\n\n\n");

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


            // Step 7: Make the moon rotate around the earth, speed 1.5
            // Step 8: Change the orbit of the moon such that it is rotated 45 degrees toward the sun/origin(see example!)

            base.Update(gameTime);
        }
    }
}
