using Challenge_Prosegur;
using Challenge_Prosegur.Controllers;
using Challenge_Prosegur.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge.Test
{
    public class OrdenesControllerTest
    {
        OrdenesController Controller { get; set; }
        CafeteriaContext CafeteriaContext { get; set; }

        public OrdenesControllerTest() 
        {
            CafeteriaContext = new CafeteriaContext();
            Controller = new OrdenesController(CafeteriaContext);
        }

        [Fact]
        public void Crear_Ok() 
        {
            // Arange
            Guid usuarioValido = Guid.NewGuid();
            Guid usuarioInvalido = Guid.Empty;
            List<OrdenesProductos> ordenesProductosValidoExistente = new List<OrdenesProductos>() {
                new OrdenesProductos() {

                }
            };
            List<OrdenesProductos> ordenesProductosValidoNoExistente = new List<OrdenesProductos>() {
                new OrdenesProductos() {
                    OrdenesID = Guid.NewGuid(),
                    ProductosID = Guid.NewGuid(),
                    Cantidad = 1,
                    UsuariosID = null,
                    Finalizada = false
                }
            };
            List<OrdenesProductos> ordenesProductosInvalido = new List<OrdenesProductos>();


            // Act
            var resultadoUsuarioErroneo = Controller.Crear(usuarioInvalido, ordenesProductosValidoExistente);
            var resultadoProductoNoExistente = Controller.Crear(usuarioValido, ordenesProductosValidoNoExistente);
            var resultadoProductoErroneo = Controller.Crear(usuarioValido, ordenesProductosInvalido);
            var resultadoExitoso = Controller.Crear(usuarioValido, ordenesProductosValidoExistente);

            // Assert
            Assert.IsType<OkObjectResult>(resultadoExitoso);
            Assert.IsType<ConflictObjectResult>(resultadoProductoNoExistente);
            Assert.IsType<BadRequestObjectResult>(resultadoProductoErroneo);
            Assert.IsType<BadRequestObjectResult>(resultadoUsuarioErroneo);
        }
    }
}
