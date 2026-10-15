namespace Astride.Input
{
    public interface IInputHandler
    {
        /// <summary>
        /// Scan for inputs. Do NOT use frametime adjustments here
        /// </summary>
        public void InputUpdate();
    }
}
