namespace Foundation.Patterns
{
	static class Facade
	{
		public static GlobalEventManager GlobalEvents => GlobalEventManager.Instance;
		public static GameSettings Settings => GameSettingsContainer.Instance.GameSettings;
	}
}