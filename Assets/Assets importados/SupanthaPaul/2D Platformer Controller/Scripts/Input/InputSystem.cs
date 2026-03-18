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

        static float lastLeftTapTime = 0f;
        static float lastRightTapTime = 0f;
        static float doubleTapTime = 0.3f;

        public static bool Dash()
        {
            bool dash = false;

            // IZQUIERDA (A o Flecha izquierda)
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Time.time - lastLeftTapTime <= doubleTapTime)
                {
                    dash = true;
                }
                lastLeftTapTime = Time.time;
            }

            // DERECHA (D o Flecha derecha)
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Time.time - lastRightTapTime <= doubleTapTime)
                {
                    dash = true;
                }
                lastRightTapTime = Time.time;
            }

            return dash;
        }
    }
}