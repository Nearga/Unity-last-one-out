using PureMVC.Interfaces;
using UnityEngine;

namespace LastOneOut
{
    public class InitializeCommand : BaseCommand
	{
        public override void Execute(INotification notification)
        {
            Debug.Log("InitializeCommand.Execute");

			// Load MainMenuView
			var view = MainMenuView.Instance;
		}
    }
}