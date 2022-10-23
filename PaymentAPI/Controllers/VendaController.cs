using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Context;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaContext _context;

        public VendaController(VendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Registrar(Venda venda)
        {
            if (venda.Data > DateTime.Now)
                return BadRequest(new { Erro = "A data não pode ser superior a data atual" });

            _context.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Consultar), new { id = venda.IdVenda }, venda );
        }

        [HttpGet("{id}")]
        public IActionResult Consultar(int id)
        {
            var venda = _context.Vendas.Find(id);

            if (venda == null)
                return NotFound();

            return Ok(venda);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Venda venda)
        {
            var vendaBanco = _context.Vendas.Find(id);

            if (vendaBanco == null)
                return NotFound();

            if (venda.Data > DateTime.Now)
                return BadRequest(new { Erro = "A data não pode ser superior a data atual" });

            if (vendaBanco.Status == EnumStatusVenda.AguardandoPagamento)
            {
                vendaBanco.Status = venda.Status;
                vendaBanco.Data = venda.Data;
                if (vendaBanco.Status == EnumStatusVenda.PagamentoAprovado || vendaBanco.Status == EnumStatusVenda.Cancelada)
                { 
                    _context.Vendas.Update(vendaBanco);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(new { Erro = "O status só pode ser alterado para: PagamentoAprovado ou Cancelada." });
                }
            }

            else if (vendaBanco.Status == EnumStatusVenda.PagamentoAprovado)
            {
                vendaBanco.Status = venda.Status;
                vendaBanco.Data = venda.Data;
                if (vendaBanco.Status == EnumStatusVenda.EnviadoTransportadora || vendaBanco.Status == EnumStatusVenda.Cancelada)
                {
                    _context.Vendas.Update(vendaBanco);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(new { Erro = "O status só pode ser alterado para: EnviadoTransportadora ou Cancelada." });
                }
            }

            else
            {
                vendaBanco.Status = venda.Status;
                vendaBanco.Data = venda.Data;
                if (vendaBanco.Status == EnumStatusVenda.Entregue)
                {
                    _context.Vendas.Update(vendaBanco);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(new { Erro = "O status só pode ser alterado para: Entregue." });
                }
            }
           
            return Ok(vendaBanco);
        }
    }
}