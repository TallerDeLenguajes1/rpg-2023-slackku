
using PersonajeSpace;
using Spectre.Console;
namespace UserInterface
{

    public static class GameInterface
    {
        public static void mostrarImagen(string path, int option)
        {
            var image = new CanvasImage(path);
            var panel = new Panel(image).Border(BoxBorder.None);
            if (option == 0) panel.Border(BoxBorder.Double).HeaderAlignment(Justify.Center).Padding(2, 1, 2, 1).BorderColor(Color.Gold3_1);
            AnsiConsole.Write(Align.Center(panel));
        }

        public static void mostrarTexto(string text, Justify aligment, Color textColor)
        {
            AnsiConsole.Write(new Markup($"[{textColor.ToString()}]" + text + "[/]").Justify(aligment));
        }

        public static void mostrarTexto(string text, Justify aligment)
        {
            AnsiConsole.Write(new Markup(text).Justify(aligment));
        }


        public static void mostrarJuego()
        {
            mostrarImagen("./pixelArts/LeagueLogo.png", 0);
            mostrarTexto("DEADMATCH", Justify.Center, Color.Red3);
        }
        // Genera la tabla de un personaje dado
        private static Table generarTablePersonaje(Personaje personaje, TypeTable typeTable)
        {
            var table = new Table();
            var innerTable = new Table().Collapse();
            string fec = "";
            string edad;

            if (personaje.Edad.Equals(0)) { edad = "???"; } else { edad = personaje.Edad.ToString(); }

            if (personaje.FecNac[2] != 0) fec = personaje.FecNac[0] + "/" + +personaje.FecNac[1] + "/" + personaje.FecNac[2];

            if (personaje.FecNac[2] > Constantes.presente) { fec += " BN"; } else { fec += " AN"; }

            var colorPorRaza = Constantes.razeColor[personaje.Raza];

            // Creacion Columna con el nombre del personaje
            table.AddColumn(new TableColumn(new Text(personaje.Nombre, new Style(colorPorRaza).Decoration(Decoration.RapidBlink)).Justify(Justify.Center)));
            //Creacion de fila con el apodo
            table.AddRow(
                new Markup(personaje.Apodo,
                new Style(Color.White,
                null,
                null).Decoration(Decoration.Underline)).Justify(Justify.Center)
               );
            // Creacion de tabla para un alineamiento de edad, nacimiento y raza
            // centrado dentro de una fila
            table.AddEmptyRow();

            table.AddRow(
                new Table().AddColumn(
                    new TableColumn(
                        new Markup("Age: " + edad,
                            new Style(
                                Color.NavajoWhite1,
                                null,
                                Decoration.SlowBlink
                            )
                        )
                    )
                ).AddColumn(
                    new TableColumn(
                        new Markup(
                            "Race: " + personaje.Raza,
                            new Style(
                                colorPorRaza,
                                 null,
                                  null
                            )
                        )
                    )
                ).NoBorder().Centered()
            );

            table.AddRow(
                new Markup(
                    "Birthday: " + fec,
                        new Style(
                            Color.LightCyan3,
                            null,
                            Decoration.SlowBlink
                        )
                ).Centered()
            );

            if (typeTable.Equals(TypeTable.ShowCase))
            {
                // Tabla distinta si es para muestra o para el combate
                string region = personaje.Region;
                var root = new Tree("");
                innerTable.AddColumn(new TableColumn(new Markup("Stats")));
                innerTable.AddColumn(new TableColumn(new Markup("Region: " + region)));
                // Obtencion de la imagen dependiente de la region
                var image = new CanvasImage("./pixelArts/" + region + ".png");
                // Creacion de los nodos con las stats
                root.AddNode("VEL: " + personaje.Velocidad);
                root.AddNode("DEX: " + personaje.Destreza);
                root.AddNode("STR: " + personaje.Fuerza);
                root.AddNode("LVL: " + personaje.Nivel);
                root.AddNode("ARM: " + personaje.Armadura);
                root.AddNode("HP: " + personaje.Salud);

                innerTable.AddRow(root, image);

                table.AddRow(innerTable);
            }

            if (typeTable.Equals(TypeTable.Matchup))
            {
                Color colorBasedOnHP = Color.Chartreuse1;
                if (personaje.Salud < 80) colorBasedOnHP = Color.Yellow3;
                if (personaje.Salud < 60) colorBasedOnHP = Color.Yellow3_1;
                if (personaje.Salud < 40) colorBasedOnHP = Color.Orange3;
                if (personaje.Salud < 20) colorBasedOnHP = Color.Red;

                table.AddRow(
                    new Markup(
                        $"HP: {personaje.Salud}",
                        new Style(colorBasedOnHP, null, null, null)
                    ).Justify(Justify.Center)
                );
            }

            table.AddEmptyRow();
            return table.Alignment(Justify.Center);
        }

        // Genera las columnas internas de una fila con sus respectivas tablas dentro
        private static Layout generarLayoutColl(List<Personaje> personaje)
        {
            if (personaje.Count > 1)
            {
                var tableLeft = generarTablePersonaje(personaje[0], TypeTable.ShowCase);
                var tableRight = generarTablePersonaje(personaje[1], TypeTable.ShowCase);
                var layout = new Layout("Root").SplitColumns(
                    new Layout("Left"),
                    new Layout("Right")
                );
                layout["Left"].Update(tableLeft);
                layout["Right"].Update(tableRight);
                return layout;
            }
            else
            {
                var table = generarTablePersonaje(personaje[0], TypeTable.ShowCase).Collapse();
                var layout = new Layout("Root");
                layout["Root"].Update(table);
                return layout;
            }

        }

