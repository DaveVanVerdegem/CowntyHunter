namespace Foundation.Patterns
{
	static class Facade
	{
		public static GlobalEventManager GlobalEvents => GlobalEventManager.Instance;
	}
}