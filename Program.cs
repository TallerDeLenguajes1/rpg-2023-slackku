using UserInterface;
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
                    Console.WriteLine("Hola");
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

            GameInterface.showPersonajes(listaPersonajes);

            Console.Write((new Personaje()).Equals(new Personaje()));
            Console.Write((new Personaje()).Equals(null));

            List<List<Personaje>> fighBracketsFirst = organizarFightBrackets(listaPersonajes, new Personaje());

            List<Personaje> survivorsFirtsBracket = new List<Personaje>();
            List<Personaje> quarterFinalsSurvivors = new List<Personaje>();
            List<Personaje> semiFinalsSurvivors = new List<Personaje>();
            List<Personaje> finalSurvivor = new List<Personaje>();


            // Entran 10, sobreviven 5
            play(fighBracketsFirst, survivorsFirtsBracket);

            // Obtengo un afortunado para que hayan pares, el obtenido pasa a
            // la ronda siguiente directamente
            Personaje luckyOne = getLuckyOne(survivorsFirtsBracket);
            quarterFinalsSurvivors.Add(luckyOne);

            // Genera aleatoriamente matchup entre los 4 que hay, sobreviven 2
            List<List<Personaje>> fighBracketsSecond = organizarFightBrackets(survivorsFirtsBracket, luckyOne);
            play(fighBracketsSecond, quarterFinalsSurvivors); // (Cuartos de Final)

            // Obtengo un afortunado para que hayan pares, el obtenido pasa a
            // la ronda siguiente directamente
            luckyOne = getLuckyOne(quarterFinalsSurvivors);
            semiFinalsSurvivors.Add(luckyOne);

            // Obtengo el formato para la batalla del par actual, entran 2, sobrevive 1
            List<List<Personaje>> fightBracketThird = organizarFightBrackets(quarterFinalsSurvivors, luckyOne);
            play(fightBracketThird, semiFinalsSurvivors); // (Semifinal)

            // Obtengo el formato para la batalla de la final, entran 2, sobrevive 1
            List<List<Personaje>> finalBracket = organizarFightBrackets(semiFinalsSurvivors, new Personaje());
            play(finalBracket, finalSurvivor);

            GameInterface.showWinnerAll(finalSurvivor);
        }

        public static List<List<Personaje>> organizarFightBrackets(List<Personaje> listaPersonajes, Personaje luckyOne)
        {
            // 1- Genera una copia de la lista original para poder seguir trabajando con la original luego
            // 2- Genera aleatoriamente los pares de las batallas quitando de a pares de personajes en lista copia
            Random rnd = new Random();
            List<Personaje> copyList = listaPersonajes.GetRange(0, listaPersonajes.Count);
            if (!luckyOne.Nivel.Equals(0))
            {
                copyList.Remove(luckyOne);
            }
            List<List<Personaje>> matches = new List<List<Personaje>>();
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

        public static void play(List<List<Personaje>> matches, List<Personaje> survivors)
        {

            int pelea = 1;
            matches.ForEach(
                match =>
                {
                    int round = 1;
                    double damageRatio = 0;
                    GameInterface.showMatchUp(match, round % 2, new int[] { 9999 });
                    Thread.Sleep(2000);
                    do
                    {
                        int dañoRecibido = 0;
                        if (round % 2 == 1)
                        {
                            dañoRecibido = calcularDamage(match, 1);
                            match[0].Salud -= dañoRecibido;
                            damageRatio += dañoRecibido;
                        }
                        else
                        {
                            dañoRecibido = calcularDamage(match, 0);
                            match[1].Salud -= dañoRecibido;
                            damageRatio += dañoRecibido;
                        }
                        GameInterface.showMatchUp(match, round % 2, new int[] { dañoRecibido, (round % 2).Equals(0) ? match[1].Salud : match[0].Salud, round });
                        round++;
                        Thread.Sleep(1000);
                    } while (match[0].Salud > 0 && match[1].Salud > 0);
                    damageRatio /= round;
                    if (match[0].Salud <= 0)
                    {
                        int[] awards = winnerAwardPerMatch();
                        GameInterface.showWinnerMatchup(match[1], damageRatio, awards);
                        survivors.Add(match[1]);
                    }
                    else
                    {
                        int[] awards = winnerAwardPerMatch();
                        GameInterface.showWinnerMatchup(match[0], damageRatio, awards);
                        survivors.Add(match[0]);
                    }


                    Thread.Sleep(3000);

                    pelea++;
                }
            );


        }

        public static int[] winnerAwardPerMatch()
        {
            return new int[] { new Random().Next(1, 3), Constantes.posibleAwardsHP[new Random().Next(0, Constantes.posibleAwardsHP.Length)] };
        }

        public static Personaje getLuckyOne(List<Personaje> listaPersonajes)
        {
            return listaPersonajes[new Random().Next(0, listaPersonajes.Count)];
        }

        public static int calcularDamage(List<Personaje> pair, int direction)
        {
            int efectividad = new Random().Next(1, 100);

            int ataqueCurrentTurn = pair[direction].Ataque();

            Personaje contrincante = (direction.Equals(0)) ? pair[1] : pair[0];

            return (((ataqueCurrentTurn * efectividad) - contrincante.Defensa()) / 300);
        }
    }
}