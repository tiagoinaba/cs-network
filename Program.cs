using System.Runtime.InteropServices;

class Program {
	static void Main() {
		Network n;
		switch (true) {
			case true when RuntimeInformation.OSDescription.ToUpper().Contains("WINDOWS"):
				Console.WriteLine("windows");
				break;
			case true when RuntimeInformation.OSDescription.ToUpper().Contains("LINUX"):
				n = new LinuxNetwork();
				var redes = n.ListarRedes();
				redes.ForEach((rede) => {
						Console.WriteLine(rede);
						});
				break;
			default:
				throw new OSNotFoundException("OS not found!");
		}
		
	}
}
