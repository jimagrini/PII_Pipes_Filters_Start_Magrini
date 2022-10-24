using System;
using System.Drawing;
using CognitiveCoreUCU;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y comprueba que la misma contenga rostro o no.
    /// </remarks>
    public class FilterConditional : IFilter
    {   
        /// <summary>
        /// Atributo añadido para poder determinar a través de que pipe continuar en el PipeConditionalFork
        /// </summary>
        /// <value></value>
        public bool isFace {get; set;}
        
        public string Path {get; set;}
        /// <summary>
        /// Método añadido para poder cambiar el directorio en el que se 
        /// guarda la copia de la imagen y para poder acceder a la misma.
        /// </summary>
        /// <param name="path"></param>
        public void PathChanger(string path)
        {
            this.Path = path;
        }

        /// Un filtro que retorna una copia de la imagen recibida y comprueba si la imagen tiene
        /// o no una cara utilizando el directorio en path.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida en un directorio indicado.</returns>
        public IPicture Filter(IPicture image)
        {
            IPicture result = image.Clone();

            CognitiveFace face= new CognitiveFace(true, Color.Red);
            face.Recognize(Path);
            
            if (face.FaceFound)
            {
                Console.WriteLine("Face detected!");
                isFace = true;
            }
            else
            {
                Console.WriteLine("No face detected");
                isFace = false;
            }
            return result;
        }
    }
}