using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class LableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("UserEntity")]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity UserEntity { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Note { get;set; }
    }
}
