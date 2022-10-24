using System;
using System.Drawing;
using CognitiveCoreUCU;

namespace CompAndDel.Filters
{
    /// <summary>
    /// Un filtro que recibe una imagen y comprueba que la misma tenga una cara.
    /// </remarks>
    public class FilterConditional : IFilter
    {   
        /// <summary>
        /// Atributo añadido para poder determinar que pipe seguir en el PipeConditionalFork
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
        /// o no una cara mediante el directorio en la que esta se guardó previamente en un FilterSave.
        /// </summary>
        /// <param name="image">La imagen a la cual se le va a aplicar el filtro.</param>
        /// <returns>La imagen recibida en un directorio indicado.</returns>
        public IPicture Filter(IPicture image)
        {
            IPicture result = image.Clone();

            CognitiveFace face= new CognitiveFace(true, Color.GreenYellow);
            face.Recognize(Path);
            
            if (face.FaceFound)
            {
                Console.WriteLine("Face Found!");
                isFace = true;
            }
            else
            {
                Console.WriteLine("No Face Found");
                isFace = false;
            }
            return result;
        }
    }
}