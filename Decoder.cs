interface Decoder {
	List<Rede> Decode(string[] input);
}

public class LinuxDecoder : Decoder {
	public List<Rede> Decode(string[] input) {
		var redes = new List<Rede>();

		foreach (string p in input) {
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
