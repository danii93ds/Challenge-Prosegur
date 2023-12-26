using Challenge_Prosegur;
using Challenge_Prosegur.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Challenge.Test
{
    public class UsuariosControllerTest
    {
        UsuariosController Controller { get; set; }
        CafeteriaContext CafeteriaContext { get; set; }

        public UsuariosControllerTest() 
        {
            CafeteriaContext = new CafeteriaContext();
            Controller = new UsuariosController(CafeteriaContext);
        }

        [Fact]
        public void Crear_Ok() 
        {
            // Arange
            string apellido = "Montañez";
            string nombre = "Daniel";
            string dni = "33333333";
            Guid rolValido = Guid.NewGuid();
            Guid rolInvalido = Guid.Empty;

            // Act
            var resultadoErroneo = Controller.Crear(apellido, nombre, dni, rolInvalido);
            var resultadoExitoso = Controller.Crear(apellido,nombre,dni, rolValido);

            // Assert
            Assert.IsType<OkObjectResult>(resultadoExitoso);
            Assert.IsType<ConflictObjectResult>(resultadoErroneo);
        }
    }
}
