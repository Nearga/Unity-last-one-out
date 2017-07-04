using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace LastOneOut
{
    public class InitializeCommand : BaseCommand
	{
        public override void Execute(INotification notification)
        {
            Debug.Log("InitializeCommand.Execute");

			// Load StartGameView
			var view = MainGameView.Instance;
		}
    }
}