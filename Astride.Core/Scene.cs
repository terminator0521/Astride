using Astride.Input;

namespace Astride.Core
{
    abstract internal class Scene
    {
        /// <summary>
        /// null means no change <br/>
        /// negative hints previous scenes <br/>
        /// positive hints next scenes
        /// </summary>
        protected abstract int NextScene { get; protected private set; }

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
        /// <param name="inputManagers"></param> Singular object that inherit the IInputHandler interface.
        virtual internal void InputUpdate(params IInputHandler[] inputManagers)
        {
            foreach (IInputHandler inputManager in inputManagers)
            {
                inputManager.InputUpdate();
            }
        }
    }
}
