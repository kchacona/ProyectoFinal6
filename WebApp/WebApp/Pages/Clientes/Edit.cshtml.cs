using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using WBL;

namespace WebApp.Pages.Clientes
{
    public class EditModel : PageModel
    {
        private readonly IClientesService ClientesService;

        public EditModel(IClientesService ClientesService)
        {
            this.ClientesService = ClientesService;
        }

        [BindProperty]
        public ClientesEntity Entity { get; set; } = new ClientesEntity();

        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }


        public async Task<IActionResult> OnGet()
        {

            try
            {
                if (id.HasValue)
                {
                    Entity = await ClientesService.GetById(new() { Cedula = id });
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
                if (Entity.Cedula.HasValue)
                {
                    //Actualizar 
                    var result = await ClientesService.Update(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "<input type='text' id='mens' value='Registro Modificado Correctamente' hidden></input><br/>";
                }
                else
                {
                    //Nuevo 
                    var result = await ClientesService.Create(Entity);

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
