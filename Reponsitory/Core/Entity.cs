﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Reponsitory.Core
{
    public abstract class Entity
    {
        [StringLength(50)]
        public string Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}