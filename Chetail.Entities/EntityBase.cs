using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chetail.Entities
{
    /// <summary>
    /// Base class all entities derive from this class.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBase"/> class.
        /// </summary>
        public EntityBase()
        {
            //this.Id = Guid.NewGuid();
            this.Created = DateTime.Now;
        }

        // Gets or sets the id.
        [Required]
        [Key]
        public int Id { get; set; }

        // Gets or sets the creation date.
        [Required]
        public DateTime Created { get; set; }

        // Gets or sets the modification date.
        public DateTime? Modified { get; set; }
    }
}