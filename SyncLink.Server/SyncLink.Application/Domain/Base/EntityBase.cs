﻿namespace SyncLink.Application.Domain.Base;

public class EntityBase
{
    public int Id { get; private set; } = -1;

    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
}