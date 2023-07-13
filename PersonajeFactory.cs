namespace PersonajeSpace
{
    class PersonajeFactory
    {
        public PersonajeFactory() { }
        public Personaje getPersonaje()
        {
            Random rnd = new Random();
            Personaje personaje = new Personaje();
            string region = Constantes.regiones[rnd.Next(0, Constantes.regiones.Length - 1)];
            int indexRandomPersonaje = rnd.Next(0, 7);

            personaje.Nombre = Constantes.personajeData[region][indexRandomPersonaje][0];
            personaje.Apodo = Constantes.personajeData[region][indexRandomPersonaje][1];
            personaje.Raza = Constantes.personajeData[region][indexRandomPersonaje][2];
            personaje.Region = region;

            // Edad random en base a raza
            int edad = 0;
            switch (personaje.Raza)
            {
                case "Human":
                case "Human & Yeti":
                    edad = rnd.Next(20, 80);
                    if (personaje.Raza.Equals("Human & Yeti")) edad = 9; // Nunu is a kid with a yeti
                    break;
                case "Darkin":
                    edad = rnd.Next(3500, 6000);
                    break;
                case "Golem":
                    if (personaje.Nombre.Equals("Orianna")) edad = rnd.Next(21, 60);
                    if (personaje.Nombre.Equals("Galio")) edad = rnd.Next(648, 705);
                    break;
                case "Spirit God":
                    edad = 10000;
                    break;
                case "Revenant":
                    edad = rnd.Next(16, 500);
                    break;
                case "Undead":
                    edad = rnd.Next(122, 179);
                    break;
                case "Yordle":
                    edad = 0;
                    break;
                case "Vastaya":
                    edad = rnd.Next(180, 250);
                    break;
                case "Ascendant":
                    edad = rnd.Next(900, 3000);
                    break;
            }
            personaje.Edad = edad;
            // Presente aproximado de noxus: 997 AN (2023 irl)
            // Obtencion de año de nacimiento - En Años Runaterra: BN | AN (Before/After Noxus)

            personaje.FecNac = new int[] { rnd.Next(1, 12), rnd.Next(1, 31), (int)Single.Abs(Constantes.presente - edad) };
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