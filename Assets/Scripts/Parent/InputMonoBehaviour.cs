using UnityEngine;

namespace Parent
{
    public class InputMonoBehaviour : MonoBehaviour
    {

        protected InputActions GameInput;

        protected void Awake()
        {
            GameInput = new InputActions();
        }

         protected void OnEnable()
        {
            GameInput.Enable();
        }

        protected void OnDisable()
        {
            GameInput.Disable();
        }
    }
}
