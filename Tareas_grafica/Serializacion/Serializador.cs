 
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Tareas_grafica.Serializacion;

public class Serializador
{
    private JsonSerializerOptions opciones;

    public Serializador()
    {
        opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                    new JsonStringEnumConverter(),
                    new Color4Converter()
            },
        };
    }
    public static string ObtenerRutaCompleta(string nombreArchivo)
    {
        string rutaActual = (Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent?.FullName)!;
        string carpetaObjetos = Path.Combine(rutaActual, "Objetos_serializados");

        if (!Directory.Exists(carpetaObjetos))
            Directory.CreateDirectory(carpetaObjetos);

        if (!nombreArchivo.EndsWith(".json"))
            nombreArchivo += ".json";

        return Path.Combine(carpetaObjetos, nombreArchivo);
    }

    public void Guardar<T>(T objeto, string nombreArchivo)
    {
        try
        {
            string path = ObtenerRutaCompleta(nombreArchivo);
            string jsonString = JsonSerializer.Serialize(objeto, opciones);
            File.WriteAllText(path, jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al serializar el objeto: " + e.Message);
        }
    }

    public T? Cargar<T>(string nombreArchivo)
    {
        try
        {
            string rutaArchivo = ObtenerRutaCompleta(nombreArchivo);
            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException($"El archivo '{rutaArchivo}' no existe.");
            return JsonSerializer.Deserialize<T>(File.ReadAllText(rutaArchivo), opciones);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al deserializar el objeto: " + e.Message);
            return default;
        }
    }
}
