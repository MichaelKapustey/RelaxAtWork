using System;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RelaxAtWork;

namespace Relaxer
{
	public static class App
	{
	
		#region Fields

		private static Settings _settings;
		private static string _pathToFile = "settings.net";

		#endregion

		#region Properties

		public static Settings Settings 
		{
			get 
			{
				return _settings ?? LoadSettings ();
			}
		}

		#endregion

		#region Public Methods

		public static async Task<OperationStatus> SaveSettingsAsync()
		{
			try
			{
				await Task.Factory.StartNew(SaveSettings);	
				return new OperationStatus(true);
			}
			catch (Exception ex)
			{
				return new OperationStatus(ex);
			}

		}

		public static void SaveSettings ()
		{
			using (var fileStream = File.Create (_pathToFile)) {
				Serialize (_settings, fileStream);
			}
		}

		public static async Task<OperationResult<Settings>> LoadSettingsAsync()
		{
			try
			{
				await Task.Factory.StartNew(LoadSettings);	
				return new OperationResult<Settings>(_settings);
			}
			catch (Exception ex)
			{
				return new OperationResult<Settings>(ex);
			}

		}

		public static Settings LoadSettings ()
		{
			using (var fileStream = File.OpenRead (_pathToFile))
			{
				_settings = Deserialize<Settings> (fileStream);
			}
			return _settings;
		}

		#endregion

		#region Private Methods

		private static void Serialize (object obj, Stream stream)
		{
			var settingsFormatter = new BinaryFormatter ();
			settingsFormatter.Serialize (stream, obj);			
		}

		private static T Deserialize<T> (Stream stream)
		{
			var settingsFormatter = new BinaryFormatter ();
			return (T)settingsFormatter.Deserialize (stream);
		}

		#endregion

	}

}

