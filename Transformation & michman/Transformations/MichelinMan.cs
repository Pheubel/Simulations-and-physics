using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace Opdracht6_Transformations
{
    class MichelinMan
    {
        /// <summary> A collection of bones in the skeleton.</summary>
        public IReadOnlyDictionary<Bones, Bone> Skeleton { get { return _skeleton as IReadOnlyDictionary<Bones, Bone>; } }
        private Dictionary<Bones, Bone> _skeleton;

        public Bone Origin { get { return b_origin; } }
        public Bone LowerBody { get { return b_body1; } }
        public Bone Head { get { return b_headLower; } }
        public Bone LeftLeg { get { return b_leftLeg; } }
        public Bone LeftKnee { get { return b_legLeftKnee; } }
        public Bone LeftAnkle { get { return b_leftFootJoint; } }
        public Bone RightLeg { get { return b_rightLeg; } }
        public Bone RightKnee { get { return b_legRightKnee; } }
        public Bone RightAnkle { get { return b_rightFootJoint; } }
        public Bone LeftArm { get { return b_leftArm; } }
        public Bone LeftElbow { get { return b_armLeftElbow; } }
        public Bone LeftHand { get { return b_leftHandJoint; } }
        public Bone RightArm { get { return b_rightArm; } }
        public Bone RightElbow { get { return b_armRightElbow; } }
        public Bone RightHandJoint { get { return b_rightHandJoint; } }
        
        

        /// <summary> Initializes the Michelin man standing in a T-pose.</summary>
        public MichelinMan()
        {
            origin = new BoneJoint();
            b_origin = new Bone(origin);


            #region Het lichaam
            body1 = new Sphere(Matrix.Identity, Color.White, 30);
            body2 = new Sphere(Matrix.Identity, Color.White, 30);
            body3 = new Sphere(Matrix.Identity, Color.White, 30);
            body4 = new Sphere(Matrix.Identity, Color.White, 30);

            //bone hierarchy
            b_body1 = b_origin.AddNewBone(body1, Matrix.CreateTranslation(0f, 0, 0), null, Matrix.CreateScale(2.8f, 2f, 2f));
            b_body2 = b_body1.AddNewBone(body2, Matrix.CreateTranslation(0f, 0.8f, 0f), null, Matrix.CreateScale(3.2f, 1.5f, 2.8f));
            b_body3 = b_body2.AddNewBone(body3, Matrix.CreateTranslation(0, 1, 0), null, Matrix.CreateScale(3.2f, 1.5f, 2.8f));
            b_body4 = b_body3.AddNewBone(body4, Matrix.CreateTranslation(0, 0.8f, 0), null, Matrix.CreateScale(2.8f, 2f, 2.2f));
            #endregion

            #region Het hoofd
            neck = new Sphere(Matrix.Identity, Color.White, 30);
            headLower = new Sphere(Matrix.Identity, Color.White, 30);
            headUpper = new Sphere(Matrix.Identity, Color.White, 30);

            leftEye = new Sphere(Matrix.Identity, Color.Black, 30);
            rightEye = new Sphere(Matrix.Identity, Color.Black, 30);

            // Head bone hierarchy
            b_neck = b_body4.AddNewBone(neck, Matrix.CreateTranslation(0, 2, 0), null, Matrix.CreateScale(0.8f, 1, 0.8f));
            b_headLower = b_neck.AddNewBone(headLower, Matrix.CreateTranslation(0, 1.5f, 0), null, Matrix.CreateScale(1.8f, 1.1f, 1.8f));
            b_headUpper = b_headLower.AddNewBone(headUpper, Matrix.CreateTranslation(0, 1, 0), null, Matrix.CreateScale(1.4f, 1.3f, 1.4f));
            b_leftEye = b_headUpper.AddNewBone(rightEye, Matrix.CreateTranslation(0.8f, 0, 1.7f),null,Matrix.CreateScale(0.5f,0.5f,0.5f));
            b_rightEye = b_headUpper.AddNewBone(leftEye, Matrix.CreateTranslation(-0.8f, 0, 1.7f),null, Matrix.CreateScale(0.5f, 0.5f, 0.5f));
            #endregion

            #region Linker Arm
            leftArm = new BoneJoint();
            leftHandJoint = new BoneJoint();

            armLeftUpper = new Sphere(Matrix.Identity, Color.White, 30);
            armLeftElbow = new Sphere(Matrix.Identity, Color.White, 30);
            armLeftLower = new Sphere(Matrix.Identity, Color.White, 30);
            leftHand = new Sphere(Matrix.Identity, Color.White, 30);

            b_leftArm = b_body4.AddNewBone(leftArm, Matrix.CreateTranslation(2.5f, 0.5f, 0));

            b_armLeftUpper = b_leftArm.AddNewBone(armLeftUpper, Matrix.CreateTranslation(1.2f, 0, 0), null, Matrix.CreateScale(2.5f, 1, 1));
            b_armLeftElbow = b_armLeftUpper.AddNewBone(armLeftElbow, Matrix.CreateTranslation(2, 0, 0), null, Matrix.CreateScale(1, 1, 1));
            b_armLeftLower = b_armLeftElbow.AddNewBone(armLeftLower, Matrix.CreateTranslation(2, 0, 0), null, Matrix.CreateScale(2.5f, 1, 1));
            b_leftHandJoint = b_armLeftLower.AddNewBone(leftHandJoint, Matrix.CreateTranslation(2.2f, 0, 0));
            b_leftHand = b_leftHandJoint.AddNewBone(leftHand, Matrix.CreateTranslation(0.6f, 0, 0), null, Matrix.CreateScale(1, 0.8f, 0.6f));

            #endregion

            #region Rechter Arm
            rightArm = new BoneJoint();
            rightHandJoint = new BoneJoint();

            armRightUpper = new Sphere(Matrix.Identity, Color.White, 30);
            armRightElbow = new Sphere(Matrix.Identity, Color.White, 30);
            armRightLower = new Sphere(Matrix.Identity, Color.White, 30);
            rightHand = new Sphere(Matrix.Identity, Color.White, 30);

            b_rightArm = b_body4.AddNewBone(rightArm, Matrix.CreateTranslation(-2.5f, 0.5f, 0));

            b_armRightUpper = b_rightArm.AddNewBone(armRightUpper, Matrix.CreateTranslation(-2f, 0, 0), null, Matrix.CreateScale(2.5f, 1, 1));
            b_armRightElbow = b_armRightUpper.AddNewBone(armRightElbow, Matrix.CreateTranslation(-2, 0, 0), null, Matrix.CreateScale(1, 1, 1));
            b_armRightLower = b_armRightElbow.AddNewBone(armRightLower, Matrix.CreateTranslation(-2, 0, 0), null, Matrix.CreateScale(2.5f, 1, 1));
            b_rightHandJoint = b_armRightLower.AddNewBone(rightHandJoint, Matrix.CreateTranslation(-2.2f, 0, 0));
            b_rightHand = b_rightHandJoint.AddNewBone(rightHand, Matrix.CreateTranslation(-0.6f, 0, 0), null, Matrix.CreateScale(1, 0.8f, 0.6f));

            #endregion

            #region Linker Been
            leftLeg = new BoneJoint();
            leftFootJoint = new BoneJoint();

            legLeftUpper = new Sphere(Matrix.Identity, Color.White, 30);
            legLeftKnee = new Sphere(Matrix.Identity, Color.White, 30);
            legLeftLower = new Sphere(Matrix.Identity, Color.White, 30);
            leftFoot = new Sphere(Matrix.Identity, Color.White, 30);

            b_leftLeg = b_origin.AddNewBone(leftLeg, Matrix.CreateTranslation(1.8f, -1.4f, 0));

            b_legLeftUpper = b_leftLeg.AddNewBone(legLeftUpper,Matrix.CreateTranslation(0,-2,0),null,Matrix.CreateScale(1.2f,2.5f,1.4f));
            b_legLeftKnee = b_legLeftUpper.AddNewBone(legLeftKnee, Matrix.CreateTranslation(0,-2,0),null, Matrix.CreateScale(1.2f,1.2f,1.2f));
            b_legLeftLower = b_legLeftKnee.AddNewBone(legLeftLower, Matrix.CreateTranslation(0, -2, 0), null, Matrix.CreateScale(1.2f, 2.5f, 1.4f));
            b_leftFootJoint = b_legLeftLower.AddNewBone(leftFootJoint, Matrix.CreateTranslation(0, -2, 0));
            b_leftFoot = b_leftFootJoint.AddNewBone(leftFoot, Matrix.CreateTranslation(0, 0, 1), null,Matrix.CreateScale(1,0.7f,2));
            #endregion

            #region Rechter Been
            rightLeg = new BoneJoint();
            rightFootJoint = new BoneJoint();

            legRightUpper = new Sphere(Matrix.Identity, Color.White, 30);
            legRightKnee = new Sphere(Matrix.Identity, Color.White, 30);
            legRightLower = new Sphere(Matrix.Identity, Color.White, 30);
            rightFoot = new Sphere(Matrix.Identity, Color.White, 30);


            b_rightLeg = b_origin.AddNewBone(rightLeg, Matrix.CreateTranslation(-1.8f, -1.4f, 0));

            b_legRightUpper = b_rightLeg.AddNewBone(legRightUpper, Matrix.CreateTranslation(0, -2, 0), null, Matrix.CreateScale(1.2f, 2.5f, 1.4f));
            b_legRightKnee = b_legRightUpper.AddNewBone(legRightKnee, Matrix.CreateTranslation(0, -2, 0), null, Matrix.CreateScale(1.2f, 1.2f, 1.2f));
            b_legRightLower = b_legRightKnee.AddNewBone(legRightLower, Matrix.CreateTranslation(0, -2, 0), null, Matrix.CreateScale(1.2f, 2.5f, 1.4f));
            b_rightFootJoint = b_legRightLower.AddNewBone(rightFootJoint, Matrix.CreateTranslation(0, -2, 0));
            b_rightFoot = b_rightFootJoint.AddNewBone(rightFoot, Matrix.CreateTranslation(0, 0, 1), null, Matrix.CreateScale(1, 0.7f, 2));
            #endregion



            _skeleton = new Dictionary<Bones, Bone>()
            {
                { Bones.Body1, b_body1},
                { Bones.Body2, b_body2},
                { Bones.Body3, b_body3},
                { Bones.Body4, b_body4},
                { Bones.Neck, b_neck},
                { Bones.HeadLower, b_headLower},
                { Bones.HeadUpper, b_headUpper},
                { Bones.LeftEye, b_leftEye},
                { Bones.RightEye, b_rightEye},
                { Bones.LeftArm, b_leftArm},
                { Bones.LeftArmUpper,b_armLeftUpper },
                { Bones.LeftElbow, b_armLeftElbow},
                { Bones.LeftArmLower, b_armLeftLower},
                { Bones.LeftHandJoint, b_leftHandJoint },
                { Bones.LeftHand, b_leftHand},
                { Bones.RightArm,b_rightArm },
                { Bones.RightArmUpper, b_armRightUpper},
                { Bones.RightElbow, b_armRightElbow},
                { Bones.RightElbowLower, b_armRightLower},
                { Bones.RightHandJoint, b_rightHandJoint },
                { Bones.RightHand, b_rightHand},
                { Bones.LeftLeg, b_leftLeg },
                { Bones.LeftLegUpper, b_legLeftLower},
                { Bones.LeftKnee, b_legLeftKnee},
                { Bones.LeftLegLower, b_legLeftUpper},
                { Bones.LeftFootJoint, b_leftFootJoint },
                { Bones.LeftFoot, b_leftFoot},
                { Bones.RightLeg, b_rightLeg },
                { Bones.RightLegUpper, b_legRightUpper},
                { Bones.RightKnee, b_legRightKnee},
                { Bones.RightLegLower, b_legRightLower},
                { Bones.RightFootJoint, b_rightFootJoint },
                { Bones.RightFoot, b_rightFoot}
            };

            Blink();
        }

        // defining michelin man
        #region De Michelin Man

        BoneJoint origin;
        Bone b_origin;

        #region Hoofd
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
        private BoneJoint leftArm, rightArm;
        private BoneJoint leftHandJoint, rightHandJoint;

        private Sphere armLeftUpper, armRightUpper;
        private Sphere armLeftElbow, armRightElbow;
        private Sphere armLeftLower, armRightLower;
        private Sphere leftHand, rightHand;

        //bones
        private Bone b_leftArm, b_rightArm;
        private Bone b_leftHandJoint, b_rightHandJoint;

        private Bone b_armLeftUpper, b_armRightUpper;
        private Bone b_armLeftElbow, b_armRightElbow;
        private Bone b_armLeftLower, b_armRightLower;
        private Bone b_leftHand, b_rightHand;
        #endregion

        #region Benen
        private BoneJoint leftLeg;
        private BoneJoint leftFootJoint, rightFootJoint;

        private Sphere legLeftUpper;
        private Sphere legLeftKnee;
        private Sphere legLeftLower;
        private Sphere leftFoot;


        private BoneJoint rightLeg;

        private Sphere legRightUpper;
        private Sphere legRightKnee;
        private Sphere legRightLower;
        private Sphere rightFoot;

        //bones
        private Bone b_leftFootJoint, b_rightFootJoint;

        private Bone b_leftLeg;

        private Bone b_legLeftUpper;
        private Bone b_legLeftKnee;
        private Bone b_legLeftLower;
        private Bone b_leftFoot;


        private Bone b_rightLeg;

        private Bone b_legRightUpper;
        private Bone b_legRightKnee;
        private Bone b_legRightLower;
        private Bone b_rightFoot;
        #endregion
        #endregion

        public void Draw()
        {
            foreach (Bone bone in _skeleton.Values)
            {
                //protects the draw from stopping due to a undefined bone
                if (bone == null) continue;

                // casts the bone as an interface implementation that can draw the current object
                var drawableObject = bone.TransformableObject as IDrawableTransformable;
                if (drawableObject == null) continue;

                drawableObject.Draw();
            }
        }

        /// <summary> Makes the michelin man blink every 2 to 5 seconds.</summary>
        private async Task Blink()
        {
            // delays by 2 to 5 seconds
            await Task.Delay(RandomHelper.Randomizer.Next(2000,5000));
            
            DoBlink(DateTime.Now,TimeSpan.FromSeconds(0.5));

            Blink();
        }

        /// <summary> The refreshrate of the blink steps in milli seconds.</summary>
        const int BLINK_REFRES_RATE = 1;

        /// <summary> Executes the animation for the blink</summary>
        /// <param name="startTime"> The time the animation was started at.</param>
        /// <param name="duration"> The length of the animation.</param>
        private async Task DoBlink(DateTime startTime,TimeSpan duration)
        {
            DateTime endTime = startTime + duration;

            if (DateTime.Now < endTime)
            {
                TimeSpan ellapsedTime = DateTime.Now - startTime;

                float progress = (float)(ellapsedTime.TotalSeconds / duration.TotalSeconds);



                float eyeScale = 0.5f * (float)(Math.Sin(progress * Math.PI + Math.PI) + 1);
                b_rightEye.SetLocalScaling(Matrix.CreateScale(0.5f, eyeScale, 0.5f));
                b_leftEye.SetLocalScaling(Matrix.CreateScale(0.5f, eyeScale, 0.5f));

                await Task.Delay(BLINK_REFRES_RATE);
                DoBlink(startTime, duration);
            }
            else
            {
                //restore values to their original scale
                b_rightEye.SetLocalScaling(Matrix.CreateScale(0.5f, 0.5f, 0.5f));
                b_leftEye.SetLocalScaling(Matrix.CreateScale(0.5f, 0.5f, 0.5f));
            }
        }
        
        public enum Bones
        {
            Body1,
            Body2,
            Body3,
            Body4,
            Neck,
            HeadLower,
            HeadUpper,
            LeftEye,
            RightEye,
            LeftArm,
            LeftArmUpper,
            LeftElbow,
            LeftArmLower,
            LeftHandJoint,
            LeftHand,
            RightArm,
            RightArmUpper,
            RightElbow,
            RightElbowLower,
            RightHandJoint,
            RightHand,
            LeftLeg,
            LeftLegUpper,
            LeftKnee,
            LeftLegLower,
            LeftFootJoint,
            LeftFoot,
            RightLeg,
            RightLegUpper,
            RightKnee,
            RightLegLower,
            RightFootJoint,
            RightFoot
        }
    }
}

