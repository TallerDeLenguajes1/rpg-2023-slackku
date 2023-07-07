using MensajeSpace;
using Spectre.Console;
namespace PersonajeSpace
{
    partial class Program
    {

        public static void Main()
        {
            PersonajeFactory personajeFactory = new PersonajeFactory();
            PersonajeJson personajeJson = new PersonajeJson();
            List<Personaje> listaPersonajes = new List<Personaje>();

            if (!personajeJson.Existe(Directory.GetCurrentDirectory() + @"\persistence.json"))
            {

                while (listaPersonajes.Count < 10 /*Cantidad de Personajes*/)
                {
                    Personaje personaje = personajeFactory.getPersonaje(); // Se aplica filtro que permite que nombres y apodos no sean repetidos
                    if (listaPersonajes.Find(personajeInLista => personajeInLista.Nombre.Equals(personaje.Nombre) || personajeInLista.Apodo.Equals(personaje.Apodo)) == null)
                    {
                        listaPersonajes.Add(personaje);
                    }
                }

                string ruta = Directory.GetCurrentDirectory();
                ruta += @"\persistence.json";
                personajeJson.GuardarPersonajes(listaPersonajes, ruta);
            }
            else
            {
                listaPersonajes = personajeJson.LeerLista();
            }
            GameInterface.mostrarJuego();



            List<List<Personaje>> fighBrackets = organizarFightBrackets(listaPersonajes);

            var some = GameInterface.generarLayoutColl(fighBrackets[0], TextStyleType.ShowPersonaje);

            AnsiConsole.Write(some);


            play(fighBrackets);
        }

        public static List<List<Personaje>> organizarFightBrackets(List<Personaje> listaPersonajes)
        {
            // 1- Genera una copia de la lista original para poder seguir trabajando con la original luego
            // 2- Genera aleatoriamente los pares de las batallas quitando de a pares de personajes en lista copia
            List<Personaje> copyList = listaPersonajes.GetRange(0, listaPersonajes.Count);
            Random rnd = new Random();
            List<List<Personaje>> matches = new List<List<Personaje>>(5);
            while (copyList.Count > 0)
            {
                List<Personaje> pair = new List<Personaje>(2);
                for (int i = 0; i < 2; i++)
                {
                    Personaje rndSelected = copyList[rnd.Next(0, copyList.Count)];
                    pair.Add(rndSelected);
                    copyList.Remove(rndSelected);
                }
                matches.Add(pair);
            }
            return matches;
        }

        public static void play(List<List<Personaje>> matches)
        {

            int round = 1;
            int pelea = 1;
            Console.WriteLine("Pelea {0}", pelea);
            double damageRatio = 0;
            do
            {
                int dañoRecibido = 0;
                Console.WriteLine("ROUND: {0}", round);
                if (round % 2 == 1)
                {
                    Console.WriteLine("Ataca {0}, {1}", matches[0][1].Nombre, matches[0][1].Apodo);
                    dañoRecibido = calcularDamage(matches[0], 1);
                    matches[0][0].Salud -= dañoRecibido;
                    damageRatio += dañoRecibido;
                }
                else
                {
                    Console.WriteLine("Ataca {0}, {1}", matches[0][0].Nombre, matches[0][0].Apodo);
                    dañoRecibido = calcularDamage(matches[0], 0);
                    matches[0][1].Salud -= dañoRecibido;
                    damageRatio += dañoRecibido;
                }
                Console.WriteLine(round % 2 == 1);
                Console.WriteLine(round % 2 == 0);

                Console.WriteLine("Daño recibido: {0}", dañoRecibido);
                Console.WriteLine("Salud 1: " + matches[0][0].Salud);
                Console.WriteLine("salud 2: " + matches[0][1].Salud);
                Console.WriteLine("-----------");
                Thread.Sleep(200);
                round++;
            } while (matches[0][0].Salud > 0 && matches[0][1].Salud > 0);
            damageRatio /= round;
            if (matches[0][0].Salud <= 0) Console.WriteLine("El ganador es: {0}, {1}", matches[0][1].Nombre, matches[0][1].Apodo);
            if (matches[0][1].Salud <= 0) Console.WriteLine("El ganador es: {0}, {1}", matches[0][0].Nombre, matches[0][0].Apodo);
            Console.WriteLine("RATIO DAMAGE: " + damageRatio);
            pelea++;


            // matches.ForEach(match =>
            // {
            //     int round = 1;
            //     int pelea = 1;
            //     Console.WriteLine("Pelea {0}", pelea);
            //     do
            //     {
            //         Console.WriteLine($"Round: {round}");
            //         Console.WriteLine("Salud 1: " + match[0].Salud);
            //         Console.WriteLine("salud 2: " + match[1].Salud);
            //         Console.WriteLine("-----------");
            //         if (round % 2 == 1) match[0].Salud -= calcularDamage(match, 0);
            //         if (round % 2 == 0) match[1].Salud -= calcularDamage(match, 1);

            //         round++;
            //     } while (match[0].Salud > 0 && match[1].Salud > 0);
            //     pelea++;
            // });
        }

        public static int calcularDamage(List<Personaje> pair, int direction)
        {
            Console.WriteLine("CURRENT JUGADOR: {0}", direction + 1);
            int efectividad = new Random().Next(1, 100);
            int ataqueCurrentTurn = pair[direction].Ataque();
            Personaje? contrincante = pair!.Find(personaje => !personaje.Ataque().Equals(ataqueCurrentTurn));
            return (((ataqueCurrentTurn * efectividad) - contrincante!.Defensa() + 2) / 400) * 2;
        }
    }
}