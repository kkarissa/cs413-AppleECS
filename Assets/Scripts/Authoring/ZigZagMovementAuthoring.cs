using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class ZigZagAuthoring : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float frequency = 4f;
    [SerializeField] private float amplitude = 0.05f;

    private class ZigZagAuthoringBaker : Baker<ZigZagAuthoring>
    {
        public override void Bake(ZigZagAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ZigZagMovement
            {
                Speed = authoring.speed,
                Frequency = authoring.frequency,
                Amplitude = authoring.amplitude
            });
        }
    }
}
