using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFacturaMvc.Utilidades
{
    public class UsuarioValidator : ValidationAttribute
    {
      
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    using (crmconceptoseEntities db = new crmconceptoseEntities())
        //    {
        //        int id = (int)value;
        //        idEstado = id;
        //        if (db.CIUDAD.Where(d => d.idEstado == id && d.descripcion == Palabra).Count() > 0)
        //        {
        //            return new ValidationResult("Esa ciudad con ese estado ya existe " + Palabra + " " + db.CIUDAD.Where(d => d.idEstado == id && d.descripcion == Palabra).Count());
        //        }
        //    }
        //    return ValidationResult.Success;
        //}
    }
}