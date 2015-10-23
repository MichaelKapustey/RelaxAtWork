using System;
using Gtk;
using Relaxer;
using System.Threading.Tasks;

namespace RelaxAtWork
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			App.LoadSettings ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
