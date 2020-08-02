using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAC.Controllers.Requests
{
    public class PesquisaProtocolo
    {
        public string ProtocoloId { get; set; }
        public string DigitroId { get; set; }
        public string Documento { get; set; }
        public string Nome { get; set; }
    }
}
