using System;
using System.Collections.Generic;
using System.Linq;

namespace pruebas
{
    class Program
    {
        enum repro { Netflix, Play, Pause, Stop };

        /********************************************************************/
        // Funcion que indica si hay espacios entre los datos enviados
        /********************************************************************/
        private static string Espacios(string pregunta)
        {
            bool espacio = true;
            string respuesta = "";
            while (espacio)
            {
                Console.Clear();
                Console.WriteLine(pregunta);
                respuesta = Console.ReadLine();
                for (int i = 0; i < respuesta.Length; i++)
                {
                    if (respuesta[i] == ' ')
                    {
                        espacio = true;
                        Console.WriteLine("Los datos no pueden contener espacios.");
                        Console.WriteLine("Seleccione Enter.");
                        Console.ReadKey();
                        break;
                    }
                    else espacio = false;

                }

            }
            return respuesta;
        }
        /********************************************************************/
        // Menú reproducir, pausar y parar videos
        /*******************************************************************/
        static bool MenuRepro(repro repro1, string titulo, string accion)
        {
            Console.Clear();
            Console.WriteLine("  " + titulo);
            Console.WriteLine("==========================");
            Console.Write(repro1.ToString() + accion);
            Console.WriteLine();
            Console.WriteLine("\n1.Pulse <" + repro.Play.ToString() + ">");
            Console.WriteLine("2.Pulse <" + repro.Pause.ToString() + ">");
            Console.WriteLine("3.Pulse <" + repro.Stop.ToString() + ">");
            Console.WriteLine("4.Volver al menu anterior.");
            Console.Write("\nSeleccione una de las opciones: ");

            switch (Console.ReadLine())
            {
                case "1":
                    MenuRepro(repro.Play, titulo, ", reproduciendo video!!!");
                    return true;
                case "2":
                    MenuRepro(repro.Pause, titulo, ", pausando video!!!");
                    return true;
                case "3":
                    MenuRepro(repro.Stop, titulo, ", parando video!!!");
                    return true;
                case "4":
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /********************************************************************/
        // Menú añadir tags y ver videos
        /*******************************************************************/
        static bool MenuTagPlayVideo(Usuario user, int indice)
        {
            //asignamos a vid el objeto de la clase video que tiene el titulo elegido
            var vid = (from a in Video.listaVids
                       where a.titulo == user.listaVids[indice].titulo
                       select a).First();

            Console.Clear();
            Console.WriteLine("  " + user.listaVids[indice].titulo.ToUpper());
            Console.WriteLine("==========================");
            Console.WriteLine("1. Añadir tags.");
            Console.WriteLine("2. Ver tags.");
            Console.WriteLine("3. Ver video.");
            Console.WriteLine("4. Volver al listado.");
            Console.WriteLine();
            Console.Write("Seleccione una de las opciones: ");
            switch (Console.ReadLine())
            {
                case "1":
                    // Añadir tags a un video
                    string tag = Espacios("Añada un tag a " + user.listaVids[indice].titulo.ToUpper() + ":");
                    vid.addTag(tag);
                    Console.Write("Tag correcto. Seleccione <enter>");
                    Console.ReadKey();
                    return true;

                case "2":
                    // Ver tags de un video
                    Console.Clear();
                    Console.WriteLine(" TAGS DE " + user.listaVids[indice].titulo.ToUpper());
                    Console.WriteLine("==========================");
                    foreach (var i in vid.tags)
                    {
                        Console.Write(i + " ");
                    }
                    Console.Write("\n\nSeleccione <enter>");
                    Console.ReadKey();
                    return true;

                case "3":
                    // Reproduccion video                    
                    bool sel2 = true;
                    while (sel2)
                    {
                        try
                        {
                            sel2 = MenuRepro(repro.Netflix, user.listaVids[indice].titulo.ToUpper(), "");
                        }

                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.Write("Seleccione <enter>");
                            Console.ReadKey();
                        }
                    }
                    return true;
                case "4":
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /********************************************************************/
        // Menú crear y ver lista de videos
        /*******************************************************************/
        static bool MenuVideos(Usuario user)
        {
            Console.Clear();
            Console.WriteLine("    MENU DE VIDEO");
            Console.WriteLine("======================");
            Console.WriteLine("1. Crear nuevo video.");
            Console.WriteLine("2. Ver lista de videos.");
            Console.WriteLine("3. Volver al menú de inicio.");
            Console.WriteLine();
            Console.Write("Seleccione una de las opciones: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Título del video:");
                    string titulo = Console.ReadLine();
                    string url = Espacios("Url del video:");
                    // Crea video del usuario;
                    user.crea(url, titulo);
                    Console.WriteLine("\nVideo creado. Seleccione <enter>");
                    Console.ReadLine();
                    return true;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Videos de " + user.nombre + " " + user.apellido + ":");
                    Console.WriteLine("========================");
                    int indice = user.lista();
                    // si indice es cero no hay videos en la lista
                    if (indice == 0)
                    {
                        Console.WriteLine("\nLa lista está vacía");
                        Console.Write("Seleccione <enter>");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("\nSeleccione un numero de la lista:");
                        int sel3 = int.Parse(Console.ReadLine());
                        if ((sel3 - 1) <= indice)
                        {
                            bool sel4 = true;
                            while (sel4)
                            {
                                try
                                {
                                    // vamos al menu de tags y reproduccion
                                    sel4 = MenuTagPlayVideo(user, sel3 - 1);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.Write("Seleccione <enter>");
                                    Console.ReadKey();
                                }
                            }

                        }
                        else
                        {
                            Console.Write("El valor introducido no es correcto. Seleccione <enter>");
                            Console.ReadKey();
                        }
                    }
                    return true;
                case "3":
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }
        /*******************************************************************/
        // Menu Login de usuarios
        /*******************************************************************/
        static void MenuLogin()
        {
            string usuario = Espacios("Escriba su usuario:");
            string password = Espacios("Escriba su password:");

            // comprobar si existe usuario en lista
            bool existeUser = Usuario.listaUsers.Any(x => x.user == usuario && x.password == password);

            if (existeUser)
            {
                // Extraer todos los campos del objeto de Usuario elegido
                var user2 = (from a in Usuario.listaUsers
                             where a.user == usuario
                             select a).First();

                Console.WriteLine("\nBienvenido " + user2.nombre + "!!!!");
                Console.WriteLine("\nEl usuario y password son correctos.");
                Console.WriteLine("Seleccione <enter>");
                Console.ReadLine();
                // ir al menu de videos
                bool sel2 = true;
                while (sel2)
                {
                    try
                    {
                        sel2 = MenuVideos(user2);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.Write("Seleccione <enter>");
                        Console.ReadKey();
                    }
                }

            }
            else
            {
                Console.WriteLine("\nEl usuario o password no es correcto.");
                Console.WriteLine("Seleccione <enter> para hacer login o registrarse.");
                Console.ReadLine();
            }
        }
        /***************************************************************/
        // Menu registro usuario nuevo
        /****************************************************************/
        static void MenuRegistro()
        {
            //Llamamos funcion espacios para no permitir datos con espacios en blanco
            string nombreRegistro = Espacios("Escriba su nombre:");
            string apellidoRegistro = Espacios("Escriba su primer apellido:");
            string nuevoUsuario = "";

            // Evitamos que dos usuarios tengan el mismo user
            bool existeUser = true;
            while (existeUser)
            {
                nuevoUsuario = Espacios("Escriba el usuario que quiere utilizar:");
                if (Usuario.listaUsers.Any(x => x.user == nuevoUsuario))
                {
                    Console.WriteLine("\nYa existe un usuario con ese nombre, pruebe otro. Seleccione <Enter>");
                    Console.ReadLine();
                }
                else existeUser = false;
            }
            string nuevoPassword = Espacios("Escriba un password:");

            // Creamos un objeto Usuario para el usuario que se acaba de registrar
            var user1 = new Usuario(nuevoUsuario, nombreRegistro, apellidoRegistro, nuevoPassword);

            Console.WriteLine("\nUsuario creado con éxito. Seleccione <Enter>");
            Console.ReadLine();
            // Vamos al menu de videos
            bool sel2 = true;
            while (sel2)
            {
                try
                {
                    sel2 = MenuVideos(user1);
                }


                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Seleccione <enter>");
                    Console.ReadKey();
                }
            }

        }
        /****************************************************************/
        // Menu de inicio
        /****************************************************************/
        static bool MenuInicio()
        {

            Console.Clear();
            Console.WriteLine("    MENU DE INICIO");
            Console.WriteLine("======================");
            Console.WriteLine("1. Usuario Login.");
            Console.WriteLine("2. Registro de usuario.");
            Console.WriteLine("3. Finalizar");
            Console.Write("\nSeleccione una de las opciones: ");
            switch (Console.ReadLine())
            {
                case "1":
                    MenuLogin();
                    return true;
                case "2":
                    MenuRegistro();
                    return true;
                case "3":
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /****************************************************************/
        // Programa principal
        /****************************************************************/
        static void Main(string[] args)
        {
            //usuarios ya registrados
            Usuario userPrueba1 = new Usuario("fgil", "Francisco", "Gil", "1234");
            Usuario userPrueba2 = new Usuario("pgarcia", "Pedro", "Garcia", "5678");
            bool sel1 = true;
            while (sel1)
            {
                try
                {
                    sel1 = MenuInicio();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Seleccione <enter>");
                    Console.ReadKey();
                }
            }
        }
    }
    /****************************************************************/
    // Clase Video
    /****************************************************************/
    class Video
    {
        //campos
        public string url { get; set; }
        public string titulo { get; set; }
        public List<String> tags = new List<String>();
        //lista de videos
        public static List<Video> listaVids = new List<Video>();

        //Constructor clase video
        public Video(string url, string titulo)
        {
            this.url = url;
            this.titulo = titulo;
            listaVids.Add(this);
        }
        // añada tags a lista
        public void addTag(string tag)
        {
            this.tags.Add(tag);
        }
    }
    /****************************************************************/
    // Clase Usuario
    /****************************************************************/
    class Usuario
    {
        //Campos
        public string user { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string password { get; set; }
        public DateTime fechaRegistro { get; set; }
        //lista de videos de cada usuario
        public List<Video> listaVids = new List<Video>();
        //lista de usuarios
        public static List<Usuario> listaUsers = new List<Usuario>();

        //Constructor clase usuario
        public Usuario(string user, string nombre, string apellido, string password)
        {
            this.user = user;
            this.nombre = nombre;
            this.apellido = apellido;
            this.password = password;
            this.fechaRegistro = DateTime.Now;
            listaUsers.Add(this);
        }

        // crea videos
        public void crea(string url, string titulo)
        {
            Video video1 = new Video(url, titulo);
            listaVids.Add(video1);

        }
        //ver lista videos
        public int lista()
        {
            int indice = 0;
            if (listaVids.Count == 0)
            {
                return indice;
            }
            else
            {
                foreach (var i in listaVids)
                {
                    Console.WriteLine((indice + 1) + ". " + i.titulo);
                    indice++;
                }
                return indice;
            }
        }
    }
}
