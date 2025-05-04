namespace TestProto
{
	public class GroundChunksPool : ObjectPool<GroundChunk>
	{
		protected override void InitializeObject(GroundChunk chunk)
		{
			chunk.Initialize();
			chunk.OnReadyToReturn += ReturnToPool;
		}

		protected override void CleanupObject(GroundChunk chunk)
		{
			chunk.OnReadyToReturn -= ReturnToPool;
		}
	}
}