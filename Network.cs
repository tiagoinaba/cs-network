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
				Arguments = "wlan show networks",
				UseShellExecute = false,
				RedirectStandardOutput = true,
			}
		};

		p.Start();

		// Pular primeira linha
		p.StandardOutput.ReadLine();
		var output = p.StandardOutput.ReadToEnd();

		var d = new LinuxDecoder();

		return d.Decode(output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

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

		return d.Decode(output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
	}
}

public class Rede {
	public required string BSSID { get; set; }
	public required int	   RSSI  { get; set; }

	public override string ToString() {
		return "Rede{ BSSID: " + this.BSSID + ", RSSI: " + this.RSSI + "}";
	}
}
