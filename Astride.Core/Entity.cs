using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core
{
    internal abstract class Entity
    {
        /// <summary>
        /// Returns real position with origin adjustment
        /// </summary>
        internal Vector2 CenterPos
        {
            get
            {
                return pos + origin;
            }
        }

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
        internal Vector2 pos;

        /// <summary>
        /// Origin and rotation
        /// </summary>
        internal Vector2 origin;

        /// <summary>
        /// displacement vector used to update position per frame
        /// </summary>
        protected Vector2 displacement = Vector2.Zero;

        /// <summary>
        /// Construct with undeclared pos and size
        /// </summary>
        internal Entity()
        {

        }

        /// <summary>
        /// Contruct with declared size and undeclared pos
        /// </summary>
        /// <param name="width">Width of entity</param>
        /// <param name="height">Size of entity</param>
        internal Entity(int width, int height)
        {
            dest = new Rectangle(0f, 0f, width, height);
            src = new Rectangle(0f, 0f, width, height);
            origin = new Vector2(width / 2, height / 2);
        }


        /// <summary>
        /// Construct with declared pos and size
        /// </summary>
        /// <param name="width">Width of entity</param>
        /// <param name="height">Size of entity</param>
        /// <param name="pos">Position of entity</param>
        internal Entity(int width, int height, Vector2 pos)
        {
            dest = new Rectangle(pos.X, pos.Y, width, height);
            src = new Rectangle(0f, 0f, width, height);
            origin = new Vector2(width / 2, height / 2);
            displacement = Vector2.Zero;
        }

        /// <summary>
        /// Update logic. Only use frametime adjustments here
        /// </summary>
        internal virtual void Update(float delta)
        {
            //update position    
            pos += displacement * delta;

            //update dest position
            dest.X = pos.X;
            dest.Y = pos.Y;
        }

        internal virtual void Render()
        {
            if (Raylib.IsTextureReady(textureSource))
                Raylib.DrawTexturePro(textureSource, src, dest, Vector2.Zero, 0f, Raylib.WHITE);
            else
                Raylib.DrawRectanglePro(dest, Vector2.Zero, 0f, Raylib.BLACK);
        }

        /// <summary>
        /// Collision logic
        /// </summary>
        /// <param name="entities">Entity(s) collided with</param>
        internal abstract void OnCollision(params Entity[] entities);
    }
}
