using Unity.Entities;
using UnityEngine;

public struct BadAppleSpawner : IComponentData
{
    public Entity BadApplePrefab;
    public float Interval;
}

[DisallowMultipleComponent]
public class BadAppleSpawnerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject BadApplePrefab;
    [SerializeField] private float badAppleSpawnInterval = 5f;

    private class BadAppleSpawnerAuthoringBaker : Baker<BadAppleSpawnerAuthoring>
    {
        public override void Bake(BadAppleSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BadAppleSpawner
            {
                BadApplePrefab = GetEntity(authoring.BadApplePrefab, TransformUsageFlags.Dynamic),
                Interval = authoring.badAppleSpawnInterval
            });
            AddComponent(entity, new BadAppleTimer { Value = 5f });
        }
    }
}
