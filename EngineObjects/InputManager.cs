using System;
using System.Collections.Generic;
using System.Diagnostics;
using GlBindings;

namespace openGlTest.EngineObjects
{
    public static class InputManager
    {
        private const int GLFW_RELEASE = 0;
        private const int GLFW_PRESS = 1;
        public static Dictionary<int, bool> mapIntDown = new Dictionary<int, bool>();
        public static bool GetKeyDown(int key)
        {
            /* Debug.Assert(Program.window != null); */
            return Glfw.GetKey(Program.window, key) == GLFW_PRESS;
        }
        public static void KeyEvent(IntPtr window, int key, int scancode, int action, int modifiers)
        {
            /* mapIntDown.Clear(); */
            /* if (mapIntDown.ContainsKey(key) && action == 1) */
            /* { */
            /*     mapIntDown[key] = true; */
            /* } */
            /* else */
            /* { */
            /*     mapIntDown.Add(key, true); */
            /* } */
        }
        public static void Update()
        {
            /* mapIntDown.Clear(); */
        }
    }
}
