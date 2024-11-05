using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct ZigZagMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // Check if the current scene is the hard level
        if (SceneManager.GetActiveScene().name != "Hard")
        {
            return; // Exit early if we're not in the hard level
        }

        // Define clamping boundaries
        float minX = -8f; // Adjust based on your scene
        float maxX = 8f; // Adjust based on your scene

        // Apply zigzag movement to bad apples in the hard level
        foreach (var (transform, zigzag) in SystemAPI.Query<RefRW<LocalTransform>, ZigZagMovement>())
        {
            float time = (float)state.WorldUnmanaged.Time.ElapsedTime;
            float offset = Mathf.Sin(time * zigzag.Frequency) * zigzag.Amplitude;

            // Update the transform position to apply zigzag motion
            var position = transform.ValueRW.Position;
            position.x += offset * zigzag.Speed; // Update position based on zigzag speed

            // Clamp the position to prevent going off-screen
            position.x = Mathf.Clamp(position.x, minX, maxX);
            transform.ValueRW.Position = position; // Set the updated position back
        }
    }
}
