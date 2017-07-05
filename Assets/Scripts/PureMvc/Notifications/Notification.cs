using PureMVC.Interfaces;

namespace LastOneOut
{
	public class Notification : INotification
	{
		public string Name { get; set; }
		public object Body { get; set; }
		public string Type { get; set; }

		public Notification()
		{ }

		public Notification(string name)
		{
			Name = name;
		}

		public Notification(string name, object body)
		{
			Name = name;
			Body = body;
		}

		public Notification(string name, object body, string type)
		{
			Name = name;
			Body = body;
			Type = type;				 
		}
	}
}
