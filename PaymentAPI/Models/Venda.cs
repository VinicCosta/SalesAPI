using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentAPI.Models
{
    
    public class Venda
    {
        [Key]
        public int IdVenda { get; protected set; }
        public string ItensVenda { get; set; }
        public int IdVendedor { get; set; }
        public string CPFVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string EmailVendedor { get; set; }
        public string TelefoneVendedor { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusVenda  Status { get; set; }
    }
}
