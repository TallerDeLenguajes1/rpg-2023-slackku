using PersonajeSpace;

PersonajeFactory personajeFactory = new PersonajeFactory();
int randomQPeronsajes = new Random().Next(4, 6);
List<Personaje> listaPersonajes = new List<Personaje>();
for (int i = 0; i < randomQPeronsajes; i++)
{
    Personaje personaje = personajeFactory.getPersonaje();
    listaPersonajes.Add(personaje);
}

listaPersonajes.ForEach(personaje =>
{
    Console.WriteLine("---------------------");
    Console.WriteLine("      PERSONAJE      ");
    Console.WriteLine("---------------------");
    Console.WriteLine($"Nombre: {personaje.Nombre}");
    Console.WriteLine($"Apodo: {personaje.Apodo}");
    Console.WriteLine($"Raza: {personaje.Raza}");
    Console.WriteLine("Fecha de Nacimiento: {0}/{1}/{2}", personaje.FecNac.Day.ToString(), personaje.FecNac.Month.ToString(), personaje.FecNac.Year.ToString());
    Console.WriteLine($"Edad: {personaje.Edad}");

    Console.WriteLine("---------------------");
    Console.WriteLine("        STATS        ");
    Console.WriteLine("---------------------");

    Console.WriteLine($"HP: {personaje.Salud}");
    Console.WriteLine($"DES: {personaje.Destreza}");
    Console.WriteLine($"VEL: {personaje.Velocidad}");
    Console.WriteLine($"STR: {personaje.Fuerza}");
    Console.WriteLine($"LVL: {personaje.Nivel}");
    Console.WriteLine($"ARMOR: {personaje.Armadura}");
    Console.WriteLine("======================");
});

