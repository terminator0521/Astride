using Astride.Input;

namespace Astride.Core.Scenes
{
    abstract internal class Scene
    {
        abstract public void Update();
        abstract public void Render();

        abstract public void UI();

        abstract internal void SetNextScene(Scene nextScene);

        virtual internal void InputUpdate(params IInputManager[] inputManagers)
        {
            foreach (var inputManager in inputManagers)
            {
                inputManager.InputUpdate();
            }
        }
    }
}
