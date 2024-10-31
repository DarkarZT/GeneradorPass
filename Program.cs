using System;
using System.Text;
using System.Security.Cryptography;

class Generador
{
    private int longitud { get; set; }
    private string cifrado { get; set; }
    private string contrasenia { get; set; }

    public Generador(int lon)
    {
        longitud = lon;
        contrasenia = "";
        cifrado = "";

    }

    public void Hash()
    {
        Console.WriteLine(cifrado);
    }
    
    public void GenerarContra()
    {
        Random randomizador = new Random();
        StringBuilder constructString = new StringBuilder();
        string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyz" +   
                                   "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +    
                                   "0123456789" +                
                                   "!@#$%^&*()_+-=[]{}|;':,.<>?";


        for (int i = 0; i < longitud; i++)
        {
            char CaracterIndependiente = caracteresPermitidos[randomizador.Next(caracteresPermitidos.Length)];
            constructString.Append(CaracterIndependiente);
        }
        contrasenia = constructString.ToString();
        Console.WriteLine("Contraseña generada: " + contrasenia);

    }
    public void Cifrado()
    {
        if (string.IsNullOrEmpty(contrasenia))
        {
            throw new Exception("No se ha creado contraseña");
        }
        else
        {
            using (Aes asim = Aes.Create())
            {
                asim.Key = Encoding.UTF8.GetBytes("pepeGrillo123456");
                asim.IV = new byte[16];

                ICryptoTransform encripcion = asim.CreateEncryptor(asim.Key, asim.IV);
                byte[] encrypt = encripcion.TransformFinalBlock(Encoding.UTF8.GetBytes(contrasenia), 0, contrasenia.Length);

                cifrado = Convert.ToBase64String(encrypt);

            }
        }
           
    }
   
}

class Program
{
    static void Main()
    {
        int option = -1;    
        Generador contrasenja = new Generador(5);
        Console.WriteLine("Elija una opción:");
        Console.WriteLine("1. Generar Contraseña");
        Console.WriteLine("2. Cifrar Contraseña");
        Console.WriteLine("3. Mostrar Hash de la Contraseña");

        while (option != 0)
        {

            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        contrasenja.GenerarContra();
                        break;
                    case 2:
                        contrasenja.Cifrado();
                        break;
                    case 3:
                        contrasenja.Hash();
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            }
            else
            {
                Console.WriteLine("Entrada no válida.");
            }
            Console.WriteLine("Elija una opción:");
            Console.WriteLine("1. Generar Contraseña");
            Console.WriteLine("2. Cifrar Contraseña");
            Console.WriteLine("3. Mostrar Hash de la Contraseña");
        }
    }
}