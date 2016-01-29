using System;

namespace ContactosModel.Model
{
    public class ContactoModel
    {
        //quien está agregando a un contacto
        public int idOrigen { get; set; }
        //quien está siendo agregado
        public int idDestino { get; set; }

        public String nombreCompleto { get; set; }
        
         
    }
}