        // Genera las filas con cada par de personajes dentro
        private static List<Layout> generarLayoutRows(List<Personaje> listaPersonajes)
        {
            List<Personaje> listaCopia = listaPersonajes.GetRange(0, listaPersonajes.Count);
            List<List<Personaje>> listaRows = new List<List<Personaje>>();
            List<Layout> layoutRows = new List<Layout>();
            while (listaCopia.Count() > 0)
            {
                List<Personaje> listaPar = new List<Personaje>();
                for (int i = 0; i < 2; i++)
                {
                    if (listaCopia.Count() > 1)
                    {
                        listaPar.Add(listaCopia[i]);
                        listaCopia.Remove(listaCopia[i]);
                    }
                    else
                    {
                        listaPar.Add(listaCopia[0]);
                        listaCopia.Remove(listaCopia[0]);
                    }
                }
                listaRows.Add(listaPar);
            }

            listaRows.ForEach(pair =>
            {
                layoutRows.Add(generarLayoutColl(pair));
            });

            return layoutRows;
        }

        // Muestra cada fila con su respectivo par de personajes
        public static void showPersonajes(List<Personaje> listaPersonajes)
        {
            List<Layout> listaRows = generarLayoutRows(listaPersonajes);
            listaRows.ForEach(row => AnsiConsole.Write(row));
        }

        // Muestreo de los personajes participantes del matchup
        public static void showMatchUp(List<Personaje> match, int direction, int[] turnInfo)
        {
            string[] option = { "Attacking", "On Guard" };
            Color[] styleColor = { Color.Red1, Color.GreenYellow };
            if (!direction.Equals(0)) { option = new string[] { "On Guard", "Attacking" }; styleColor = new Color[] { Color.GreenYellow, Color.Red1 }; }

            var grid = new Grid();

            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();

            Panel centerPanel = new Panel("");

            if (turnInfo[0].Equals(9999)) { centerPanel = generarPanelCentroMatchup(); }
            else { centerPanel = generarPanelCentroMatchup(turnInfo); }

            grid.AddRow(
                generarTablePersonaje(match[0], TypeTable.Matchup).Caption(option[0], new Style(styleColor[0])),
                centerPanel,
                generarTablePersonaje(match[1], TypeTable.Matchup).Caption(option[1], new Style(styleColor[1]))
            );

            AnsiConsole.Write(new Align(grid.Expand(), HorizontalAlignment.Center, VerticalAlignment.Middle));

        }

        private static Panel generarPanelCentroMatchup()
        {
            Panel panel = new Panel(
                new CanvasImage("./pixelArts/vsImage.png")
            );
            return panel;
        }

        private static Panel generarPanelCentroMatchup(int[] turnInfo)
        {

            Panel panel = new Panel("");
            string direction = (turnInfo[2] % 2).Equals(0) ? "[yellow]> > > > > > > > >[/]" : "[yellow]< < < < < < < < <[/]";

            if (!turnInfo[0].Equals(0))
            {
                panel = new Panel(
                    new Panel(
                        new Markup(
                            $"[black on white] Round {turnInfo[2]} [/]\n\n" +
                            $"Damage Dealt: {turnInfo[0]}\n" +
                            direction
                        ).Centered()
                    ).Padding(2, 1, 2, 1).Expand()
                );

            }
            else
            {
                panel = new Panel(
                    new Panel(
                        new Markup(
                            $"[black on white] Round {turnInfo[2]} [/]\n\n" +
                            "¡DODGED!\n" +
                            direction
                        ).Centered()
                    ).Padding(2, 1, 2, 1).Expand()
                );
            }

            return panel.Collapse().Header("[grey93] Combat Info [/]").Collapse().HeaderAlignment(Justify.Center);
        }

        public static void showWinnerMatchup(Personaje personaje, double damageRatio, int[] bonus)
        {

            Grid grid = new Grid().Expand();

            Table table = new Table().Expand().Centered();
            table.AddColumn("").HideHeaders();
            table.AddRow(
                new Markup(
                    $"[white]{personaje.Nombre}[/], [white]{personaje.Apodo}[/] [bold gold3_1]won the match[/]"
                ).Centered()
            );
            table.AddEmptyRow();
            table.AddRow(
                new Markup(
                    $"[underline aqua]DAMAGE RATIO:[/][slowblink aqua]{((int)damageRatio)}[/]"
                ).Centered()
            );
            table.AddEmptyRow();
            table.AddRow(
                new Markup(
                    $"[bold lime]LVL: +{bonus[0]}[/] [bold lime]HP: +{bonus[1]}[/]"
                ).Centered()
            );


            CanvasImage cvImg = new CanvasImage("./pixelArts/winner.png");
            Panel panel = new Panel(cvImg).NoBorder();



            grid.AddColumn();
            grid.AddEmptyRow();

            grid.AddRow(panel).Alignment(Justify.Center);
            grid.AddRow(
                table.Centered()
            ).Alignment(Justify.Center);

            AnsiConsole.Write(new Align(grid, HorizontalAlignment.Center, VerticalAlignment.Middle));

        }

        public static void showWinnerAll(List<Personaje> winner)
        {
            Layout winnersLayout = generarLayoutColl(winner);

            // Add an api request to that api that gets random activities
            // for showing a random win text.

            AnsiConsole.Write(winnersLayout);
        }

    }
    public enum TypeTable
    {
        Matchup,
        ShowCase,
    }
}