using System;
using System.Collections.Generic;
using System.Text;

namespace ex_videos
{
    public class Video
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
}
