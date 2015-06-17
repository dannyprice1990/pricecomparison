using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chetail.DTO
{
    public abstract class DTOBase
    {
        public DTOBase()
        {
            this.Created = DateTime.Now;
        }

        // Gets or sets the id.
        public int Id { get; set; }

        // Gets or sets the creation date.
        public DateTime Created { get; set; }

        // Gets or sets the modification date.
        public DateTime? Modified { get; set; }
    }
}