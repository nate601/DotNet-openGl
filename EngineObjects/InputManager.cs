using System;
using System.Collections.Generic;

namespace openGlTest.EngineObjects
{
    public static class InputManager
    {
        public static Dictionary<int, bool> mapIntDown = new Dictionary<int, bool>();
        public static bool GetKeyDown(int key)
        {
            if(mapIntDown.ContainsKey(key))
            {
                return mapIntDown[key];
            }
            return false;
        }
        public static void KeyEvent(IntPtr window, int key, int scancode, int action, int modifiers)
        {
            mapIntDown.Clear();
            if (mapIntDown.ContainsKey(key) && action == 1)
            {
                mapIntDown[key] = true;
            }
            else
            {
                mapIntDown.Add(key, true);
            }
        }
    }
}
