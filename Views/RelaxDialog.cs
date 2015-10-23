using System;
using System.Timers;

namespace Relaxer
{
	public partial class RelaxDialog : Gtk.Dialog
	{
		#region Fields

		private Timer _breakTimer = new Timer(1000);
		private int _secondsLeft;

		#endregion

		#region Constructors

		public RelaxDialog ()
		{
			this.Build ();

			_breakTimer.Elapsed += BreakTimer_Elapsed;
			_breakTimer.Start ();
		}

		public RelaxDialog(int secondsBeforeClose, string text):this()
		{
			_secondsLeft = secondsBeforeClose;
			labelText.Text = text+"\nleft: "+_secondsLeft+"; ";
		}

		#endregion

		#region Handlers

		private void BreakTimer_Elapsed (object sender, ElapsedEventArgs e)
		{
			_secondsLeft--;
			if (_secondsLeft > 0) {
				Gtk.Application.Invoke (UpdateTime);
			} else {
				CleanResourcesAndDestroy ();
			}
		}

		private void UpdateTime(object sender, EventArgs e)
		{
			labelText.Text+=_secondsLeft+"; ";
		}

		private void ButtonClose_Clicked (object sender, EventArgs e)
		{
			CleanResourcesAndDestroy ();
		}

		#endregion

		#region Private Methods

		private void CleanResourcesAndDestroy(){
			_breakTimer.Dispose ();
			Destroy ();
		}

		#endregion
	}
}

