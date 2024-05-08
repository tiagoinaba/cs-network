using System.Runtime.InteropServices;

class Program {
	static Dictionary<string, Command> commands = new Dictionary<string, Command>() {
		{"teste", new Add()}
	};

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
		}

		bool quit = false;

		while (!quit) {
			Console.WriteLine("guiame > ");
			String? command = Console.ReadLine();
			if (command == null) continue; 
			string[] partes = command.Split([' ']);

			switch (partes[0]) {
				case "help": 
					help();
					break;
				default:
					help();
					break;
			}
		}
	}

	/// <summary>Escreve a página de ajuda para o usuário.</summary>
	static void help() {
		Console.WriteLine(@"Comandos: 
	add  {sala | ponto | medida | roteador}
	list {sala | ponto | medida | roteador}
	rm   {sala | ponto | medida | roteador} {código}");
	}

	static void help(string verb) {
		Command cmd = commands[verb];
		if (cmd != null) cmd.help();
		else {
			Console.WriteLine($"Comando não encontrado: {verb}\n");
			help();
		}
	}
}
