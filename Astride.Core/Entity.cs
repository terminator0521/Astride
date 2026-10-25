using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core
{
    internal abstract class Entity : IDisposable
    {
        internal bool IsEnabled { get; private protected set; } = false; //if astroid is enabled

        //array of sound effects
        protected AudioStream[]? soundEffect;

        //number of entities that have been initiated
        internal static int s_instances = 0;

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

        internal Rectangle HitBox { get; protected private set; }

        /// <summary>
        /// Rectangle used for parsing sprite.
        /// </summary>
        protected Rectangle src;

        /// <summary>
        /// Sprite or spritesheet of your texture.
        /// </summary>
        protected Texture textureSource;

        /// <summary>
        /// Represents the angle value in degrees or radians.
        /// </summary>
        protected float angle;

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
            s_instances++;
        }

        /// <summary>
        /// Contruct with declared size and undeclared pos
        /// </summary>
        /// <param name="width">Width of entity</param>
        /// <param name="height">Size of entity</param>
        internal Entity(int width, int height)
        {
            s_instances++;
            origin = new Vector2(width / 2, height / 2);
            src = new Rectangle(0f, 0f, width, height);
            dest = new Rectangle(0f, 0f, width, height);
            HitBox = new Rectangle(dest.X - origin.X, dest.Y - origin.Y, width, height);
        }


        /// <summary>
        /// Construct with declared pos and size
        /// </summary>
        /// <param name="width">Width of entity</param>
        /// <param name="height">Size of entity</param>
        /// <param name="pos">Position of entity</param>
        internal Entity(int width, int height, Vector2 pos)
        {
            s_instances++;
            dest = new Rectangle(pos.X, pos.Y, width, height);
            src = new Rectangle(0f, 0f, width, height);
            origin = new Vector2(width / 2, height / 2);
            displacement = Vector2.Zero;
            HitBox = new Rectangle(dest.X - origin.X, dest.Y - origin.Y, width, height);
            this.pos = pos;
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

            //update hitbox pos
            HitBox = HitBox with
            {
                X = dest.X - origin.X,
                Y = dest.Y - origin.Y
            };
        }

        /// <summary>
        /// Does gpu draw calls
        /// </summary>
        internal virtual void Render()
        {
            //check if texture is loaded
            if (Raylib.IsTextureReady(textureSource))
                //draw texture
                Raylib.DrawTexturePro(textureSource, src, dest, origin, angle, Raylib.WHITE);
            else
                //draw placeholder rectangle
                Raylib.DrawRectanglePro(dest, origin, 0f, Raylib.BLACK);
        }

        /// <summary>
        /// Handle collision logic
        /// </summary>
        /// <param name="e">reference of entity object collided with</param>
        internal abstract void OnCollision(Entity e);

        /// <summary>
        /// Initializes entity texture
        /// </summary>
        /// <param name="texturePath">Path of texture</param>
        protected void UpdateTexture(string texturePath)
        {
            //load texture to vram
            textureSource = Raylib.LoadTexture(texturePath);

            //init parsing rectangle
            src = new Rectangle(0, 0, textureSource.width, textureSource.height);
        }

        public void Dispose()
        {
            //unload texture
            Raylib.UnloadTexture(textureSource);

            //check if soundEffect array is null or empty
            if (soundEffect?.Length != 0)
            {
                //delete each sound effect
                for (int i = 0; i < soundEffect?.Length; i++)
                {
                    Raylib.UnloadAudioStream(soundEffect[i]);
                }
            }
            //nullify container reference
            soundEffect = null;

            //subtract one from total instances created
            s_instances--;
        }

        /// <summary>
        /// Returns a random index number of an array
        /// </summary>
        /// <typeparam name="A">Array type</typeparam>
        /// <param name="array">Array refernce</param>
        /// <returns></returns>
        protected A RandomIndex<A>(in A[] array)
        {
            return array[Random.Shared.Next(array.Length)]; //return random index in given array
        }
    }
}
