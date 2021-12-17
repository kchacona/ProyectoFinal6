using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using WBL;

namespace WebApp.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly IProductosService ProductosService;

        public EditModel(IProductosService ProductosService)
        {
            this.ProductosService = ProductosService;
        }

        [BindProperty]
        public ProductosEntity Entity { get; set; } = new ProductosEntity();

        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }


        public async Task<IActionResult> OnGet()
        {

            try
            {
                if (id.HasValue)
                {
                    Entity = await ProductosService.GetById(new() { Id_Productos = id });
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
                if (Entity.Id_Productos.HasValue)
                {
                    //Actualizar 
                    var result = await ProductosService.Update(Entity);

                    if (result.CodeError != 0) throw new Exception(result.MsgError);
                    TempData["Msg"] = "<input type='text' id='mens' value='Registro Modificado Correctamente' hidden></input><br/>";
                }
                else
                {
                    //Nuevo 
                    var result = await ProductosService.Create(Entity);

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
