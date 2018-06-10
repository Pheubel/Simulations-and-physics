using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht6_Transformations
{
    class Bone
    {
        // bone transformations
        public Matrix LocalTransform { get { return LocalOrientation * AnimationTransform; } }
        public Matrix WorldTransform { get { return (ParentBone?.WorldTransform ?? Matrix.Identity) * LocalTransform; } }
        public Matrix AnimationTransform { get; set; } = Matrix.Identity;

        public Matrix LocalOrientation
        {
            get
            {
                return LocalTranslation * LocalRotation * LocalScaling;
            }
        }

        public Quaternion Rotation { get; private set; }

        public Matrix WorldTranslation { get { return (ParentBone?.WorldTranslation ?? Matrix.Identity) * LocalTranslation; } }

        public Matrix LocalScaling { get; private set; }
        public Matrix LocalRotation { get; private set; }
        public Matrix LocalTranslation { get; private set; }

        // bone family tree
        public Bone ParentBone { get; private set; }
        public IReadOnlyList<Bone> ChildBones { get { return _childBones.AsReadOnly(); } }
        private List<Bone> _childBones = new List<Bone>();

        //
        private ITransformable _transformableObject;

        #region Initializations

        public Bone(ITransformable transformable, Matrix? localTranslation = null, Matrix? localRotation = null, Matrix? localScaling = null)
        {
            _transformableObject = transformable;
            LocalTranslation = localTranslation ?? Matrix.Identity;
            LocalRotation = localRotation ?? Matrix.Identity;
            LocalScaling = localScaling ?? Matrix.Identity;

            Rotation = LocalRotation.;

            _transformableObject.SetTransform(WorldTransform);
        }

        #endregion

        public void SetLocalTranslation(Matrix translation)
        {
            LocalTranslation = translation;
            BoneUpdated();
        }

        public void ApplyLocalTranslation(Matrix translation)
        {
            LocalTranslation *= translation;
            BoneUpdated();
        }


        private void BoneUpdated()
        {
            _transformableObject.SetTransform(WorldTransform);
            for (int i = 0; i < _childBones.Count; i++)
            {
                _childBones[i].BoneUpdated();
            }
        }

        /// <summary> Checks if the specific bone is a child bone.</summary>
        /// <returns> true if child, false if not.</returns>
        public bool IsChild()
        {
            return ParentBone != null;
        }

        /// <summary> Checks if the specific bone is a parent bone.</summary>
        /// <returns> true if parent, false if not.</returns>
        public bool IsParent()
        {
            return ChildBones.Count != 0;
        }

        #region Skeleton structure mutation

        public void AddNewBone(Bone newBone)
        {
            newBone.ParentBone = this;
            newBone.BoneUpdated();
            _childBones.Add(newBone);
        }

        public void AddNewBones(ICollection<Bone> newBones)
        {
            _childBones.AddRange(newBones);
            foreach (Bone newBone in newBones)
            {
                newBone.ParentBone = this;
                newBone.BoneUpdated();
            }
        }

        public void AttachBone(Bone bone)
        {
            this._childBones.Add(bone);
            bone.ParentBone = this;
        }

        public void AttachBones(ICollection<Bone> bones)
        {
            this._childBones.AddRange(bones);
            foreach (Bone bone in bones)
                bone.ParentBone = this;
        }

        public void DetachBone(Bone bone)
        {
            bone.ParentBone = null;
            this._childBones.Remove(bone);
        }

        public void DetachBones(ICollection<Bone> bones)
        {
            _childBones.RemoveAll(b => bones.Contains(b));
            foreach (Bone bone in bones)
            {
                bone.ParentBone = null;
            }
        }

        public void DetachAllBones()
        {
            foreach (Bone bone in _childBones)
                bone.ParentBone = null;
            _childBones.Clear();
        }

        public void RemoveFromSkeleton(RemoveAction action)
        {
            switch (action)
            {
                case RemoveAction.AttachChildrenToParent:
                    foreach (Bone childBone in _childBones)
                        childBone.ParentBone = this.ParentBone;
                    this.ParentBone._childBones.Remove(this);
                    this.ParentBone._childBones.AddRange(_childBones);
                    break;

                case RemoveAction.DetachChildren:
                    this.ParentBone._childBones.Remove(this);
                    foreach (Bone childBone in _childBones)
                        childBone.ParentBone = null;
                    break;

                case RemoveAction.DissolveAllChildrenFromSkeleton:
                    ParentBone._childBones.Remove(this);
                    this.ParentBone = null;
                    CleanChildren();
                    break;

                default: throw new Exception("Unhandled dispose action");
            }
        }

        private void CleanChildren()
        {
            foreach (Bone bone in _childBones)
            {
                bone.CleanChildren();
                bone.ParentBone = null;
            }
            _childBones.Clear();
        }

        public enum RemoveAction : byte
        {
            DetachChildren,
            AttachChildrenToParent,
            DissolveAllChildrenFromSkeleton
        }

        #endregion

        #region old bone
        /*
        public Matrix LocalTransform { get; private set; }
        public Matrix WorldTransform { get; private set; }
        public Vector3 PrevJoint { get; private set; }
        public Vector3 NextJoint { get { return PrevJoint + Direction * Length; } }
        public Vector3 Center { get { return PrevJoint + Direction * Length / 2; } }
        public Vector3 Direction { get; private set; } 
        public float Length { get; private set; }
        public float LengthSqr { get { return Length * Length; } }

        public List<Bone> ChildBones { get; private set; } = new List<Bone>();
        public Bone ParentBone { get; private set; }


        public Bone(Vector3 prevJoint, Vector3 direction, float length,Quaternion rotation)
        {
            PrevJoint = prevJoint;
        }

        public void Transform(Matrix transform)
        {
            LocalTransform *= transform;
            BoneUpdated();
        }

        /// <summary> Checks if the specific bone is a child bone.</summary>
        /// <returns> true if child, false if not.</returns>
        public bool IsChild()
        {
            return ParentBone != null;
        }

        /// <summary> Checks if the specific bone is a parent bone.</summary>
        /// <returns> true if parent, false if not.</returns>
        public bool IsParent()
        {
            return ChildBones.Count != 0;
        }

        /// <summary> Updates the child bone with it's parent transformation.</summary>
        private void BoneUpdated()
        {
            WorldTransform *= ParentBone?.WorldTransform ?? Matrix.Identity * LocalTransform;
            for (int i = 0; i < ChildBones.Count; i++)
            {
                ChildBones[i].BoneUpdated();
            }
        }
        */
        #endregion
    }
}
