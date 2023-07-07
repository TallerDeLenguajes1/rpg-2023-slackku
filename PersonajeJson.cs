using System.Text.Json;

namespace PersonajeSpace
{
    class PersonajeJson
    {
        public void GuardarPersonajes(List<Personaje> listaPer, string ruta)
        {
            string jsonString = JsonSerializer.Serialize(listaPer);
            using (StreamWriter persistence = File.AppendText(ruta))
            {
                persistence.WriteLine(jsonString);
                persistence.Close();
            }
        }
        public List<Personaje> LeerLista()
        {
            string ruta = Directory.GetCurrentDirectory();
            ruta += @"\persistence.json";
            using (StreamReader persistence = File.OpenText(ruta))
            {
                string jsonString = persistence.ReadToEnd();
                List<Personaje> listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(jsonString)!;
                return listaPersonajes;
            }
        }
        public Boolean Existe(string ruta)
        {
            FileInfo fInfo = new FileInfo(ruta);
            return fInfo.Exists && fInfo.Length != 0;
        }
    }
}