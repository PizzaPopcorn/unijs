using UnityEngine;

namespace UniJS.InstanceTools
{
    internal static class JSTimeManager
    {
        public static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        public static float GetTimeScale()
        {
            return Time.timeScale;
        }

        public static float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public static float GetUnscaledDeltaTime()
        {
            return Time.unscaledDeltaTime;
        }

        public static float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }

        public static float GetRealtimeSinceStartup()
        {
            return Time.realtimeSinceStartup;
        }

        public static float GetTime()
        {
            return Time.time;
        }
    }
}
