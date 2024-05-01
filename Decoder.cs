using System.Text.Json;

interface Decoder {
	List<Rede> Decode(string input);
}

public class WindowsDecoder : Decoder {
	public List<Rede> Decode(string input) {
		List<Rede>? res = JsonSerializer.Deserialize<List<Rede>>(input);
		if (res != null) return res;

		string[] lines = input.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
		var redes = new List<Rede>();

		foreach (string line in lines) {
			string[] parts = line.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			string bssid = "";
			string rssi = "";

			foreach (string p in parts) {
				string[] prop = p.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

				if (prop[0].Contains("BSSID")) {
					bssid = String.Join(":", prop.Skip(1).Take(6));
				} else if (prop[0].Contains("Sinal")) {
					rssi = prop[1].Split("%")[0];
				}
			}

			if (bssid != "" && rssi != "")
				redes.Add(new Rede{
						BSSID = bssid,
						Sinal = Int32.Parse(rssi),
						});
		}

		return redes;
	}
}

public class LinuxDecoder : Decoder {
	public List<Rede> Decode(string input) {
		string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		var redes = new List<Rede>();

		foreach (string p in lines) {
			// Dois espaços para separar mais precisamente os campos
			string[] parts = p.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
			string bssid;
			string rssi;
			string nome;

			if (parts[0] == "*") {
				bssid = parts[1];
				nome = parts[2];
				rssi = parts[6];
			} else {
				bssid = parts[0];
				nome = parts[1];
				rssi = parts[5];
			}

			redes.Add(new Rede{
					BSSID = bssid.Trim(),
					Sinal = Int32.Parse(rssi),
					Nome = nome.Trim(),
					});
		}

		return redes;
	}
}
