//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebFacturaMvc.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.existencia = new HashSet<existencia>();
            this.detalleCotizacion = new HashSet<detalleCotizacion>();
            this.RFQItem = new HashSet<RFQItem>();
            this.compraDetalle = new HashSet<compraDetalle>();
            this.ventaDetalle = new HashSet<ventaDetalle>();
        }
    
        public string idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precioUnitario { get; set; }
        public string idCategoria { get; set; }
        public string idMarca { get; set; }
        public string bandaAncha { get; set; }
        public Nullable<int> channels { get; set; }
        public string unidadMedida { get; set; }
    
        public virtual categoria categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<existencia> existencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalleCotizacion> detalleCotizacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RFQItem> RFQItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<compraDetalle> compraDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ventaDetalle> ventaDetalle { get; set; }
    }
}
