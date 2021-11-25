using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//importar bibliotecas necessárias para o projeto 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICartaoDeCredito.DataBase;
using APICartaoDeCredito.Models;
//using System.Net.Mail;
using System.Text.RegularExpressions;

namespace APICartaoDeCredito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteDadosController : ControllerBase
    {
        private readonly ApiContex _context;

        public ClienteDadosController(ApiContex context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("lead")]
        public async Task<ActionResult<LeadRequest>> LeadUpdate(LeadRequest leadRequest)
        {
            

            return Ok(leadRequest);
        }

        // GET: api/ClienteDados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDados>>> GetClientesDb()
        {
            return await _context.ClientesDb.ToListAsync();
        }

        /* VERSÃO 1 GET - foi usado a classe TYPE na versão 1 do GET, para acessar dados da nossa Clsse ClienteDados
         * public class NEW_TYPE
         {
             public string Cartao { get; set; }
         };
        */

        // GET: api/ClienteDados/5
        [HttpGet("{email}")] //Definindo na rota que é possivel consultar o email apenas digitndo ele no navegador.
        public ActionResult<IEnumerable<ClienteDados>> GetEmail(string email)
        {
            /*VERÇÃO 1 
             * var list = from a in _context.ClientesDb
                        where a.Email.Equals(email)
                        select new NEW_TYPE
                        {
                            Cartao = a.CardNumber
                        };
            var CartoesdeCredito = await list.ToListAsync();
            return Ok(list);
            VERSÃO 2
            var list = _context.ClientesDb.Where(a => a.Email.Equals(email)).Select(s => new { Cartao = s.CardNumber }).ToList(); Essa versão retornava "Cartao = numero do cartão"
            */
            var list = _context.ClientesDb.Where(a => a.Email.Equals(email)).Select(s => s.CardNumber).ToList(); //VERSÃO 3 - essa versão retorna apenas o numero do cartão , e em ordem de criação
            return Ok(list);
        }

        // PUT: api/ClienteDados/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClienteDados(int id, ClienteDados clienteDados)
        {
            if (id != clienteDados.Id)
            {
                return BadRequest();
            }

            _context.Entry(clienteDados).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteDadosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClienteDados
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ClienteDados>> PostClienteDados(ClienteDados clienteDados)
        {
            /*Existe a opção de usar a classe MailAddress fornecida, porém a validação dele permite muitos erros.
             * Usando o Regex podemos ser mais acertivos, apesar da vasta variedade de formatos de emails existentes
             * bool IsValid(string emailaddress)
            {
                try
                {
                    MailAddress m = new MailAddress(emailaddress);

                    return true;
                }
                catch (FormatException)
                {
                    return false;
                }
            }*/
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"; //Formatação aceita na validação de email

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            bool isvalid = regex.IsMatch(clienteDados.Email); //Fazendo a validação de email.
            string card; //Recebe o numero de cartão de crédito concatenado
            var list = _context.ClientesDb.Select(a => new { a.CardNumber }).ToList(); //Registra todos os cartoes de crédito numa lista para validação

            do
            {
                Random rnd = new Random(); //Criando uma instancia da classe Random
                int ccn1 = rnd.Next(1000, 9999); //Alocando os numeros aleatórios gerados da classe Random em variaveis
                int ccn2 = rnd.Next(1000, 9999);
                int ccn3 = rnd.Next(1000, 9999);
                int ccn4 = rnd.Next(1000, 9998);
                card = $"{ccn1} {ccn2} {ccn3} {ccn4}";
            } while (card.Equals(list)); //Fazendo validação de que o cartão de crédito não existe no BD.
            clienteDados.CardNumber = card;

            if (isvalid) //iff de validação do formato do email
            {
                _context.ClientesDb.Add(clienteDados); //salvando tudo no InMemory Banco de dados
                await _context.SaveChangesAsync();

                return CreatedAtAction("PostClienteDados", new { clienteDados = clienteDados.CardNumber }, clienteDados.CardNumber); //Retorna o numero do cartão de crédito gerado
            }
            else
            {
                return CreatedAtAction("PostClienteDados", "This email format is not valid, try again"); //Retorna erro caso o formato do email digitado não seja válido
            }

        }


        // DELETE: api/ClienteDados/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDados>> DeleteClienteDados(int id)
        {
            var clienteDados = await _context.ClientesDb.FindAsync(id);
            if (clienteDados == null)
            {
                return NotFound();
            }

            _context.ClientesDb.Remove(clienteDados);
            await _context.SaveChangesAsync();

            return clienteDados;
        }

        private bool ClienteDadosExists(int id)
        {
            return _context.ClientesDb.Any(e => e.Id == id);
        }
    }
}
