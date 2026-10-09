using Astride.Input;

namespace Astride.Core
{
    abstract internal class Scene
    {
        abstract public void Update();
        abstract public void Render();

        abstract public void UI();

        abstract internal Scene SetNextScene();

        virtual internal void InputUpdate(params ControlListener[] inputManagers)
        {
            foreach (var inputManager in inputManagers)
            {
                inputManager.InputUpdate();
            }
        }
    }
}
