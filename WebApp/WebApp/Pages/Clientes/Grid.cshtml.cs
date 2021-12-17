using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WBL;
using Entity;

namespace WebApp.Pages.Clientes
{
    public class GridModel : PageModel
    {
        private readonly IClientesService ClientesService;

        public GridModel(IClientesService ClientesService)
        {
            this.ClientesService = ClientesService;
        }


        public IEnumerable<ClientesEntity> GridList { get; set; } = new List<ClientesEntity>();

        public string Mensaje { get; set; } = "";
        public async Task<IActionResult> OnGet()
        {

            try
            {
                GridList = await ClientesService.Get();

                if (TempData.ContainsKey("Msg"))
                {
                    Mensaje = TempData["Msg"] as string;
                }

                TempData.Clear();

                return Page();

            }
            catch (Exception ex)
            {

               return Content(ex.Message) ;
            }

        }

        public async Task<IActionResult> OnGetEliminar(int? id)
        {

            try
            {
                var result = await ClientesService.Delete( new ClientesEntity { Cedula = id});

                if (result.CodeError!=0)
                {
                    throw new Exception(result.MsgError);
                }

                TempData["Msg"] = "<input type='text' id='mens' value='Registro Eliminado Correctamente' hidden></input> <br//>"; ;

                return Redirect("Grid");
        

            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }
    }
}
