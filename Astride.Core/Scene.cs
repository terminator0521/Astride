using Astride.Input;

namespace Astride.Core
{
    abstract internal class Scene
    {
        /// <summary>
        /// 0 means no change <br/>
        /// negative hints previous scenes <br/>
        /// positive hints next scenes
        /// </summary>
        protected int NextScene { get; protected private set; } = 0;

        /// <summary>
        /// Handle logic updates
        /// </summary>
        internal abstract void Update();

        /// <summary>
        /// Handle render updates
        /// </summary>
        internal abstract void Render();

        /// <summary>
        /// Handle UI updates
        /// </summary>
        internal abstract void UI();

        /// <summary>
        /// Set Scene
        /// </summary>
        /// <returns>A scene</returns>
        internal abstract Scene SetNextScene();

        /// <summary>
        /// Handles inputs. Must call in Update().
        /// </summary>
        /// <param name="inputManagers">update input handelers in scene.</param> 
        virtual internal void InputUpdate(IInputHandler[] inputManagers)
        {
            foreach (var inputManager in inputManagers)
            {
                inputManager.InputUpdate(); //update 
            }
        }
    }
}
