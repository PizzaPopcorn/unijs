using System;

namespace UniJS.Payloads
{
    [Serializable]
    public class FloatPayload
    {
        public string name;
        public float value;
    }

    [Serializable]
    public class IntPayload
    {
        public string name;
        public int value;
    }

    [Serializable]
    public class BoolPayload
    {
        public string name;
        public bool value;
    }

    [Serializable]
    public class LayerWeightPayload
    {
        public int layerIndex;
        public float weight;
    }
}
