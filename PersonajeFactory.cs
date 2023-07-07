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

            // Edad random en base a raza
            switch (personaje.Raza)
            {
                case "Humano":
                    personaje.Edad = rnd.Next(16, 80);
                    break;
                case "Elfo":
                    personaje.Edad = rnd.Next(16, 300);
                    break;
                case "Goblin":
                    personaje.Edad = rnd.Next(16, 100);
                    break;
                case "DemiHumano":
                    personaje.Edad = rnd.Next(16, 150);
                    break;
                case "Enano":
                    personaje.Edad = rnd.Next(16, 200);
                    break;
            }
            // Obtencion de año de nacimiento
            DateTime now = DateTime.Now;
            int yearNac = now.Year - personaje.Edad;
            int[] dayMonth = { rnd.Next(1, 12), rnd.Next(1, 31) };
            personaje.FecNac = new DateTime(yearNac, dayMonth[0], dayMonth[1]);
            // Stats random
            personaje.Velocidad = rnd.Next(1, 10);
            personaje.Destreza = rnd.Next(1, 5);
            personaje.Fuerza = rnd.Next(1, 10);
            personaje.Nivel = rnd.Next(1, 10);
            personaje.Armadura = rnd.Next(1, 10);
            personaje.Salud = 100;

            return personaje;
        }

    }
}