﻿namespace SyncLink.Application.Dtos;

public class MessageDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; private set; } = default;
    public DateTime EditedDateTime { get; private set; } = default;
    public bool IsEdited { get; private set; } = false;
    public string Text { get; private set; } = null!;
    public int SenderId { get; private set; } = 0;
    public int RoomId { get; private set; } = 0;
    public int GroupId { get; private set; } = 0;
}