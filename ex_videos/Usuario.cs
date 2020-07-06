using System;
using System.Collections.Generic;
using System.Text;

namespace ex_videos
{
    public class Usuario
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
