using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalizaVan.Models;

namespace LocalizaVan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacoesController : ControllerBase
    {
        private readonly RastreioContext _context;

        public LocalizacoesController(RastreioContext context)
        {
            _context = context;
        }

        // GET: api/Localizacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localizacao>>> GetLocalizacoes()
        {
            return await _context.Localizacoes.ToListAsync();
        }

        // GET: api/Localizacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> GetLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);

            if (localizacao == null)
            {
                return NotFound();
            }

            return localizacao;
        }

        // PUT: api/Localizacoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalizacao(int id, Localizacao localizacao)
        {
            if (id != localizacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(localizacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalizacaoExists(id))
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

        // POST: api/Localizacoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Localizacao>> PostLocalizacao(Localizacao localizacao)
        {
            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocalizacao", new { id = localizacao.Id }, localizacao);
        }

        // DELETE: api/Localizacoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> DeleteLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            _context.Localizacoes.Remove(localizacao);
            await _context.SaveChangesAsync();

            return localizacao;
        }

        private bool LocalizacaoExists(int id)
        {
            return _context.Localizacoes.Any(e => e.Id == id);
        }
    }
}
