using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enumerables
{
    public class EnumerablesDB
    {
        /// <summary>
        /// Borrado Logico
        /// </summary>
        public enum EstadoLogico
        {
            Activo = 0,
            Borrado = 1
        }

        public enum EnumInsert
        {
            Insert_Ok = 1,
            Insert_False = 0,
        }
        public enum EnumUpdate
        {
            Update_Ok = 1,
            Update_False = 0,
        }
        public enum EnumDelete
        {
            Delete_Ok = 1,
            Delete_False = 0,
        }
    }
}

//public enum EstadosCarrito
//{
//    Pagado = 1,
//    Sin_Pagar = 2
//}
//public enum EstadosPedidos
//{
//    Terminado = 1,
//    Cancelado = 2,
//    En_Preparacion = 3
//}
//public enum EstadosProductos
//{
//    Activo = 1,
//    Borrado = 2,
//    Sin_Stock = 3
//}
//public enum IdRoles
//{
//    Administrador = 1,
//    Usuario = 2,
//    Empleado = 3,
//    AdminyEmpleado = 4,
//    Todos = 5
//}


//public enum Categorias
//{
//    NiditoContencion = 1,
//    Reductor = 2,
//    AlmohadoAmamantar = 3,
//    ReductorHuevito = 4

//}


