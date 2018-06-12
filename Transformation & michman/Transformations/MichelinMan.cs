using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Opdracht6_Transformations
{
    class MichelinMan
    {
        public IReadOnlyDictionary<Bones, Bone> Skeleton { get { return _skeleton as IReadOnlyDictionary<Bones, Bone>; } }
        private Dictionary<Bones, Bone> _skeleton;

        public Bone Origin { get { return b_body1; } }
        public Bone LeftLeg { get { return b_legLeftUpper; } }
        public Bone RightLeg { get { return b_legRightUpper; } }
        public Bone LeftArm { get { return b_armLeftUpper; } }
        public Bone RightArm { get { return b_armRightUpper; } }
        public Bone Head { get { return b_headLower; } }


        public MichelinMan()
        {
            #region Het lichaam
            body1 = new Sphere(Matrix.Identity, Color.White, 30);
            body2 = new Sphere(Matrix.Identity, Color.White, 30);
            body3 = new Sphere(Matrix.Identity, Color.White, 30);
            body4 = new Sphere(Matrix.Identity, Color.White, 30);

            //bone hierarchy
            b_body1 = new Bone(body1, Matrix.CreateTranslation(0f, 0, 0), null, Matrix.CreateScale(2.8f, 2f, 2.6f));
            b_body1.AddNewBone(b_body2 = new Bone(body2, Matrix.CreateTranslation(0f, 0.8f, 0f), null, Matrix.CreateScale(3.2f, 1.5f, 3.2f)));
            b_body2.AddNewBone(b_body3 = new Bone(body3, Matrix.CreateTranslation(0, 1, 0), null, Matrix.CreateScale(3.2f, 1.5f, 3.2f)));
            b_body3.AddNewBone(b_body4 = new Bone(body4, Matrix.CreateTranslation(0, 0.8f, 0), null, Matrix.CreateScale(2.8f, 2f, 2.6f)));
            #endregion

            #region Het hoofd
            neck = new Sphere(Matrix.Identity, Color.White, 30);
            headLower = new Sphere(Matrix.Identity, Color.White, 30);
            headUpper = new Sphere(Matrix.Identity, Color.White, 30);

            leftEye = new Sphere(Matrix.Identity, Color.Black, 30);
            rightEye = new Sphere(Matrix.Identity, Color.Black, 30);

            // Head bone hierarchy
            b_body4.AddNewBone(b_neck = new Bone(neck, Matrix.CreateTranslation(0, 2, 0), null, Matrix.CreateScale(0.8f, 1, 0.8f)));
            b_neck.AddNewBone(b_headLower = new Bone(headLower, Matrix.CreateTranslation(0, 1.5f, 0), null, Matrix.CreateScale(1.8f, 1.1f, 1.8f)));
            b_headLower.AddNewBone(b_headUpper = new Bone(headUpper, Matrix.CreateTranslation(0, 1, 0), null, Matrix.CreateScale(1.4f, 1.3f, 1.4f)));

            //eye hierarchy
            b_headUpper.AddNewBone(b_leftEye = new Bone(leftEye));
            b_headUpper.AddNewBone(b_rightEye = new Bone(rightEye));
            #endregion

            #region Linker Arm

            #endregion

            #region Rechter Arm

            #endregion

            #region Linker Been

            #endregion

            #region Rechter Been

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
                { Bones.LeftArmUpper,b_armLeftUpper },
                { Bones.LeftElbow, b_armLeftElbow},
                { Bones.LeftArmLower, b_armLeftLower},
                { Bones.LeftHand, b_leftHand},
                { Bones.RightArmUpper, b_armRightUpper},
                { Bones.RightElbow, b_armRightElbow},
                { Bones.RightElbowLower, b_armRightLower},
                { Bones.RightHand, b_rightHand},
                { Bones.LeftLegUpper, b_legLeftLower},
                { Bones.LeftKnee, b_legLeftKnee},
                { Bones.LeftLegLower, b_legLeftUpper},
                { Bones.LeftFoot, b_leftFoot},
                { Bones.RightLegUpper, b_legRightUpper},
                { Bones.RightKnee, b_legRightKnee},
                { Bones.RightLegLower, b_legRightLower},
                { Bones.RightFoot, b_rightFoot}
            };
        }

        #region De Michelin Man

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

        #region Benen
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

        public void Update()
        {

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
            LeftHand,
            RightArm,
            RightArmUpper,
            RightElbow,
            RightElbowLower,
            RightHand,
            LeftLeg,
            LeftLegUpper,
            LeftKnee,
            LeftLegLower,
            LeftFoot,
            RightLeg,
            RightLegUpper,
            RightKnee,
            RightLegLower,
            RightFoot
        }
    }
}
