using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventoContabil.Data;
using EventoContabil.Models;
using Microsoft.AspNetCore.Http;
using System;
using RestSharp;
using System.IO;
using Newtonsoft.Json.Linq;
using EventoContabil.Facade;
using System.Text;

namespace EventoContabil.Controllers
{
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            return View();
        }


        public static byte[] GetBytes(Stream s)
        {
            using (var ms = new MemoryStream())
            {
                s.CopyTo(ms);
                return ms.ToArray();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile upload)
        {

            var url = new Uri(@"https://xlsxtojson.herokuapp.com/api/v1/exceltojson");

            var rest = new RestClient(url) { Encoding = Encoding.UTF8 };

            var request = new RestRequest(Method.POST);
            request.AddFile("upload", GetBytes(upload.OpenReadStream()), upload.FileName, "multipart/form-data");

            var response = rest.Execute(request).Content;


            var json = JObject.Parse(response);

            var handler = new Handler(json);

            var eventos = handler.Eventos();

            await _context.Eventos.AddRangeAsync(eventos);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }




        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Conta,Descricao,Valor,Competencia,Tipo")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
