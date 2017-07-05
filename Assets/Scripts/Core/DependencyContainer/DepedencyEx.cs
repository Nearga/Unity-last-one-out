namespace LastOneOut
{
	public static class DepedencyEx
	{
		public static void Inject(this object obj)
		{
			DependencyContainer.Instance.Inject(obj);
		}
	}
}