using System.Text.Json;

interface Decoder {
	List<Rede> Decode(string input);
}

public class WindowsDecoder : Decoder {
	public List<Rede> Decode(string input) {
		Console.WriteLine(input);
		List<Rede>? res = JsonSerializer.Deserialize<List<Rede>>(input);
		if (res != null) return res;
		else return new List<Rede>();
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
					SSID = nome.Trim(),
					});
		}

		return redes;
	}
}
