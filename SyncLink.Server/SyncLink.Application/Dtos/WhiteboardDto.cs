﻿namespace SyncLink.Application.Dtos;

public class WhiteboardDto
{
    public string Name { get; set; } = null!;

    public List<WhiteboardElementDto> WhiteboardElements { get; set; } = null!;

    public int OwnerId { get; set; }

    public int GroupId { get; set; }

    public DateTime LastUpdatedTime { get; set; }
}

public class WhiteboardElementDto
{
    public ElementTypeEnumDto Type { get; set; }
    public string Value { get; set; } = null!;
    public string Id { get; set; } = null!;
    public int X { get; set; }
    public int Y { get; set; }
    public int Rotation { get; set; }
    public int Opacity { get; set; }
    public WhiteboardElementOptionsDto Options { get; set; } = null!;
}

public enum ElementTypeEnumDto
{
    BRUSH,
    LINE,
    RECT,
    ELLIPSE,
    IMAGE,
    TEXT,
    SHAPE
}

public class WhiteboardElementOptionsDto
{
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int? StrokeWidth { get; set; }
    public string StrokeColor { get; set; }
    public string Fill { get; set; }
    public LineJoinEnumDto LineJoin { get; set; }
    public LineCapEnumDto LineCap { get; set; }
    public int? Left { get; set; }
    public int? Top { get; set; }
    public int? FontSize { get; set; }
    public string FontFamily { get; set; }
    public string FontStyle { get; set; }
    public string FontWeight { get; set; }
    public string Color { get; set; }
    public string DashArray { get; set; }
    public int? DashOffset { get; set; }
    public int? X1 { get; set; }
    public int? Y1 { get; set; }
    public int? X2 { get; set; }
    public int? Y2 { get; set; }
    public int? Rx { get; set; }
    public int? Ry { get; set; }
    public int? Cx { get; set; }
    public int? Cy { get; set; }
}

public enum LineCapEnumDto
{
    BUTT,
    SQUARE,
    ROUND
}

public enum LineJoinEnumDto
{
    MITER,
    ROUND,
    BEVEL,
    MITER_CLIP
}