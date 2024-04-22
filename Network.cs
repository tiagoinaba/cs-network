using System.Diagnostics;

interface Network {
	public List<Rede> ListarRedes(); 
}

class WindowsNetwork : Network {
	public List<Rede> ListarRedes() {
		// Processo para executar comando no console.
		var p = new Process {
			StartInfo = {
				FileName = "netsh",
				Arguments = "wlan show networks mode=bssid",
				UseShellExecute = false,
				RedirectStandardOutput = true,
			}
		};

		p.Start();

		var output = p.StandardOutput.ReadToEnd();

		var d = new WindowsDecoder();

		return d.Decode(output);
	}
}

class LinuxNetwork : Network {
	public List<Rede> ListarRedes() {
		// Processo para executar comando no console.
		var p = new Process {
			StartInfo = {
				FileName = "nmcli",
				Arguments = "d w list",
				UseShellExecute = false,
				RedirectStandardOutput = true,
			}
		};

		p.Start();

		// Pular primeira linha
		p.StandardOutput.ReadLine();
		var output = p.StandardOutput.ReadToEnd();

		var d = new LinuxDecoder();

		return d.Decode(output);
	}
}

public class Rede {
	public string BSSID { get; set; }
	private int _RSSI { get; set; }
	public int RSSI {
		get { return _RSSI; }
		set { _RSSI = value; }
	}
	public int RSSIdBm {
		get { return (_RSSI / 2) - 100; }
	}

	public override string ToString() {
		return "Rede{ BSSID: " + this.BSSID + ", RSSI: " + this.RSSI + ", RSSIdBm: " + this.RSSIdBm + "}";
	}
}
