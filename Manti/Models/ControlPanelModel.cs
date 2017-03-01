using System.Diagnostics;
using System.IO;

using Manti.Classes.Settings;

namespace Manti.Models {
	public class ControlPanelModel {
		private ControlPanelModel() { isWorldOnline = false; isAuthOnline = false; }

		private static ControlPanelModel model;

		public bool isWorldOnline { get; set; }
		public bool isAuthOnline { get; set; }

		public string pathAuthServer { get; set; }
		public string pathWorldServer { get; set; }
		public string pathAuthConfig { get; set; }
		public string pathWorldConfig { get; set; }

		public string findFile(string contain, string extension) {
			string path = Settings.getSetting(Setting.PathServer);

			string sFile = null;

			foreach(var file in Directory.GetFiles(path)) {
				string fileName = Path.GetFileName(file);
				string ext = Path.GetExtension(file);

				if(fileName.Contains(contain) && ext == extension) {
					sFile = fileName;
					break;
				}
			}

			return sFile;
		}

		public bool findServerPaths(string startPath) {
			bool pathsFound = false;
			char dsc = Path.DirectorySeparatorChar;

			string authServer = startPath + dsc + findFile("auth", ".exe");
			string worldServer = startPath + dsc + findFile("world", ".exe");

			if(File.Exists(authServer) && File.Exists(worldServer)) {
				pathAuthServer  = authServer;
				pathWorldServer = worldServer;

				pathsFound = true;
			}

			return pathsFound;
		}
		public bool findConfigPaths(string startPath) {
			bool pathsFound = false;
			char dsc = Path.DirectorySeparatorChar;

			string authConfig = startPath + dsc + findFile("auth", ".conf");
			string worldConfig = startPath + dsc + findFile("world", ".conf");

			if(File.Exists(authConfig) && File.Exists(worldConfig)) {
				pathAuthConfig = authConfig;
				pathWorldConfig = worldConfig;

				pathsFound = true;
			}

			return pathsFound;
		}	

		public bool startProcess(string path, bool hideProcess) {
			var process = new System.Diagnostics.Process();

			process.StartInfo.FileName = path;
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
			process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;

			if(hideProcess) {
				process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				process.StartInfo.CreateNoWindow = true;
			}

			try {
				process.Start();
				return true;
			} catch { return false; }
		}
		public bool closeProcess(string path) {
			string fileName = Path.GetFileName(path);
			fileName = fileName.Substring(0, fileName.Length - 4);

			bool closedProcess = false;

			foreach(var process in Process.GetProcessesByName(fileName)) {
				process.Kill();
				closedProcess = true;
			}

			return closedProcess;
		}
		public bool isProcessRunning(string path) {
			string fileName = Path.GetFileName(path);
			fileName = fileName.Substring(0, fileName.Length - 4);

			Process[] pName = Process.GetProcessesByName(fileName);

			if(pName.Length > 0) { return true; }

			return false;
		}

		public static ControlPanelModel getInstance() {
			if(model == null) { model = new ControlPanelModel(); }

			return model;
		}
	}
}
