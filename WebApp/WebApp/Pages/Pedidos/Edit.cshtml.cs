using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using WBL;

namespace WebApp.Pages.Pedidos
{
    public class EditModel : PageModel
    {
        private readonly IPedidosService PedidosService;

        public EditModel(IPedidosService PedidosService)
        {
            this.PedidosService = PedidosService;
        }

        [BindProperty]
        public PedidosEntity Entity { get; set; } = new PedidosEntity();

        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }


        public async Task<IActionResult> OnGet()
        {

            try
            {
                if (id.HasValue)
                {
                    Entity = await PedidosService.GetById(new() { Codigo = id });
                }

                return Page();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                if (Entity.Codigo.HasValue)
                {
                    //Actualizar 
                    var result = await PedidosService.Update(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "<input type='text' id='mens' value='Registro Modificado Correctamente' hidden></input><br/>";
                }
                else
                {
                    //Nuevo 
                    var result = await PedidosService.Create(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "<input type='text' id='mens' value='Registro Agregado Correctamente' hidden></input><br/>";

                }

                return RedirectToPage("Grid");
            }



            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }




    }
}
