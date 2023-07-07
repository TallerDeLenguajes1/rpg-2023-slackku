
using PersonajeSpace;
using Spectre.Console;
namespace MensajeSpace
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
            mostrarImagen("LeagueLogo.png", 0);
            mostrarTexto("DEADMATCH", Justify.Center, Color.Red3);
        }


        public static Layout generarLayoutColl(List<Personaje> personaje, TextStyleType textStyleType)
        {
            if (textStyleType == TextStyleType.ShowPersonaje)
            {
                var tableLeft = generarTablePersonaje(personaje[0]);
                var tableRight = generarTablePersonaje(personaje[1]);
                var layout = new Layout("Root").SplitColumns(
                    new Layout("Left"),
                    new Layout("Right")
                );
                layout["Left"].Update(tableLeft);
                layout["Right"].Update(tableRight);
                return layout;
            }
            return new Layout();
        }

        public static Table generarTablePersonaje(Personaje personaje)
        {
            var table = new Table();
            var innerTable = new Table().Collapse();
            string fec = personaje.FecNac.Month + "/" + +personaje.FecNac.Day + "/" + +personaje.FecNac.Year;
            var root = new Tree("");

            // Creacion Columna con el nombre del personaje
            table.AddColumn(new TableColumn(new Text(personaje.Nombre).Justify(Justify.Center)));
            //Creacion de fila con el apodo
            table.AddRow(
                new Markup(personaje.Apodo,
                new Style(Color.BlueViolet,
                null,
                Decoration.SlowBlink).Decoration(Decoration.Underline)).Justify(Justify.Center)
               );

            table.AddRow(
                new Table().AddColumn(
                    new TableColumn(
                        new Markup("Age: " + personaje.Edad,
                            new Style(
                                Color.BlueViolet,
                                null,
                                Decoration.SlowBlink
                            )
                        ).Justify(Justify.Right)
                    )
                ).AddColumn(
                    new TableColumn(
                        new Markup(
                            "Race: " + personaje.Raza,
                            new Style(
                                Color.BlueViolet,
                                 null,
                                  Decoration.SlowBlink
                            )
                        ).Justify(Justify.Center)
                    )
                ).AddColumn(
                    new TableColumn(
                        new Markup(
                            "Birthday: " + fec,
                             new Style(
                                Color.BlueViolet,
                                null,
                                 Decoration.SlowBlink
                            )
                        ).Justify(Justify.Left)
                    )
                ).Centered().NoBorder()
            );

            innerTable.AddColumn(new TableColumn(new Markup("Stats")));
            innerTable.AddColumn(new TableColumn(new Markup("Region")));

            var image = new CanvasImage("./pixelArts/Piltover.png");

            root.AddNode("VEL: " + personaje.Velocidad);
            root.AddNode("DEX: " + personaje.Destreza);
            root.AddNode("STR: " + personaje.Fuerza);
            root.AddNode("LVL: " + personaje.Nivel);
            root.AddNode("ARM: " + personaje.Armadura);
            root.AddNode("HP: " + personaje.Salud);

            innerTable.AddRow(root, image);

            table.AddRow(innerTable);

            return table;
        }

        


    }

    public enum TextStyleType
    {
        ShowPersonaje,
        ShowGridPersonaje,
        ShowMatchUp,
        ShowWinnerMU,
        ShowWinnerTournament
    }
}