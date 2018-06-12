using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht6_Transformations
{
    /// <summary> A object which can handle joints between other transformable okbects</summary>
    public class BoneJoint : ITransformable
    {
        /// <summary> The current active transform on this object</summary>
        public Matrix Transform { get { return _transform; } }
        private Matrix _transform = Matrix.Identity;

        /// <summary> This method is not active on the given transformable</summary>
        public Matrix ApplyTransform(Matrix transformation)
        {
            throw new NotImplementedException();
        }

        /// <summary> Sets the transformation of this bone to the given transformation.</summary>
        /// <param name="transformation"> New transformation of this object</param>
        /// <returns> Result of the transformation.</returns>
        public Matrix SetTransform(Matrix transformation)
        {
            return _transform = transformation;
        }
    }

    class Bone
    {
        /// <summary> The space between parents.</summary>
        public Matrix Orientation { get; private set; } = Matrix.Identity;
        /// <summary> The local rotation of the bone.</summary>
        public Matrix LocalRotation { get; private set; } = Matrix.Identity;
        /// <summary> The local transform as result of the orientation and the rotation.</summary>
        public Matrix LocalTransform { get { return LocalRotation * Orientation; } }
        /// <summary> The transform of the bone in world space.</summary>
        public Matrix WorldTransform { get { return LocalTransform * (ParentBone?.WorldTransform ?? Matrix.Identity); } }

        /// <summary> The scaling which the bone will undergo through, does not affect other transformations.</summary>
        public Matrix LocalScaling { get; private set; }

        // bone family tree
        /// <summary> The parent bone of the current bone.</summary>
        public Bone ParentBone { get; private set; }
        /// <summary> The collection of child bones attached to the current bone.</summary>
        public IReadOnlyList<Bone> ChildBones { get { return _childBones.AsReadOnly(); } }
        private List<Bone> _childBones = new List<Bone>();

        /// <summary> The object the transform will be applied to.</summary>
        public ITransformable TransformableObject { get { return _transformableObject; } }
        private ITransformable _transformableObject;

        #region Initializations
        /// <summary> Creates a new bone for the given ITransformable object.</summary>
        /// <param name="transformable"> Object to allow transformation.</param>
        /// <param name="localTranslation"> Starting translation from the parent.</param>
        /// <param name="localRotation"> Starting rotation from the parent.</param>
        /// <param name="localScaling"> SScaling of the object.</param>
        public Bone(ITransformable transformable, Matrix? localTranslation = null, Matrix? localRotation = null, Matrix? localScaling = null)
        {
            _transformableObject = transformable;
            Orientation = localTranslation ?? Matrix.Identity;
            LocalRotation = localRotation ?? Matrix.Identity;
            LocalScaling = localScaling ?? Matrix.Identity;

            _transformableObject.SetTransform(WorldTransform);
        }

        #endregion

        #region Transformations
        /// <summary> Sets the new local transformation of the bone in perspective of the parent.</summary>
        /// <param name="translation"> Translation matrix with the new translation.</param>
        public void SetLocalTranslation(Matrix translation)
        {
            Orientation = translation;
            BoneUpdated();
        }

        /// <summary> Applies the given translation matrix to the bone to change the offset to the parent.</summary>
        /// <param name="translation"> Translation matrix containing the changed translation.</param>
        public void ApplyLocalTranslation(Matrix translation)
        {
            Orientation *= translation;
            BoneUpdated();
        }

        /// <summary> Sets the new local rotation of the bone in perspective of the parent.</summary>
        /// <param name="translation"> Rotation matrix with the new translation.</param>
        public void SetLocalRotation(Matrix rotation)
        {
            LocalRotation = rotation;
            BoneUpdated();
        }

        /// <summary> Adds the local rotation matrix to the bone's current rotation matrix.</summary>
        /// <param name="rotation"> Matrix containing the rotation.</param>
        public void ApplyLocalRotation(Matrix rotation)
        {
            LocalRotation *= rotation;
            BoneUpdated();
        }

        public void SetLocalScaling(Matrix scaling)
        {
            LocalScaling = scaling;
            BoneUpdated();
        }

        public void ApplyLocalScaling(Matrix scaling)
        {
            LocalScaling *= scaling;
            BoneUpdated();
        }
        #endregion

        /// <summary> Applies the updated transformation to the bone and signals to the children to update.</summary>
        private void BoneUpdated()
        {
            _transformableObject.SetTransform(LocalScaling * WorldTransform);
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
        
        /// <summary> Creates a new bone and adds it to the calling bone's children.</summary>
        /// <param name="transformable"> The transformable the bone is going to be attached to.</param>
        /// <param name="localTranslation"> The local translation the bone will have.</param>
        /// <param name="localRotation"> The local rotation the bone will have.</param>
        /// <param name="localScaling"> The local scaling the bone will have.</param>
        /// <returns> New bone.</returns>
        public Bone AddNewBone(ITransformable transformable, Matrix? localTranslation = null, Matrix? localRotation = null, Matrix? localScaling = null)
        {
            Bone newBone = new Bone(transformable,localTranslation,localRotation,localScaling);

            this._childBones.Add(newBone);
            newBone.ParentBone = this;
            return newBone;
        }

        /// <summary> Adds a collection of bones to the calling bone's children.</summary>
        /// <param name="newBones"></param>
        public ICollection<Bone> AddNewBones(ICollection<ITransformable> transformables)
        {
            Bone[] newBones = new Bone[transformables.Count];

            int i = 0;
            foreach (ITransformable transformable in transformables)
            {
                newBones[i] = new Bone(transformable) { ParentBone = this };
                i++;
            }

            _childBones.AddRange(newBones);

            return newBones;
        }

        /// <summary> Adds a bone to the calling bone's children.</summary>
        /// <param name="bone"> new child bone.</param>
        public void AttachBone(Bone bone)
        {
            bone.ParentBone = this;
            bone.BoneUpdated();
            _childBones.Add(bone);
        }

        public void AttachBones(ICollection<Bone> bones)
        {
            _childBones.AddRange(bones);
            foreach (Bone newBone in bones)
            {
                newBone.ParentBone = this;
                newBone.BoneUpdated();
            }
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
    }
}
