using PureMVC.Patterns.Proxy;
using System;
using UnityEngine;

namespace LastOneOut
{
	public class SettingsProxy : Proxy
	{
		public SettingsProxy() : base("SettingsProxy") { }
		public SettingsProxy(string name) : base(name) { }

		public GameSettings GameSettings { get; private set; }

		public override void OnRegister()
		{
			// Load settings from the scriptable objects
			var settings = Resources.Load("Settings/GameSettings", typeof(ScriptableObject));
			if (settings == null)
				Debug.LogException(new Exception("GameSettings cant be loaded"));

			GameSettings = settings as GameSettings;			
		}
	}
}