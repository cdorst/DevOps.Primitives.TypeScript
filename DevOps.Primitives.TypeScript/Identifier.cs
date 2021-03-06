﻿using DevOps.Primitives.Strings;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Identifiers", Schema = nameof(TypeScript))]
    public class Identifier
    {
        public Identifier() { }
        public Identifier(in AsciiStringReference name) => Name = name;
        public Identifier(in string name) : this(new AsciiStringReference(in name)) { }

        [Key]
        [ProtoMember(1)]
        public int IdentifierId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference Name { get; set; }
        [ProtoMember(3)]
        public int NameId { get; set; }

        public override string ToString() => Name.Value;
    }
}
