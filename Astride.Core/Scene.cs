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
        internal abstract void Update();
        internal abstract void Render();

        internal abstract void UI();

        internal abstract Scene SetNextScene();

        virtual internal void InputUpdate(params IInputHandler[] inputManagers)
        {
            foreach (var inputManager in inputManagers)
            {
                inputManager.InputUpdate();
            }
        }
    }
}
