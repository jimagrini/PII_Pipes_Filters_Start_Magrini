using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;
using CompAndDel.Filters;

namespace CompAndDel.Pipes
{
    public class PipeConditionalFork: IPipe
    {
        public FilterConditional conditionalFilter;
        IPipe IsTruePipe;
        IPipe IsFalsePipe;
        
        /// <summary>
        /// La cañería recibe una imagen, la clona y envìa la original por una cañeria y la clonada por otra.
        /// </summary>
        /// <param name="tipoFiltro">Tipo de filtro que se debe aplicar sobre la imagen. Se crea un nuevo filtro con los parametros por defecto</param>
        /// <param name="nextPipe">Siguiente cañeria con filtro</param>
        /// <param name="next2Pipe">Siguiente cañeria sin filtro</param>
        public PipeConditionalFork(FilterConditional conditionalFilter, IPipe IsTruePipe, IPipe IsFalsePipe) 
        {
            this.conditionalFilter = conditionalFilter;
            this.IsTruePipe = IsTruePipe;
            this.IsFalsePipe = IsFalsePipe;           
        }
        
        /// <summary>
        /// La cañería recibe una imagen, chequea si la misma contiene un rostro o no
        ///  a través de FilterConditional y en caso de tener una cara envia la imagen por una
        /// cañeria, de lo contrario lo hara por otra
        /// </summary>
        /// <param name="picture">imagen a filtrar y enviar a las siguientes cañerías</param>
        public IPicture Send(IPicture picture)
        {
            picture = this.conditionalFilter.Filter(picture);
            if (conditionalFilter.isFace == true)
            {
                return this.IsTruePipe.Send(picture);
            }
            else
            {
                return this.IsFalsePipe.Send(picture);
            }
        }
    }
}