using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Numerics;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core
{
    internal abstract class Entity
    {
        /// <summary>
        /// Movement speed of entity
        /// </summary>
        protected float speed = 20f;

        /// <summary>
        /// Entity hitbox and position in the world.
        /// </summary>
        internal Rectangle dest;

        /// <summary>
        /// Rectangle used for parsing sprite.
        /// </summary>
        protected Rectangle src;

        /// <summary>
        /// Sprite or spritesheet of your texture.
        /// </summary>
        protected Texture textureSource;

        /// <summary>
        /// Center position
        /// </summary>
        protected Vector2 pos;

        protected Vector2 displacement = Vector2.Zero;

        internal Entity(Texture textureSource, int width, int height, Vector2 initialPosition, float speed)
        {
            this.dest = new Rectangle(0, 0, width, height);
            this.src = new Rectangle(0, 0, width, height);
            this.textureSource = textureSource;
            this.pos = initialPosition;
            this.speed = speed;
        }
        internal Entity(int width, int height, Vector2 initialPosition, float speed)
        {
            this.dest = new Rectangle(0, 0, width, height);
            this.src = new Rectangle(0, 0, width, height);
            this.pos = initialPosition;
            this.speed = speed;
        }

        internal virtual void Update()
        {
            //update position    
            pos += displacement;

            //update dest position
            dest.X = pos.X;
            dest.Y = pos.Y;
        }

        internal virtual void Render()
        {
            if (Raylib.IsTextureReady(textureSource))
                Raylib.DrawTexturePro(textureSource, src, dest, Vector2.Zero, 0, Raylib.WHITE);
            else
                Raylib.DrawRectanglePro(dest, Vector2.Zero, 0f, Raylib.BLACK);
        }
    }
}
