﻿namespace Architecture.Domain.MarkarEntity;

public interface IActivatable
{
    DateTime? ValidFrom { get;}
    DateTime? ValidTo { get; }
}
