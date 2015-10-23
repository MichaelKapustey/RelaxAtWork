using System;

namespace Relaxer
{
	[Serializable]
	public class Settings
	{
		public int ShortBreakInterval{ get; set; } 
		public int LongBreakInterval { get; set;}
		public int WorkoutInterval { get; set;}

		public int ShortBreakTime{get;set;}
		public int LongBreakTime{ get; set; }
		public int WorkoutTime{ get; set; }


		public Settings ()
		{
		}
	}
}

