using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

// Empty components can be used to tag entities
public struct BadAppleTag : IComponentData
{
}

public struct BadAppleBottomY : IComponentData
{
    // If you have only one field in a component, name it "Value"

    public float Value;
}

public struct BadAppleDamage : IComponentData
{
    // If you have only one field in a component, name it "Value"

    public float Value;
}

public struct ZigZagMovement : IComponentData //ADDED
{
    public float Speed;
    public float Amplitude;
    public float Frequency;
}

[DisallowMultipleComponent]
public class BadAppleAuthoring : MonoBehaviour
{
    [SerializeField] private float bottomY = -14f;
    [SerializeField] private int damage = 50; 

    [SerializeField] private float zigzagSpeed = 1f; //ADDED
    [SerializeField] private float zigzagFrequency = 2f;
    [SerializeField] private float zigzagAmplitude = 0.5f;

    private class BadAppleAuthoringBaker : Baker<BadAppleAuthoring>
    {
        public override void Bake(BadAppleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<BadAppleTag>(entity);
            AddComponent(entity, new BadAppleBottomY { Value = authoring.bottomY });
            AddComponent(entity, new BadAppleDamage { Value = authoring.damage });

            if (SceneManager.GetActiveScene().name == "Hard")
            {
                // Only add the ZigZagMovement component in the hard level
                AddComponent(entity, new ZigZagMovement
                {
                    Speed = authoring.zigzagSpeed,
                    Amplitude = authoring.zigzagAmplitude,
                    Frequency = authoring.zigzagFrequency
                });
            }
        }
    }
}
