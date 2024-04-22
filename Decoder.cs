using System.Linq;

interface Decoder {
	List<Rede> Decode(string input);
}

public class WindowsDecoder : Decoder {
	public List<Rede> Decode(string input) {
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
						RSSI = Int32.Parse(rssi),
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
			string[] parts = p.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
			string bssid;
			string rssi;
			if (parts[0] == "*") {
				bssid = parts[1];
				rssi = parts[6];
			} else {
				bssid = parts[0];
				rssi = parts[5];
			}

			redes.Add(new Rede{
					BSSID = bssid,
					RSSI = Int32.Parse(rssi),
					});
		}

		return redes;
	}
}
