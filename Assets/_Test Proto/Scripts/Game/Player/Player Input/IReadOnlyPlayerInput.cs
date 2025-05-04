namespace TestProto.Players
{
	public interface IReadOnlyPlayerInput
	{
		public float HorizontalInput { get; }
		
		public bool IsActive { get; }
	}
}