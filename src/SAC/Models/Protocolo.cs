using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAC.Models
{
    public class Protocolo
    {
        [Key]
        public string ProtocoloId { get; set; }
        public string DigitroId { get; set; }
        
        #region Props Cliente
        [DisplayName("Cliente")]
        public string ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        #endregion

        [DisplayName("Setor")]
        public int SetorId { get; set; }
        public virtual Setor Setor { get; set; }

        public string Observacao { get; set; }

        [DisplayName("Motivo")]
        public int MotivoId { get; set; }
        public virtual Motivo Motivo { get; set; }


        [DisplayName("Assunto")]
        public int AssuntoId { get; set; }
        public virtual Assunto Assunto { get; set; }

        public List<Documento> Documentos { get; set; }
        public DateTime DtCriacao { get; set; }
        public DateTime DtAlterado { get; set; }

        public Protocolo(){}
        public Protocolo(bool geraId)
        {
            if (geraId)
            {
                ProtocoloId = DateTime.Now.ToString("yyyyMMddHHmmssFFFF");
                DtCriacao = DtAlterado = DateTime.Now;
            }
        }
    }
}
