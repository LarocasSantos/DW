#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesenvolvimentoWeb;
using System.Text.RegularExpressions;

namespace DesenvolvimentoWeb.Controllers
{


    public class PoemasController : Controller
    {
        private readonly DesenvolvimentoWebContext _context;

        public PoemasController(DesenvolvimentoWebContext context)
        {
            _context = context;
        }

        // GET: Poemas
        public async Task<IActionResult> Index()
        {
            List<Poema> lista = await _context.Poemas.ToListAsync();

            return View(GetRandomElements(lista, 6));


        }

        public async Task<IActionResult> IndexAutor(string autor)
        {

            var autorParsed = String.Join("", Regex.Matches(autor, @"\@(.+?)\@")
                                                .Cast<Match>()
                                                .Select(m => m.Groups[1].Value));


            List<Poema> lista = await _context.Poemas.Where(x => x.Autor == autorParsed).Take(6).ToListAsync();

            var aFaltar = 6 - lista.Count;

            List<Poema> result = new List<Poema>();
            result = Join(result, lista);

            if (aFaltar > 0)
            {

                for (int i = 0; i < aFaltar; i++)
                {
                    List<Poema> tempList = GetRandomElements(await _context.Poemas.ToListAsync(), 1);

                    if (!result.Contains(tempList[0]))
                    {
                        result = Join(result, tempList);

                    }
                    else
                    {
                        i--;
                        continue;
                    }

                }
                return View("Index", result);
            }
            return View("Index", lista);

        }



        public async Task<IActionResult> IndexPalavra(string palavraInput)
        {
            if (!String.IsNullOrEmpty(palavraInput))
            {
                palavraInput = palavraInput.ToUpper();
                List<Poema> lista = await _context.Poemas.Where(x => x.Autor.ToUpper().Contains(palavraInput) || x.Conteudo.ToUpper().Contains(palavraInput) || x.Titulo.ToUpper().Contains(palavraInput)).Take(6).ToListAsync();

                var aFaltar = 6 - lista.Count;

                List<Poema> result = new List<Poema>();
                result = Join(result, lista);

                if (aFaltar > 0)
                {

                    for (int i = 0; i < aFaltar; i++)
                    {
                        List<Poema> tempList = GetRandomElements(await _context.Poemas.ToListAsync(), 1);

                        if (!result.Contains(tempList[0]))
                        {
                            result = Join(result, tempList);

                        }
                        else
                        {
                            i--;
                            continue;
                        }

                    }
                    return View("Index", result);
                }
                return View("Index", lista);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> IndexTema(string tema)
        {

            var temaParsed = String.Join("", Regex.Matches(tema, @"\@(.+?)\@")
                                                .Cast<Match>()
                                                .Select(m => m.Groups[1].Value));


            List<Poema> lista = await _context.Poemas.Where(x => x.Tema == temaParsed).Take(6).ToListAsync();

            var aFaltar = 6 - lista.Count;
            List<Poema> result = new List<Poema>();
            result = Join(result, lista);

            if (aFaltar > 0)
            {

                for (int i = 0; i < aFaltar; i++)
                {
                    List<Poema> tempList = GetRandomElements(await _context.Poemas.ToListAsync(), 1);

                    if (!result.Contains(tempList[0]))
                    {
                        result = Join(result, tempList);

                    }
                    else
                    {
                        i--;
                        continue;
                    }

                }
                return View("Index", result);
            }
            return View("Index", lista);

        }




        public IActionResult Pesquisar()
        {
            return View();
        }

        public IActionResult Palavra()
        {
            return View();
        }


        public IActionResult Sobre()
        {
            return View();
        }



        public async Task<IActionResult> Autor()
        {
            List<string> Autores = await _context.Poemas.OrderBy(a => a.Autor).Select(x => x.Autor).Distinct().ToListAsync();
            List<string> AutoresParsed = new List<string>();

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            bool FirstAuthor = true;

            foreach (char c in alphabet)
            {
                bool letterUsed = false;


                foreach (string autor in Autores)
                {
                    if (autor[0] == char.ToUpper(c))
                    {
                        if (!letterUsed)
                        {
                            letterUsed = true;
                            if (FirstAuthor)
                            {
                                AutoresParsed.Add("<p>" + char.ToUpper(c) + " &nbsp &nbsp " + "@" + autor + "@" + " </p>");
                                FirstAuthor = false;
                            }
                            else
                            {
                                AutoresParsed.Add("<p class=\"maiormargem\" >" + char.ToUpper(c) + " &nbsp &nbsp " + "@" + autor + "@" + " </p>");

                            }
                        }
                        else
                        {
                            AutoresParsed.Add("<p> &nbsp &nbsp &nbsp &nbsp" + "@" + autor + "@" + " </p>");
                        }
                    }


                    //< p > A & nbsp & nbspAlberto Caeiro </ p >
                    //< p > &nbsp & nbsp & nbsp & nbspAlexandre 0'neill</p>
                    //< p class="maiormargem">B &nbsp &nbspNome do Autor</p>
                    //< p >&nbsp &nbsp &nbsp &nbspNome do Autor</p>
                    //< p class="maiormargem">C &nbsp &nbspNome do Autor</p>

                }

                //do something with letter

            }



            return View(AutoresParsed);
        }


        public async Task<IActionResult> Tema()
        {
            List<string> Autores = await _context.Poemas.OrderBy(a => a.Tema).Select(x => x.Tema).Distinct().ToListAsync();
            List<string> AutoresParsed = new List<string>();

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            bool FirstAuthor = true;

            foreach (char c in alphabet)
            {
                bool letterUsed = false;


                foreach (string autor in Autores)
                {
                    if (autor[0] == char.ToUpper(c))
                    {
                        if (!letterUsed)
                        {
                            letterUsed = true;
                            if (FirstAuthor)
                            {
                                AutoresParsed.Add("<p>" + char.ToUpper(c) + " &nbsp &nbsp " + "@" + autor + "@" + " </p>");
                                FirstAuthor = false;
                            }
                            else
                            {
                                AutoresParsed.Add("<p class=\"maiormargem\" >" + char.ToUpper(c) + " &nbsp &nbsp " + "@" + autor + "@" + " </p>");

                            }
                        }
                        else
                        {
                            AutoresParsed.Add("<p> &nbsp &nbsp &nbsp &nbsp" + "@" + autor + "@" + " </p>");
                        }
                    }


                    //< p > A & nbsp & nbspAlberto Caeiro </ p >
                    //< p > &nbsp & nbsp & nbsp & nbspAlexandre 0'neill</p>
                    //< p class="maiormargem">B &nbsp &nbspNome do Autor</p>
                    //< p >&nbsp &nbsp &nbsp &nbspNome do Autor</p>
                    //< p class="maiormargem">C &nbsp &nbspNome do Autor</p>

                }

                //do something with letter

            }



            return View(AutoresParsed);
        }

        public List<T> Join<T>(List<T> first, List<T> second)
        {
            if (first == null)
            {
                return second;
            }
            if (second == null)
            {
                return first;
            }

            return first.Concat(second).ToList();
        }
        private List<T> GetRandomElements<T>(IEnumerable<T> list, int elementsCount)
        {


            return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();


        }

        private bool PoemaExists(int id)
        {
            return _context.Poemas.Any(e => e.Id == id);
        }
    }
}
