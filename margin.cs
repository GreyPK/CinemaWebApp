//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaWebApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class margin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public margin()
        {
            this.ticket = new HashSet<ticket>();
        }
    
        public float coef_start { get; set; }
        public float coef_mid { get; set; }
        public float coef_end { get; set; }
        public int id_margin { get; set; }
        public int id_film_type { get; set; }
    
        public virtual film_type film_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ticket> ticket { get; set; }
    }
}
