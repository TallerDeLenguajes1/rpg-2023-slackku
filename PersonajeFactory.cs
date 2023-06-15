namespace PersonajeSpace
{
    class PersonajeFactory
    {
        public PersonajeFactory() { }
        public Personaje getPersonaje()
        {
            Random rnd = new Random();
            Personaje personaje = new Personaje();

            personaje.Nombre = Constantes.nombres[rnd.Next(0, Constantes.nombres.Length - 1)];
            personaje.Apodo = Constantes.apodos[rnd.Next(0, Constantes.apodos.Length - 1)];
            personaje.Raza = Constantes.razas[rnd.Next(0, Constantes.razas.Length - 1)];
            
            // Edad random
            personaje.Edad = rnd.Next(15, 73);
            // Obtencion de año de nacimiento
            DateTime now = DateTime.Now;
            int yearNac = now.Year - personaje.Edad;
            int[] dayMonth = { rnd.Next(1, 12), rnd.Next(1, 31) };
            personaje.FecNac = new DateTime(yearNac, dayMonth[0], dayMonth[1]);

            personaje.Velocidad = rnd.Next(0, 10);
            personaje.Destreza = rnd.Next(0, 5);
            personaje.Fuerza = rnd.Next(0, 10);
            personaje.Nivel = rnd.Next(0, 10);
            personaje.Armadura = rnd.Next(0, 10);
            personaje.Salud = 100;

            return personaje;
        }

    }
}