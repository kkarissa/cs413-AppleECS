using Unity.Burst;
using Unity.Entities;

public struct Timer : IComponentData
{
    public float Value;
}

public struct BadAppleTimer : IComponentData
{
    public float Value;
}

public partial struct TimerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var timer in SystemAPI.Query<RefRW<Timer>>())
        {
            timer.ValueRW.Value = timer.ValueRO.Value - deltaTime;
        }

        foreach (var badTimer in SystemAPI.Query<RefRW<BadAppleTimer>>())
        {
            badTimer.ValueRW.Value = badTimer.ValueRO.Value - deltaTime;
        }
    }
}
