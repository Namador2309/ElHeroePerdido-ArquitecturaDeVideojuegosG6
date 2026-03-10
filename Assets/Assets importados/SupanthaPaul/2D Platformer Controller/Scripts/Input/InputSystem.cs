using UnityEngine;

namespace SupanthaPaul
{
    public class InputSystem : MonoBehaviour
    {
        public static float HorizontalRaw()
        {
            float h = 0f;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                h = -1f;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                h = 1f;

            return h;
        }

        public static bool Jump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public static bool Dash()
        {
            return Input.GetKeyDown(KeyCode.X);
        }
    }
}