﻿using System;
using System.Collections.Generic;

namespace SunLex.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }

        public List<BaseDomainEvent> Events = new();
    }

    public abstract class BaseEntity: BaseEntity<Guid>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
