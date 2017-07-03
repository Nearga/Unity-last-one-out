using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace LastOneOut
{
	// Obligatory for every command.
    //public partial class Commands
    //{
    //    public static string InitializeCommand = "InitializeCommand";
    //}

    public class InitializeCommand : BaseCommand
	{
        public override void Execute(INotification notification)
        {
            Debug.Log("InitializeCommand.Execute");

			// Load StartGameView
			var view = StartGameView.Instance;
		}
    }
}