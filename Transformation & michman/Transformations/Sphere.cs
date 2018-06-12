﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opdracht6_Transformations
{
    public interface ITransformable
    {
        Matrix Transform { get; }

        Matrix ApplyTransform(Matrix transformation);
        Matrix SetTransform(Matrix transformation);

        void Draw();
    }

    public class Sphere : ITransformable
    {
        public struct VertexPositionColorNormal : IVertexType
        {
            public Vector3 Position;
            public Color Color;
            public Vector3 Normal;

            VertexDeclaration IVertexType.VertexDeclaration
            {
                get { return new VertexDeclaration
            (
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(sizeof(float) * 3 + 4, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            ); }
            }
        }

        VertexPositionColorNormal[] vertices;
        VertexBuffer vbuffer;
        short[] indices;
        IndexBuffer ibuffer;
        int nvertices, nindices;
        BasicEffect effect;
        GraphicsDevice graphics;
        int numVertices;
        Color color;

        public Matrix Transform { get { return _transform; } private set { _transform = value; } }
        private Matrix _transform;

        public float RotationSpeedAroundPivot { get; private set; }

        public Matrix ApplyTransform(Matrix transformation)
        {
            return _transform *= transformation;
        }

        public Matrix SetTransform(Matrix transformation)
        {
            return _transform = transformation;
        }

        public Sphere(Matrix transform,Color color, int numVertices, float rotationSpeedAroundPivot = 0)
        {
            RotationSpeedAroundPivot = rotationSpeedAroundPivot;
            this.Transform = transform;
            this.color = color;
            this.numVertices = numVertices;
            graphics = SimPhyGameWorld.World.GraphicsDevice;
            effect = new BasicEffect(SimPhyGameWorld.Graphics);
            effect.EnableDefaultLighting();
            nvertices = numVertices * numVertices;
            nindices = numVertices * numVertices * 6;
            vbuffer = new VertexBuffer(graphics, typeof(VertexPositionColorNormal), nvertices, BufferUsage.WriteOnly);
            ibuffer = new IndexBuffer(graphics, IndexElementSize.SixteenBits, nindices, BufferUsage.WriteOnly);
            createVertices();
            createIndices();
            vbuffer.SetData<VertexPositionColorNormal>(vertices);
            ibuffer.SetData<short>(indices);
            effect.VertexColorEnabled = true;
        }
        void createVertices() // Note: This can be done a lot more efficiently if multiple spheres have the same number of vertices
        {
            vertices = new VertexPositionColorNormal[nvertices];
            Vector3 center = new Vector3(0, 0, 0);
            Vector3 rad = new Vector3(1, 0, 0);
            for (int x = 0; x < numVertices; x++)
            {
                float difx = 360.0f / (float)numVertices;
                for (int y = 0; y < numVertices; y++)
                {
                    float dify = 360.0f / (float)numVertices;
                    Matrix zrot = Matrix.CreateRotationZ(MathHelper.ToRadians(y * dify));
                    Matrix yrot = Matrix.CreateRotationY(MathHelper.ToRadians(x * difx));
                    Vector3 point = Vector3.Transform(Vector3.Transform(rad, zrot), yrot);
                    Vector3 normal = point;
                    point.Normalize();

                    vertices[x + y * numVertices] = new VertexPositionColorNormal() { Position = point, Color = color, Normal = normal };
                }
            }
        }

        void createIndices()
        {
            indices = new short[nindices];
            int i = 0;
            for (int x = 0; x < numVertices; x++)
            {
                for (int y = 0; y < numVertices; y++)
                {
                    int s1 = x == numVertices-1 ? 0 : x + 1;
                    int s2 = y == numVertices-1 ? 0 : y + 1;
                    short upperLeft = (short)(x * numVertices + y);
                    short upperRight = (short)(s1 * numVertices + y);
                    short lowerLeft = (short)(x * numVertices + s2);
                    short lowerRight = (short)(s1 * numVertices + s2);
                    indices[i++] = upperLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerLeft;
                    indices[i++] = lowerLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerRight;
                }
            }
        }
        public void Draw()
        {
            effect.View = SimPhyGameWorld.World.View;
            effect.Projection = SimPhyGameWorld.World.Projection;
            effect.World = this.Transform;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserIndexedPrimitives<VertexPositionColorNormal>(PrimitiveType.TriangleList, vertices, 0, nvertices, indices, 0, indices.Length / 3);
            }
        }

        
    }
}
