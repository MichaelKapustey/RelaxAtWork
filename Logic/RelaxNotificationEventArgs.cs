using System;

namespace Relaxer
{
	public class RelaxNotificationEventArgs:EventArgs
	{
		public string Title { get; private set;}

		public string Message { get; private set;}

		public int RelaxInterval{ get; private set;}

		public RelaxNotificationEventArgs (string title, string message, int relaxInterval)
		{
			Title=title;
			Message = message;
			RelaxInterval = relaxInterval;

		}
	}
}

