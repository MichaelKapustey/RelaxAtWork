using System;
using Gtk;

namespace Relaxer
{
	public partial class SettingsDialog : Gtk.Dialog
	{
		public SettingsDialog ()
		{
			this.Build ();

			var settings = App.Settings;

			spinShortBreakInterval.Value = settings.ShortBreakInterval; 
			spinLongBreakInterval.Value = settings.LongBreakInterval;
			spinWorkoutInterval.Value = settings.WorkoutInterval;

			spinShortBreakTime.Value = settings.ShortBreakTime;
			spinLongBreakTime.Value = settings.LongBreakTime;
			spinWorkoutTime.Value = settings.WorkoutTime;
		}

		protected async void ButtonOk_Click (object sender, EventArgs e)
		{
			var settings = App.Settings;

			settings.ShortBreakInterval = Convert.ToInt32 (spinShortBreakInterval.Value); 
			settings.LongBreakInterval = Convert.ToInt32 (spinLongBreakInterval.Value);
			settings.WorkoutInterval = Convert.ToInt32 (spinWorkoutInterval.Value);

			settings.ShortBreakTime = Convert.ToInt32 (spinShortBreakTime.Value);
			settings.LongBreakTime = Convert.ToInt32 (spinLongBreakTime.Value);
			settings.WorkoutTime = Convert.ToInt32 (spinWorkoutTime.Value);

			var result= await App.SaveSettingsAsync();
			if (!result.Success)
			{
				MessageDialog d = new MessageDialog (this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok,"");
				d.Text=result.Exception.Message;
				d.Run();
			}

			this.Destroy ();
		}

		protected void ButtonCancel_Click (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}

