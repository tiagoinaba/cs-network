using System.Diagnostics;
using System.Text.Json.Serialization;

interface Network {
	public List<Rede> ListarRedes(); 
}

class WindowsNetwork : Network {
	public List<Rede> ListarRedes() {
		// Processo para executar comando no console.
		var p = new Process {
			StartInfo = {
				FileName = "python",
				Arguments = "-m lswn",
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
	[JsonPropertyName("BSSID")]
	public string BSSID { get; set; } = string.Empty;

	[JsonPropertyName("SSID")]
	public string? SSID { get; set; }

	[JsonPropertyName("RSSIdBm")]
	public int RSSIdBm { get; set; }

	public override string ToString() {
		return "Rede{ SSID: " + this.SSID 
			+ ", BSSID: " + this.BSSID + ", RSSIdBm: " + this.RSSIdBm + " }";
	}
}
