namespace PersonajeSpace
{
    public class Personaje
    {
        private string? nombre;
        private string? apodo;
        private string? raza;
        private DateTime fecNac;
        private int edad;
        private int vel;
        private int des;
        private int str;
        private int lvl;
        private int armor;
        private int hp;


        public string Raza { set => raza = value; get => raza!; }
        public string Nombre { set => nombre = value; get => nombre!; }
        public string Apodo { set => apodo = value; get => apodo!; }
        public DateTime FecNac { set => fecNac = value; get => fecNac; }
        public int Edad { set => edad = value; get => edad; }

        public int Velocidad { set => vel = value; get => vel; } // 0-10
        public int Destreza { set => des = value; get => des; } // 0-5
        public int Fuerza { set => str = value; get => str; } // 0-10 
        public int Nivel { set => lvl = value; get => lvl; } // 0-10 
        public int Armadura { set => armor = value; get => armor; } // 0-10
        public int Salud { set => hp = value; get => hp; } // 100

        public Personaje()
        {
        }

        public Personaje(string raz, string name, string nick, DateTime fecN, int age)
        {
            raza = raz;
            nombre = name;
            apodo = nick;
            fecNac = fecN;
            edad = age;
        }

        public int Ataque()
        {
            return Destreza * Fuerza * Nivel;
        }

        public int Defensa()
        {
            return Armadura * Velocidad;
        }
    }
}