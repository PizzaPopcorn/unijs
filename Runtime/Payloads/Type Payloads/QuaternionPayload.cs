
using UnityEngine;

namespace UniJS.Payloads
{
    [System.Serializable]
    public class QuaternionPayload
    {
        public float x;
        public float y;
        public float z;
        public float w = 1f;

        public QuaternionPayload() { }

        public QuaternionPayload(Quaternion quaternion)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
    }
}
