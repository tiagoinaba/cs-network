interface Command {
	public void help();
}

class Add : Command {
	public void help() => Console.WriteLine("add {sala | ponto | roteador | medida} {opções}: cria uma nova sala, ponto, roteador ou medida com as opções passadas");
}
