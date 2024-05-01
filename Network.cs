using System.Diagnostics;

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
	public string BSSID { get; set; } = string.Empty;
	public string? Nome { get; set; }
	private int _Sinal { get; set; }
	public int Sinal {
		get { return _Sinal; }
		set { _Sinal = value; }
	}
	public int RSSIdBm {
		get { return (_Sinal / 2) - 100; }
	}

	public override string ToString() {
		return "Rede{ Nome: " + this.Nome 
			+ ", BSSID: " + this.BSSID + ", Sinal: " 
			+ this.Sinal + ", RSSIdBm: " + this.RSSIdBm + " }";
	}
}
