using System;
using Gtk;
using System.Timers;
using Relaxer;


public partial class MainWindow: Gtk.Window

{	private int _secondsLeftForShortBreak=App.Settings.ShortBreakInterval;
	private int _secondsLeftForLongBreak=App.Settings.LongBreakInterval;
	private int _secondsLeftToWorkout=App.Settings.WorkoutInterval;

	private Timer _breakTimer = new Timer(1000);

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		_breakTimer.Elapsed += Timer_Elapsed;
		_breakTimer.Start ();
	}

	void UpdateTime(object sender, EventArgs e){
		this.labelTimeForShortBreak.Text = _secondsLeftForShortBreak.ToString();
		this.labelTimeForLongBreak.Text = _secondsLeftForLongBreak.ToString();
		this.labelTimeForWorkout.Text = _secondsLeftToWorkout.ToString();
	}

	void ShowNotification (object sender, EventArgs e)
	{
		_breakTimer.Stop();

		var args = e as RelaxNotificationEventArgs;
		var title = args.Title;
		var message = args.Message;
		var time = args.RelaxInterval;

		ShowMessage(message,title,time);
		_breakTimer.Start();
	}

	void Timer_Elapsed (object sender, ElapsedEventArgs e)
	{
		if (_secondsLeftForShortBreak<=0)
		{
			Gtk.Application.Invoke (this,new RelaxNotificationEventArgs("Short break",
			                                                            "Time for short break. Lets make some eyes excersises.",
			                                                            App.Settings.ShortBreakTime)
			                                                            ,ShowNotification);
			_secondsLeftForShortBreak=App.Settings.ShortBreakInterval;
			return;
		}
		_secondsLeftForShortBreak--;

		if (_secondsLeftForLongBreak<=0)
		{
			Gtk.Application.Invoke (this,new RelaxNotificationEventArgs("Long break", 
			                                                            "Time for long break. Go for a walk.",
			                                                            App.Settings.LongBreakTime)
			                        ,ShowNotification);

			_secondsLeftForLongBreak=App.Settings.LongBreakInterval;
			return;
		}
		_secondsLeftForLongBreak--;

		if (_secondsLeftToWorkout<=0)
		{
			Gtk.Application.Invoke (this,new RelaxNotificationEventArgs("Workout!", 
			                                                            "Time for workout! Go and do it!",
			                                                            App.Settings.WorkoutTime)
			                        ,ShowNotification);
			_secondsLeftToWorkout=App.Settings.WorkoutInterval;
			return;
		}
		_secondsLeftToWorkout--;
		Gtk.Application.Invoke (UpdateTime);
	}

	void ShowMessage(string text,string title,int time){
		var msdSame = 
			new RelaxDialog(time,text);
		msdSame.KeepAbove = true;
		msdSame.Fullscreen ();
		msdSame.Run();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void ButtonPlayPause_Click (object sender, EventArgs e)
	{(sender as Gtk.Button).Label=_breakTimer.Enabled?"Play":"Pause";
		_breakTimer.Enabled = !_breakTimer.Enabled;
	}

	protected void ButtonSettings_Click (object sender, EventArgs e)
	{
		_breakTimer.Stop ();
		var settingsDialog = new SettingsDialog ();
		settingsDialog.Run ();
		Refresh ();
		_breakTimer.Start ();
	}

	private void Refresh()
	{
		_secondsLeftForShortBreak=App.Settings.ShortBreakInterval;
		_secondsLeftForLongBreak=App.Settings.LongBreakInterval;
		_secondsLeftToWorkout=App.Settings.WorkoutInterval;	
	}
}
