namespace PersonajeSpace
{
    public static class Constantes
    {

        public static int presente = 997;
        public static Dictionary<string, Dictionary<int, List<string>>> personajeData = new Dictionary<string, Dictionary<int, List<string>>>()
        {
            ["Demacia"] = new Dictionary<int, List<string>>(){
                { 0 , new List<string>() { "Lucian", "The Purifier", "Human" }},
                { 1 , new List<string>() { "Galio", "The Colossus", "Golem" }},
                { 2 , new List<string>() { "Garen", "The Might of Demacia", "Human" }},
                { 3 , new List<string>() { "Lux", "The Lady of Luminosity", "Human" }},
                { 4 , new List<string>() { "Vayne", "The Night Hunter", "Human" }},
                { 5 , new List<string>() { "Kayle", "The Righteous", "Human" }},
                { 6 , new List<string>() { "Sylas", "The Unshackled", "Human" }},
                { 7 , new List<string>() { "Sona", "Maven of the strings", "Human" }},
            },
            ["Noxus"] = new Dictionary<int, List<string>>()
            {
                { 0 , new List<string>() { "Draven", "The Glorious Executioner", "Human" }},
                { 1 , new List<string>() { "Katarina", "The Sinister Blade", "Human" }},
                { 2 , new List<string>() { "Samira", "The Desert Rose", "Human" }},
                { 3 , new List<string>() { "Swain", "The Noxian Grand General", "Human" }},
                { 4 , new List<string>() { "LeBlanc", "The Deceiver", "Human" }},
                { 5 , new List<string>() { "Mordekaise", "The Iron Revenant", "Revenant" }},
                { 6 , new List<string>() { "Sion", "The Undead Juggernaut", "Undead" }},
                { 7 , new List<string>() { "Darius", "The Hand of Noxus", "Human" }}
            },
            ["Freljord"] = new Dictionary<int, List<string>>()
            {
                { 0 , new List<string>() { "Anivia", "The Cryophoenix", "Spirit God" }},
                { 1 , new List<string>() { "Ornn", "The Fire Below the Mountain", "Spirit God" }},
                { 2 , new List<string>() { "Volibear", "The Relentless Storm", "Spirit God" }},
                { 3 , new List<string>() { "Lissandra", "The Ice Witch", "Human" }},
                { 4 , new List<string>() { "Ashe", "The Frost Archer", "Human" }},
                { 5 , new List<string>() { "Sejuani", "The Winter's Wrath", "Human" }},
                { 6 , new List<string>() { "Tryndamere", "The Barbarian King", "Human" }},
                { 7 , new List<string>() { "Nunu & Willump", "The Boy and his Yeti", "Human & Yeti" }}
            },
            ["Jonia"] = new Dictionary<int, List<string>>()
            {
                { 0 ,  new List<string>() { "Zed", "The Master of shadows", "Human" }},
                { 1 ,  new List<string>() { "Yasuo", "The Unforgiven", "Human" }},
                { 2 ,  new List<string>() { "Yone", "The Unforgotten", "Human" }},
                { 3 ,  new List<string>() { "Xayah", "The Rebel", "Vastaya" }},
                { 4 ,  new List<string>() { "Rakan", "The Charmer", "Vastaya" }},
                { 5 ,  new List<string>() { "Varus", "The Arrow of Retribution", "Darkin" }},
                { 6 ,  new List<string>() { "Shen", "The Eye of Twilight", "Human" }},
                { 7 ,  new List<string>() { "Kennen", "The Hearth of the Tempest", "Yordle" }}
            },
            ["Piltover"] = new Dictionary<int, List<string>>()
            {
                { 0 , new List<string>() { "Caitlyn", "The Sheriff of Piltover", "Human" }},
                { 1 , new List<string>() { "Camille", "The Steel Shadow", "Human" }},
                { 2 , new List<string>() { "Ezreal", "The Prodigal Explorer", "Human" }},
                { 3 , new List<string>() { "Heimerdinger", "The Revered Inventor", "Yordle" }},
                { 4 , new List<string>() { "Jayce", "The Defender of Tomorrow", "Human" }},
                { 5 , new List<string>() { "Orianna", "The Lady of Clockwork", "Golem" }},
                { 6 , new List<string>() { "Seraphine", "The Starry-Eyed Songtress", "Human" }},
                { 7 , new List<string>() { "Vi", "The Piltover Enforcer", "Human" }}
            },
            ["Shurima"] = new Dictionary<int, List<string>>()
            {
                { 0 , new List<string>() { "Akshan", "The Rogue Sentinel", "Human" }},
                { 1 , new List<string>() { "Azir", "The Emperor of the sands", "Ascendant" }},
                { 2 , new List<string>() { "Xerath", "The Magus Ascendant", "Ascendant" }},
                { 3 , new List<string>() { "Renekton", "The Butcher of the sands", "Ascendant" }},
                { 4 , new List<string>() { "Nasus", "The Curator of the sands", "Ascendant" }},
                { 5 , new List<string>() { "Naafiri", "The Hound of a hundred bites", "Darkin" }},
                { 6 , new List<string>() { "Amumu", "The Sad Mummy", "Yordle" }},
                { 7 , new List<string>() { "K'Sante", "The Pride of Nazumah", "Human" }}
            },
        };

        public static string[] regiones = {
            "Noxus",
            "Piltover",
            "Shurima",
            "Demacia",
            "Jonia",
            "Freljord"
        };

        public static Dictionary<string, Spectre.Console.Color> razeColor = new Dictionary<string, Spectre.Console.Color>
        {
            {"Human", Spectre.Console.Color.Aqua},
            {"Ascendant", Spectre.Console.Color.DarkGoldenrod},
            {"Golem", Spectre.Console.Color.Grey74},
            {"Yordle", Spectre.Console.Color.DarkOrange3_1},
            {"Vastaya", Spectre.Console.Color.MediumVioletRed},
            {"Spirit God", Spectre.Console.Color.Silver},
            {"Revenant", Spectre.Console.Color.Teal},
            {"Undead", Spectre.Console.Color.Grey53},
            {"Darkin", Spectre.Console.Color.Red3_1}
        };

        public static int[] posibleAwardsHP = { 10, 5, 7, 11, 25, 20 };

        public static string urlApiRequest = "https://www.boredapi.com/api/activity?type=recreational&participants=1";

    }
}