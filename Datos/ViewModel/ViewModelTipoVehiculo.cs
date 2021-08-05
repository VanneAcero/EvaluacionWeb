using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ViewModel
{
    public class ViewModelTipoVehiculo
    {
        public int CodigoVehiculo { get; set; }
        public int DescripcionVehiculo { get; set; }
        public int Codigo { get; set; }        
        public string Nombres { get; set; }
        public int Estado { get; set; }

        public object ToList()
        {
            throw new NotImplementedException();
        }
    }
}
