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
        /*
         * joint animation transform (A) = (animate within a joint's space)
         * joint orientation transform (O) = (joint space to parent joint space, in the BIND POSE)
         * joint local transform (L=O*A) = (Gregory:) joint local pose transform (joint space to parent joint space, in the CURRENT POSE)
         * joint global transform (G=L*L*L*...) = (Gregory:) joint's current pose (joint space to object space, in the CURRENT POSE)
         * joint final transform (F=G*B-1) = skinning matrices/transforms (object space to object space -- BIND POSE to CURRENT POSE)
         * inverse bind pose (B-1=O-1*O-1*O-1*...) (object space to joint space, in the BIND POSE)
         * joint space = bone space
         */

        /// <summary> space between parents</summary>
        public Matrix Orientation { get; private set; } = Matrix.Identity;
        public Matrix LocalRotation { get; private set; } = Matrix.Identity;
        public Matrix LocalTransform { get { return LocalRotation * Orientation  ; } }
        public Matrix WorldTransform { get { return   LocalTransform * (ParentBone?.WorldTransform ?? Matrix.Identity); } }

        // bone transformations
        /*
        public Matrix WorldTranslation { get { return (ParentBone?.WorldTranslation ?? Matrix.Identity) * LocalTranslation; } }
        public Matrix WorldRotation { get { return (ParentBone?.WorldRotation ?? Matrix.Identity) * LocalRotation; } }

        public Matrix AnimationTransform { get; set; } = Matrix.Identity;*/
        
        public Matrix LocalScaling { get; private set; }
        
        public Matrix LocalTranslation { get; private set; }

        // bone family tree
        public Bone ParentBone { get; private set; }
        public IReadOnlyList<Bone> ChildBones { get { return _childBones.AsReadOnly(); } }
        private List<Bone> _childBones = new List<Bone>();

        public ITransformable TransformableObject { get { return _transformableObject; } }
        private ITransformable _transformableObject;

        #region Initializations

        public Bone(ITransformable transformable, Matrix? localTranslation = null, Matrix? localRotation = null, Matrix? localScaling = null)
        {
            _transformableObject = transformable;
            Orientation = localTranslation ?? Matrix.Identity;
            LocalRotation = localRotation ?? Matrix.Identity;
            LocalScaling = localScaling ?? Matrix.Identity;

            _transformableObject.SetTransform(WorldTransform);
        }

        #endregion

        public void SetLocalTranslation(Matrix translation)
        {
            Orientation = translation;
            BoneUpdated();
        }

        public void ApplyLocalTranslation(Matrix translation)
        {
            Orientation *= translation;
            BoneUpdated();
        }

        public void SetLocalRotation(Matrix rotation)
        {
            LocalRotation = rotation;
            BoneUpdated();
        }

        public void ApplyLocalRotation(Matrix rotation)
        {
            LocalRotation *= rotation;
            BoneUpdated();
        }

        private void BoneUpdated()
        {
            _transformableObject.SetTransform(LocalScaling* WorldTransform );
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
