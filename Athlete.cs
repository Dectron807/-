//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ызщке
{
    using System;
    using System.Collections.Generic;
    
    public partial class Athlete
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Achievements { get; set; }
        public Nullable<int> SectionId { get; set; }
    
        public virtual Section Section { get; set; }
    }
}
