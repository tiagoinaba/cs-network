using System.Runtime.InteropServices;

class Program {
	static void Main() {
		Network n;
		List<Rede> redes;

		switch (true) {
			case true when RuntimeInformation.OSDescription.ToUpper().Contains("WINDOWS"):
				n = new WindowsNetwork();
				redes = n.ListarRedes();
				redes.ForEach((rede) => {
						Console.WriteLine(rede);
						});
				break;
			case true when RuntimeInformation.OSDescription.ToUpper().Contains("LINUX"):
				n = new LinuxNetwork();
				redes = n.ListarRedes();
				redes.ForEach((rede) => {
						Console.WriteLine(rede);
						});
				break;
			default:
				throw new OSNotFoundException("OS not found!");
		}
		
	}
}
