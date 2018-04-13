using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Model
{
    public abstract class Entity<T> : IEntity<T>
    {
        public Entity()
        {
            CreatedTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        object IEntity.Id
        {
            get { return Id; }
            set { }
        }

        [DataType(DataType.DateTime)]
        public DateTime CreatedTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedTime { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
