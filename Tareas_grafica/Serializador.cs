
using System.Text.Json;


namespace Tareas_grafica;

public class Serializador
{
    public void Serializar<U>(U objeto, string nombreArchivo, string path = @"Directory.GetCurrentDirectory()")
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(objeto);
            File.WriteAllText(nombreArchivo + ".json", jsonString);
            Console.WriteLine($"objeto serializado en {Path.Combine(path, nombreArchivo)}.json");
        }
        catch (Exception e) 
        { 
            Console.WriteLine("Error al serializar objeto: " + e.Message);
        }
    }

    
}
