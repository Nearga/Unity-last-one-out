using PureMVC.Patterns.Proxy;
using System;
using UnityEngine;

namespace LastOneOut
{
	public class GameSettingsProxy : Proxy
	{
		public GameSettingsProxy() : base("SettingsProxy") { }
		public GameSettingsProxy(string name) : base(name) { }

		public GameSettingsModel GameSettings { get; private set; }

		public override void OnRegister()
		{
			// Load settings from the scriptable objects
			var settings = Resources.Load("Settings/GameSettings", typeof(ScriptableObject));
			if (settings == null)
				Debug.LogException(new Exception("GameSettings cant be loaded"));

			GameSettings = settings as GameSettingsModel;			
		}
	}
}