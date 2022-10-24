using TwitterUCU;
using System;
using System.Drawing;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y retorna una copia de la misma en un directorio.
    /// </remarks>
    public class FilterTwitter : IFilter
    {   
        
        public string Path {get; set;}

        public string Post {get; set;}
        /// <summary>
        ///Metodo para cambiar el directorio en el que se guarda la copia de la imagen
        /// </summary>
        /// <param name="path"></param>
        public void PathChanger(string path)
        {
            this.Path = path;
        }

        /// Un filtro que retorna una copia de la imagen recibida y la publica en twitter
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida en un directorio indicado.</returns>
        public IPicture Filter(IPicture image)
        {
            IPicture result = image.Clone();
            
            var Twitter = new TwitterImage();
            Twitter.PublishToTwitter(Post, Path);
            Console.WriteLine(Twitter.PublishToTwitter(Post, Path));

            return result;
        }
    }
}