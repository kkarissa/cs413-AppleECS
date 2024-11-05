using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;


[UpdateAfter(typeof(TimerSystem))]
public partial struct BadAppleSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        new SpawnJob { ECB = ecb }.Schedule();
    }

    [BurstCompile]
    private partial struct SpawnJob : IJobEntity
    {
        public EntityCommandBuffer ECB;

        private void Execute(in LocalTransform transform, in BadAppleSpawner spawner, ref BadAppleTimer timer)
        {
            if (timer.Value > 0)
                return;

            timer.Value = spawner.Interval;
            var BadAppleEntity = ECB.Instantiate(spawner.BadApplePrefab);
            ECB.SetComponent(BadAppleEntity, LocalTransform.FromPosition(transform.Position));
        }
    }
}
